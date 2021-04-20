using System.Text.Json.Serialization;

namespace Brain.Cli.Dtos
{
    public class LoginResponse
    {
        [JsonPropertyName("_success")]
        public bool success{ get; set; }

        [JsonPropertyName("_token")]
        public string token{ get; set; }
    }
}