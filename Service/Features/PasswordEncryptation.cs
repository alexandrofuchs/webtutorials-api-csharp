using System;
using System.Security.Cryptography;
using System.Text;

namespace WebTutorialsApp.Middleware.Features
{
    public class PasswordEncryptation
    {
        #region INSTANCIATION
        private readonly HashAlgorithm _hashAlgorithm;
        public PasswordEncryptation()
        {  
            _hashAlgorithm = SHA512.Create();            
        }
        #endregion INSTANCIATION

        #region PUBLIC METHODS
        public string Encrypt(string password)
        {
            if (!(password.Length > 0))
            {
                throw new Exception("Invalid Password!");
            }

            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _hashAlgorithm.ComputeHash(encodedValue);
            return ConverterToString(encryptedPassword);
        }
        public bool Verify(string informedPassword, string storedPassword)
        {
            if (informedPassword == null || !(informedPassword.Length > 0))
            {
                throw new Exception("Invalid informed password!");
            }
            if (storedPassword == null || !(storedPassword.Length > 0))
            {
                throw new Exception("Invalid stored password!");
            }
            var encodedValue = Encoding.UTF8.GetBytes(informedPassword);
            var encryptedInformedPassword = _hashAlgorithm.ComputeHash(encodedValue);
            return ConverterToString(encryptedInformedPassword) == storedPassword;
        }
        #endregion PUBLIC METHODS

        #region PRIVATE METHODS
        private string ConverterToString(byte[] encryptedValue)
        {
            var stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedValue)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
        #endregion PRIVATE METHODS
    }
}
