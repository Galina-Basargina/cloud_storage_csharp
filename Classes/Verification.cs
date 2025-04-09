using System;
using System.Security.Cryptography;
using System.Text;

namespace coursework3.Classes
{
    internal class Verification
    {
        // Шифровка строки в md5
        public static string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Конвертируем байты в HEX-строку
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // x2 - формат в 16-ричной системе
                }
                return sb.ToString();
            }
        }

        // Метод для проверки пароля
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            return inputPassword.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
