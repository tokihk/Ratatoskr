using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic
{
    internal static class CryptoUtil
    {
        private static readonly Guid SEED_BASE = new Guid("C4E4A99D-4C22-4C2C-AD29-241CF4058576");


        public static (byte[] key, byte[] iv) GenerateKey(string passcode, int key_size, int block_size)
        {
            var seed = new Rfc2898DeriveBytes(passcode, SEED_BASE.ToByteArray());

            seed.IterationCount = 1000;

            return (seed.GetBytes(key_size), seed.GetBytes(block_size));
        }

        public static byte[] Encrypt(byte[] plain_data, string passcode)
        {
            var crypto_mgr = new AesManaged();
            var crypto_key = GenerateKey(passcode, crypto_mgr.KeySize / 8, crypto_mgr.BlockSize / 8);

            crypto_mgr.Key = crypto_key.key;
            crypto_mgr.IV = crypto_key.iv;

            using (var encryptor = crypto_mgr.CreateEncryptor()) {
                return (encryptor.TransformFinalBlock(plain_data, 0, plain_data.Length));
            }
        }

        public static string EncryptText(string text, string passcode)
        {
            return (System.Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(text), passcode)));
        }

        public static byte[] Decrypt(byte[] crypto_data, string passcode)
        {
            var crypto_mgr = new AesManaged();
            var crypto_key = GenerateKey(passcode, crypto_mgr.KeySize / 8, crypto_mgr.BlockSize / 8);

            crypto_mgr.Key = crypto_key.key;
            crypto_mgr.IV = crypto_key.iv;

            using (var decryptor = crypto_mgr.CreateDecryptor()) {
                return (decryptor.TransformFinalBlock(crypto_data, 0, crypto_data.Length));
            }
        }

        public static string DecryptText(string text, string passcode)
        {
            return (Encoding.UTF8.GetString(Decrypt(System.Convert.FromBase64String(text), passcode)));
        }
    }
}
