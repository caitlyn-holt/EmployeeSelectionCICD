using EmployeeSelection.UITests.PageObjects;
using FluentAssertions;
using TechTalk.SpecFlow;
using System;
using System.Threading;

namespace EmployeeSelection.UITests.StepDefinitions
{
    [Binding]
    public class PasswordPolicySteps
    {
        private LoginWindowPage _loginPage;
        private bool _result;
        private string? _currentPassword;

        public PasswordPolicySteps()
        {
            _loginPage = TestHooks.LoginPage;
        }


        [Given(@"пользователь вводит пароль ""(.*)""")]
        public void GivenUserEntersPassword(string password)
        {
            if (password == "null")
                _currentPassword = null;
            else
                _currentPassword = password?.Trim('"');

            // вводим пароль в поле (если UI открыт)
            if (_loginPage != null && _currentPassword != null)
            {
                _loginPage.EnterPassword(_currentPassword);
                Thread.Sleep(500);
            }
        }
        [Given(@"пользователь имеет пароль ""(.*)""")]
        public void GivenUserHasPassword(string password)
        {
            GivenUserEntersPassword(password);
        }
        [When(@"система проверяет надежность пароля")]
        public void WhenSystemChecksPasswordStrength()
        {
            if (_loginPage != null && _currentPassword != null)
            {
                // Вводим уникальный логин для каждого теста
                string uniqueUsername = $"test_{Guid.NewGuid():N}".Substring(0, 10);
                _loginPage.EnterUsername(uniqueUsername);
                Thread.Sleep(300);

                // Нажимаем кнопку регистрации (она проверит пароль)
                _loginPage.ClickRegister();
                Thread.Sleep(1000); // Ждём результат

                // Проверяем результат: если нет сообщения об ошибке -> пароль надёжный
                string errorMessage = _loginPage.GetErrorMessage();

                // Если сообщение об ошибке содержит "Пароль ненадёжный" или подобное
                // В зависимости от того, как ваше приложение сообщает об ошибке
                if (string.IsNullOrEmpty(errorMessage))
                {
                    // Нет ошибки - регистрация прошла, пароль надёжный
                    _result = true;
                }
                else if (errorMessage.Contains("ненадёжный") || errorMessage.Contains("должен содержать") || errorMessage.Contains("цифру"))
                {
                    // Есть сообщение о ненадёжном пароле
                    _result = false;
                }
                else
                {
                    // Другая ошибка
                    // В этом случае нужно очистить и попробовать снова
                    _loginPage.ClearFields();
                    Thread.Sleep(500);

                    // Повторяем с другим логином
                    uniqueUsername = $"test_{Guid.NewGuid():N}".Substring(0, 10);
                    _loginPage.EnterUsername(uniqueUsername);
                    Thread.Sleep(300);
                    _loginPage.EnterPassword(_currentPassword);
                    Thread.Sleep(300);
                    _loginPage.ClickRegister();
                    Thread.Sleep(1000);

                    errorMessage = _loginPage.GetErrorMessage();
                    _result = string.IsNullOrEmpty(errorMessage);
                }

                // Очищаем поля для следующего теста
                _loginPage.ClearFields();
                Thread.Sleep(500);
            }
            else
            {
                // Fallback: если UI не доступен, используем прямой вызов
                var authService = new EmployeeSelection.Core.Services.AuthenticationService();
                _result = authService.IsPasswordStrong(_currentPassword);
            }
        }

        [When(@"пользователь пытается установить пароль ""(.*)""")]
        public void WhenUserTriesToSetPassword(string password)
        {
            GivenUserEntersPassword(password);
            WhenSystemChecksPasswordStrength();
        }


        [Then(@"пароль должен считаться надежным")]
        public void ThenPasswordShouldBeStrong()
        {
            _result.Should().BeTrue("пароль должен соответствовать политике надежности");
        }

        [Then(@"пароль должен считаться ненадежным")]
        public void ThenPasswordShouldBeWeak()
        {
            _result.Should().BeFalse("пароль НЕ должен соответствовать политике надежности");
        }

        [Then(@"результат должен быть (true|false)")]
        public void ThenResultShouldBe(bool expected)
        {
            _result.Should().Be(expected);
        }

        [Then(@"результат должен быть ""(.*)""")]
        public void ThenResultShouldBeString(string expected)
        {
            bool expectedBool = expected.ToLower() == "true" || expected.ToLower() == "истина";
            _result.Should().Be(expectedBool);
        }
    }
}
