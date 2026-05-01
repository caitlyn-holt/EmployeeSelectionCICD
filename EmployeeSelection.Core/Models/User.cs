using System.Security.Cryptography;
using System.Text;

namespace EmployeeSelection.Core.Models
{
    /// <summary>
    /// Представляет пользователя системы с функциями аутентификации.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя для входа в систему.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Хеш пароля пользователя (алгоритм SHA-256).
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Роль пользователя в системе (например, "HR", "Admin", "Candidate").
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="User"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль в открытом виде (будет захеширован).</param>
        /// <param name="role">Роль пользователя в системе.</param>
        public User(int id, string username, string password, string role)
        {
            Id = id;
            Username = username;
            Role = role;
            PasswordHash = HashPassword(password);
        }

        /// <summary>
        /// Изменяет пароль пользователя.
        /// </summary>
        /// <param name="newPassword">Новый пароль.</param>
        public void ChangePassword(string newPassword)
        {
            PasswordHash = HashPassword(newPassword);
        }

        /// <summary>
        /// Проверяет корректность введённого пароля.
        /// </summary>
        /// <param name="password">Пароль для проверки.</param>
        /// <returns>True, если пароль верный; иначе False.</returns>
        public bool ValidatePassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }

        /// <summary>
        /// Хеширует пароль с использованием алгоритма SHA-256.
        /// </summary>
        /// <param name="password">Пароль для хеширования.</param>
        /// <returns>Хеш пароля в формате Base64.</returns>
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
