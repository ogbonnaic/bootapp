using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Bootapp.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace Bootapp.Util
{
    public interface IKeyTokenUtils
    {
        public string GenerateToken(dynamic obj);
        public bool ValidateToken(string key,string token);
        string GenerateKey(int size, bool lowerCase = false);
    }

    public class KeyTokenUtils : IKeyTokenUtils
    {
        private readonly IConfiguration _configuration;

        public KeyTokenUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public string GenerateToken(dynamic obj)
        {
            try
            {
                var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));
              
                var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(obj.api_key.ToString()));

                return Convert.ToBase64String(signature);

            }
            catch(Exception ex)
            {
                return null;
            }
         
        }

        public bool ValidateToken(string key,string token)
        {
            var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));

            var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

            var encodestring= Convert.ToBase64String(signature);

            if (encodestring == token)
            {
                return true;
            }

            return false;

        }




        public string GenerateKey(int size,bool lowerCase=false)
        {
            string source = "abcdefghijklmnopqrstuvwxyz0123456789";

            if (!lowerCase)
                source = source.ToUpper();

            var sb = new StringBuilder();
            Random random = new();
            for (var i = 0; i < size; i++)
            {
                var c = source[random.Next(0, source.Length)];
                sb.Append(c);
            }

            return sb.ToString();
        }

       
    }
}
