using System.Linq;
using EmployeeSelection.Core.Models;
using EmployeeSelection.Core.Services;

namespace EmployeeSelection.Core.Updaters
{
    /// <summary>
    /// Класс для комплексного обновления данных пользователя.
    /// </summary>
    public class UserUpdater
    {
        private AuthenticationService auth;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserUpdater"/>.
        /// </summary>
        /// <param name="auth">Сервис аутентификации для выполнения операций.</param>
        public UserUpdater(AuthenticationService auth)
        {
            this.auth = auth;
        }

        /// <summary>
        /// Обновляет данные пользователя: регистрирует нового или обновляет существующего.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <param name="role">Роль пользователя.</param>
        /// <returns>Обновленный или созданный объект пользователя.</returns>
        public User Update(string username, string password, string role)
        {
            var user = auth.GetUsers()
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                user = auth.RegisterUser(username, password, role);
            }
            else
            {
                if (!user.ValidatePassword(password))
                    user.ChangePassword(password);

                auth.ChangeUserRole(username, role);
            }

            return user;
        }
    }
}
