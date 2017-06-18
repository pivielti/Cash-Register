using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Web.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [JsonProperty("username")]
        //[RegularExpression("/^[a-z0-9_-]{3,16}$/")]
        public string UserName { get; set; }

        [Required]
        [JsonProperty("password")]
        //[RegularExpression("/^[a-zA-Z0-9_-]{6,18}$/")]
        public string Password { get; set; }
    }
}
