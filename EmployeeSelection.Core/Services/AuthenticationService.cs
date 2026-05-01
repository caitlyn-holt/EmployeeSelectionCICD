using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeSelection.Core.Models;

namespace EmployeeSelection.Core.Services
{
    /// <summary>
    /// Сервис для управления пользователями: регистрация, аутентификация и управление ролями.
    /// </summary>
    public class AuthenticationService
    {
        private List<User> users = new List<User>();

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="username">Имя пользователя (должно быть уникальным).</param>
        /// <param name="password">Пароль (минимум 6 символов).</param>
        /// <param name="role">Роль пользователя в системе.</param>
        /// <returns>Созданный объект пользователя.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь уже существует или пароль слишком короткий.</exception>
        public User RegisterUser(string username, string password, string role)
        {
            if (users.Any(u => u.Username == username))
                throw new Exception("User exists");
            if (password.Length < 6)
                throw new Exception("Password too short");

            var user = new User(users.Count + 1, username, password, role);
            users.Add(user);
            return user;
        }

        /// <summary>
        /// Выполняет вход пользователя в систему.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Объект авторизованного пользователя.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден или пароль неверный.</exception>
        public User Login(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new Exception("User not found");
            if (!user.ValidatePassword(password))
                throw new Exception("Wrong password");

            return user;
        }

        /// <summary>
        /// Изменяет роль существующего пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="role">Новая роль пользователя.</param>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден.</exception>
        public void ChangeUserRole(string username, string role)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new Exception("User not found");

            user.Role = role;
        }

        /// <summary>
        /// Возвращает список всех зарегистрированных пользователей.
        /// </summary>
        /// <returns>Список объектов <see cref="User"/>.</returns>
        public List<User> GetUsers()
        {
            return users;
        }

        /// <summary>
        /// Проверяет надежность пароля.
        /// </summary>
        /// <param name="password">Пароль для проверки.</param>
        /// <returns>True, если пароль надежный (минимум 8 символов и содержит цифру); иначе False.</returns>
        public bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;
            return password.Any(char.IsDigit);
        }
    }
}
