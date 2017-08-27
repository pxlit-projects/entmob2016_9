using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.Security.Cryptography.Core;

namespace EuphoricElephant.Custom
{
    public static class CustomPasswordIncriptor
    {
        public static String sha256_hash(String password)
        {
            IBuffer input = CryptographicBuffer.ConvertStringToBinary(password,
            BinaryStringEncoding.Utf8);

            // hash it...
            var hasher = HashAlgorithmProvider.OpenAlgorithm("SHA256");
            IBuffer hashed = hasher.HashData(input);

            // format it...
            return CryptographicBuffer.EncodeToHexString(hashed);

        }
    }
}
