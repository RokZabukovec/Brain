namespace Brain.Api.Resources
{
    public class UserAuthenticatedResponse
    {
        public string _status { get; set; }

        public string _token { get; set; }

        public UserAuthenticatedResponse(string status, string token)
        {
            _status = status;

            _token = token;
        }
    }
}