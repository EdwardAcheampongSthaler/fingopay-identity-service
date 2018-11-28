using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace STH.BiometricIdentityService.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }


        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool EqualsTo(this string str, string other, bool caseSensitive = false)
        {
            return string.Equals(str, other,
                                 caseSensitive
                                     ? StringComparison.CurrentCulture
                                     : StringComparison.CurrentCultureIgnoreCase);
        }

        public static string Encrypt(this string currentString, string salt, string RFCPassword)
        {
            int Rfc2898KeygenIterations = 100;
            int AesKeySizeInBits = 128;
            byte[] Salt = Encoding.Default.GetBytes(salt);
            byte[] rawPlaintext = Encoding.Unicode.GetBytes(currentString);
            byte[] cipherText = null;
            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = AesKeySizeInBits;
                int KeyStrengthInBytes = aes.KeySize / 8;
                Rfc2898DeriveBytes rfc2898 =
                    new Rfc2898DeriveBytes(RFCPassword, Salt, Rfc2898KeygenIterations);
                aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                        cs.FlushFinalBlock();
                    }
                    cipherText = ms.ToArray();
                }
            }
            return Convert.ToBase64String(cipherText);
        }

        public static string Decrypt(this string currentString, string salt, string RFCPassword)
        {
            int Rfc2898KeygenIterations = 100;
            int AesKeySizeInBits = 128;
            byte[] Salt = Encoding.Default.GetBytes(salt);
            byte[] cipherText = Convert.FromBase64String(currentString);
            byte[] plainText = null;
            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = AesKeySizeInBits;
                int KeyStrengthInBytes = aes.KeySize / 8;
                Rfc2898DeriveBytes rfc2898 =
                    new Rfc2898DeriveBytes(RFCPassword, Salt, Rfc2898KeygenIterations);
                aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                        //cs.FlushFinalBlock();
                    }
                    plainText = ms.ToArray();
                }

            }
            return Encoding.Unicode.GetString(plainText);
        }
    }
}