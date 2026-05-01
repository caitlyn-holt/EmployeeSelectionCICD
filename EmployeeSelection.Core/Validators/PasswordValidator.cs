using System.Linq;

namespace EmployeeSelection.Core.Validators
{
    /// <summary>
    /// Предоставляет методы для валидации паролей согласно политике безопасности.
    /// </summary>
    public class PasswordValidator
    {
        /// <summary>
        /// Проверяет, соответствует ли пароль требованиям надёжности.
        /// </summary>
        /// <param name="password">Пароль для проверки. Может быть <c>null</c>.</param>
        /// <returns>
        /// <c>true</c>, если пароль имеет длину не менее 8 символов и содержит хотя бы одну цифру;
        /// иначе <c>false</c>.
        /// </returns>
        public bool IsStrong(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;
            return password.Any(char.IsDigit);
        }

        /// <summary>
        /// Проверяет, имеет ли пароль минимальную требуемую длину.
        /// </summary>
        /// <param name="password">Пароль для проверки. Может быть <c>null</c>.</param>
        /// <param name="minLength">Минимальная допустимая длина пароля.</param>
        /// <returns>
        /// <c>true</c>, если пароль не пуст и его длина больше или равна <paramref name="minLength"/>;
        /// иначе <c>false</c>.
        /// </returns>
        public bool HasMinimumLength(string password, int minLength)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= minLength;
        }

        /// <summary>
        /// Проверяет, содержит ли пароль хотя бы одну цифру.
        /// </summary>
        /// <param name="password">Пароль для проверки. Может быть <c>null</c>.</param>
        /// <returns>
        /// <c>true</c>, если пароль не пуст и содержит хотя бы один символ цифры;
        /// иначе <c>false</c>.
        /// </returns>
        public bool ContainsDigit(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Any(char.IsDigit);
        }
    }
}
