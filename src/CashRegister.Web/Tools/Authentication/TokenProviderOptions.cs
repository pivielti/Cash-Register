﻿using Microsoft.IdentityModel.Tokens;
using System;

namespace CashRegister.Web.Tools.Authentication
{
    public class TokenProviderOptions
    {
        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}
