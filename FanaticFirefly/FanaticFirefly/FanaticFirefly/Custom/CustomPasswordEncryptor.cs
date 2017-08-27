using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace FanaticFirefly.Custom
{
    public static class CustomPasswordEncryptor
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
