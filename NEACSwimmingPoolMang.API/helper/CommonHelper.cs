using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using NEACSwimmingPoolMang.helper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.API.helper
{
    public class CommonHelper : ICommonHelper
    {
        private readonly IConfiguration Configuration;
        public CommonHelper(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string VerifyHashedPassword(string password)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(this.Configuration["user:salt"]);

            byte[] hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            );

            string hashText = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            return hashText;
        }


    }
}
