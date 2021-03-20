namespace Brain.Api.Resources
{
    public class UserAuthenticatedResponse
    {
        public bool _success { get; set; }

        public string _token { get; set; }

        public UserAuthenticatedResponse(bool success, string token)
        {
            _success = success;

            _token = token;
        }
    }
}