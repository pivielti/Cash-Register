namespace CashRegister.Web.Models.Auth
{
    public class AuthenticationOptions
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expiration { get; set; }
    }
}
