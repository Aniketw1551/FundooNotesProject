//-----------------------------------------------------------------------
// <copyright file="Cipher.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace CommonLayer.Model
{
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

    /// <summary>
    /// Cipher Model
    /// </summary>
    public class Cipher
    {
        /// <summary>
        /// Method to encrypt password
        /// </summary>
        /// <param name="encryptPassword">Encrypt password</param>
        /// <returns>encrypt password</returns>
        public static string Encrypt(string encryptPassword)
        {
            try
            {
                string encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                byte[] clearBytes = Encoding.Unicode.GetBytes(encryptPassword);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
                    {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                  });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptPassword = Convert.ToBase64String(ms.ToArray());
                    }

                    return encryptPassword;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to decrypt password
        /// </summary>
        /// <param name="cipherText">The cipher</param>
        /// <returns>decrypt password</returns>
        public static string Decrypt(string cipherText)
        {
            try
            {
                string encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] 
                    {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }

                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }

                return cipherText;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}