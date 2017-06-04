using Newtonsoft.Json;

namespace CashRegister.Web.Models.Auth
{
    public class UserDatas
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("roles")]
        public string[] Roles { get; set; }
    }
}
