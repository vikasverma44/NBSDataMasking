using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetCommonUtilities
{
    public class CryptoUtility
    {
        private const string keyString = "NZsP6NnmfBuYeJrrAKNuVQ==";
        public static string ComputeHash(string text)
        {
            try
            {
                return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                     password: text,
                     salt: Encoding.UTF8.GetBytes(keyString.ToCharArray()),
                     prf: KeyDerivationPrf.HMACSHA1,
                     iterationCount: 10000,
                     numBytesRequested: 256 / 8));
            }
            catch { return string.Empty; }
        }

        public static string Encrypt(string text)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(keyString);
                using (var aesAlg = Aes.Create())
                {
                    using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(text);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            return Convert.ToBase64String(result);
                        }
                    }
                }
            }
            catch { return string.Empty; }
        }
        public static string Decrypt(string cipherText)
        {
            try
            {
                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[16];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
                var key = Encoding.UTF8.GetBytes(keyString);

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch { return string.Empty; }
        }

        // Default size of random password is 15  
        public static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

    }
}
