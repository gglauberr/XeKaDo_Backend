using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.CrossCutting.Extensions.Hash
{
    public static class HashExtensions
    {
        public static byte[] BytesSHA512(this string input)
        {
            using var sha512 = new SHA512Managed();
            var bytes = Encoding.UTF8.GetBytes(input);
            var bHash = sha512.ComputeHash(bytes);
            return bHash;
        }

        public static string SHA512(this string input)
        {
            var sHash = input.BytesSHA512()
                .Select((b) => b.ToString("x2"))
                .Aggregate((i, n) => string.Concat(i, n));
            return sHash;
        }
    }
}
