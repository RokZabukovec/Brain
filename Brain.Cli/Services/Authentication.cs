using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Brain.Cli.Dtos;
using Brain.Cli.Exceptions;

namespace Brain.Cli.Services
{
    public class Authentication
    {
        private readonly HttpClient _client;
        
        public bool isAuthenticated()
        {
            return true;
        }
        
        public async Task<String> Login(LoginResponse loginResponse)
        {
            HttpResponseMessage response = _client.PostAsync("https://localhost:5001/api/auth/login", new StringContent("{Email:'kzabuk@gmail.com',Password:'Chemistryforgood1!'}", Encoding.UTF8, "application/json")).Result;
            String content = await response.Content.ReadAsStringAsync();
            try
            {
                LoginResponse login =  JsonSerializer.Deserialize<LoginResponse>(content);
                if (login.success != null && login.success)
                {
                    return login.token;
                }

                throw new LoginException("Login failed.");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}