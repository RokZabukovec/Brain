using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Brain.Api.Models.Account;
using Brain.Api.Resources;
using Brain.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Brain.Api.Controllers.Authentication
{

    [ApiController]
    [Route("/api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly  JwtSettings _jwtSettings;
        
        private readonly RoleManager<Role> _roleManager;
        
        private readonly UserManager<User> _userManager;
        
        private readonly IMapper _mapper;

        public AuthenticationController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }
        
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(UserSignupResource userSignUpResource)
        {
            
            var user = _mapper.Map<UserSignupResource, User>(userSignUpResource);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            if (userCreateResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                string jwtToken = GenerateJwt(user, roles);
                return Ok(new UserAuthenticatedResponse(true, jwtToken));
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserSignupResource userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
            if (user is null)
            {
                return NotFound(new UserAuthenticatedResponse(false, null));
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var jwtToken = GenerateJwt(user, roles);
                return Ok(new UserAuthenticatedResponse(true, jwtToken));
            }

            return BadRequest("Email or password incorrect.");
        }
        
        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if(string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }
        
        [HttpPost("user/{userEmail}/role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }
        
        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires : expires,
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}