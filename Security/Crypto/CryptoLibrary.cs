using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tieptuyen.Api.Security.Crypto
{
    /// <summary>
    /// Provides cryptographic functionality.
    /// </summary>
    public static class CryptoLibrary
    {
        private const string PassPhrase = "k55P@5sw0rD";
        private const string SaltValue = "5@1tvalu3";
        private const string HashAlgorithm = "SHA1";
        private const string InitVector = "@1B2c3D4e5F6g7H8";
        private const int PasswordIterations = 2;
        private const int KeySize = 256;

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">Plaintext value to be encrypted.</param>
        /// <returns>Encrypted value formatted as a base64-encoded string.</returns>
        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, SaltValue);
        }

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">Plaintext value to be encrypted.</param>
        /// <param name="saltValue">SaltValue value used in the encryption.</param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt(string plainText, string saltValue)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            PassPhrase,
                                                            saltValueBytes,
                                                            HashAlgorithm,
                                                            PasswordIterations))
            {
                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                // ReSharper disable CSharpWarnings::CS0618
                byte[] keyBytes = password.GetBytes(KeySize / 8);
                // ReSharper restore CSharpWarnings::CS0618

                // Create uninitialized Rijndael encryption object.
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    // It is reasonable to set encryption mode to Cipher Block Chaining
                    // (CBC). Use default options for other symmetric key parameters.
                    symmetricKey.Mode = CipherMode.CBC;

                    // Generate encryptor from the existing key bytes and initialization 
                    // vector. Key size will be defined based on the number of the key 
                    // bytes.
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                                     keyBytes,
                                                                     initVectorBytes))
                    {
                        // Define memory stream which will be used to hold encrypted data.
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Define cryptographic stream (always use Write mode for encryption).
                            using (CryptoStream cryptoStream = new CryptoStream(
                                memoryStream,
                                encryptor,
                                CryptoStreamMode.Write))
                            {
                                // Start encrypting.
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                                // Finish encrypting.
                                cryptoStream.FlushFinalBlock();

                                // Convert our encrypted data from a memory stream into a byte array.
                                byte[] cipherTextBytes = memoryStream.ToArray();

                                // Close both streams.
                                memoryStream.Close();
                                cryptoStream.Close();

                                // Convert encrypted data into a base64-encoded string.
                                string cipherText = Convert.ToBase64String(cipherTextBytes);

                                // Return encrypted string.
                                return cipherText;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts specified cipher text using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">Base64-formatted cipher text value.</param>
        /// <returns>Decrypted string value.</returns>
        public static string Decrypt(string cipherText)
        {
            return Decrypt(cipherText, SaltValue);
        }

        /// <summary>
        /// Decrypts specified cipher text using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">Base64-formatted cipher text value.</param>
        /// <param name="saltValue">SaltValue value used in the encryption.</param>
        /// <returns>Decrypted string value.</returns>
        public static string Decrypt(string cipherText, string saltValue)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            PassPhrase,
                                                            saltValueBytes,
                                                            HashAlgorithm,
                                                            PasswordIterations))
            {
                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                // ReSharper disable CSharpWarnings::CS0618
                byte[] keyBytes = password.GetBytes(KeySize / 8);
                // ReSharper restore CSharpWarnings::CS0618

                // Create uninitialized Rijndael encryption object.
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    // It is reasonable to set encryption mode to Cipher Block Chaining
                    // (CBC). Use default options for other symmetric key parameters.
                    symmetricKey.Mode = CipherMode.CBC;

                    // Generate decryptor from the existing key bytes and initialization 
                    // vector. Key size will be defined based on the number of the key 
                    // bytes.
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                                     keyBytes,
                                                                     initVectorBytes))
                    {
                        // Define memory stream which will be used to hold encrypted data.
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            // Define cryptographic stream (always use Read mode for encryption).
                            using (CryptoStream cryptoStream = new CryptoStream(
                                memoryStream,
                                decryptor,
                                CryptoStreamMode.Read))
                            {
                                // Since at this point we don't know what the size of decrypted data
                                // will be, allocate the buffer long enough to hold ciphertext;
                                // plaintext is never longer than ciphertext.
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                                // Start decrypting.
                                int decryptedByteCount = cryptoStream.Read(
                                    plainTextBytes,
                                    0,
                                    plainTextBytes.Length);

                                // Close both streams.
                                memoryStream.Close();
                                cryptoStream.Close();

                                // Convert decrypted data into a string. 
                                // Let us assume that the original plaintext string was UTF8-encoded.
                                string plainText = Encoding.UTF8.GetString(
                                    plainTextBytes,
                                    0,
                                    decryptedByteCount);

                                // Return decrypted string.   
                                return plainText;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tries to decrypt a cipher.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="decryptText">The decrypt text.</param>
        /// <returns><c>true</c> on success; otherwise <c>false</c>.</returns>
        public static bool TryDecrypt(string cipherText, out string decryptText)
        {
            return TryDecrypt(cipherText, SaltValue, out decryptText);
        }

        /// <summary>
        /// Tries to decrypt a cipher.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="saltValue">The salt value.</param>
        /// <param name="decryptText">The decrypt text.</param>
        /// <returns><c>true</c> on success; otherwise <c>false</c>.</returns>
        public static bool TryDecrypt(string cipherText, string saltValue, out string decryptText)
        {
            try
            {
                decryptText = Decrypt(cipherText, saltValue);
                return true;
            }
            catch (Exception)
            {
                decryptText = cipherText;
                return false;
            }
        }

        /// <summary>
        /// Generates a random salt value.
        /// </summary>
        /// <param name="length">The length of the salt value.</param>
        /// <returns>Random generated salt value.</returns>
        public static string GenerateSaltValue(int length)
        {
            byte[] randomArray = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomArray);
            }

            return Convert.ToBase64String(randomArray);
        }
    }
}
