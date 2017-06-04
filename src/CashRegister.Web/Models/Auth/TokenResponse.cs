using Newtonsoft.Json;

namespace CashRegister.Web.Models.Auth
{
    public class TokenResponse
    {
        [JsonProperty("auth_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int Expiration { get; set; }
    }
}
