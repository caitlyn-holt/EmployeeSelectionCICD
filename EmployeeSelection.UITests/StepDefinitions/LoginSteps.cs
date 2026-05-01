using EmployeeSelection.UITests.PageObjects;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace EmployeeSelection.UITests.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private LoginWindowPage _loginPage;
        private string? _lastRegisteredUsername;
        private string? _lastRegisteredPassword;

        public LoginSteps()
        {
            _loginPage = TestHooks.LoginPage;
        }



        [Given(@"пользователь не авторизован")]
        public void GivenUserNotAuthorized()
        {
            _loginPage.IsDisplayed().Should().BeTrue("окно входа должно быть открыто");
        }

        [Given(@"пользователь зарегистрирован с логином ""(.*)"" и паролем ""(.*)""")]
        public void GivenUserRegistered(string username, string password)
        {
            _lastRegisteredUsername = username;
            _lastRegisteredPassword = password;
            _loginPage.RegisterAs(username, password);
            string error = _loginPage.GetErrorMessage();
            error.Should().BeNullOrEmpty("регистрация должна пройти успешно");
        }

        [Given(@"пользователь снова не авторизован")]
        public void GivenUserNotAuthorizedAgain()
        {
            _loginPage.Dispose();

            string exePath = @"D:\учёба\8 семестр\покпо\EmployeeSelection.UI\bin\Debug\net8.0-windows\EmployeeSelection.UI.exe";
            _loginPage = new LoginWindowPage(exePath);
        }


        [When(@"пользователь вводит логин ""(.*)"" и пароль ""(.*)""")]
        public void WhenUserEntersUsernameAndPassword(string username, string password)
        {
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
        }

        [When(@"нажимает кнопку ""(.*)""")]
        public void WhenНажимаетКнопку(string buttonName)
        {
            switch (buttonName.ToLower())
            {
                case "войти":
                case "вход":
                case "login":
                    _loginPage.ClickLogin();
                    break;
                case "зарегистрироваться":
                case "регистрация":
                case "register":
                    _loginPage.ClickRegister();
                    break;
                default:
                    throw new Exception($"Неизвестная кнопка: {buttonName}");
            }
        }

        [When(@"пользователь пытается войти с логином ""(.*)"" и паролем ""(.*)""")]
        public void WhenUserAttemptsToLogin(string username, string password)
        {
            _loginPage.LoginAs(username, password);
        }
        [Then(@"система выполняет вход")]
        public void ThenSystemLogsIn()
        {
            string error = _loginPage.GetErrorMessage();
            error.Should().BeNullOrEmpty($"Вход не выполнен. Ошибка: {error}");
        }

        [Then(@"главное окно становится доступным")]
        public void ThenMainWindowIsAvailable()
        {
            string error = _loginPage.GetErrorMessage();
            error.Should().BeNullOrEmpty("при успешном входе не должно быть ошибок");
        }

        [Then(@"система показывает сообщение ""(.*)""")]
        public void ThenSystemShowsMessage(string expectedMessage)
        {
            System.Threading.Thread.Sleep(500);

            string actualMessage = _loginPage.GetErrorMessage();
            actualMessage.Should().Contain(expectedMessage,
                $"Ожидалось сообщение '{expectedMessage}', но получили '{actualMessage}'");
        }

        [Then(@"система не должна показывать сообщение об ошибке")]
        public void ThenSystemShouldNotShowError()
        {
            string error = _loginPage.GetErrorMessage();
            error.Should().BeNullOrEmpty("не должно быть сообщения об ошибке");
        }

        [Then(@"вход не должен быть выполнен")]
        public void ThenLoginShouldNotBeSuccessful()
        {
            System.Threading.Thread.Sleep(1000);
            string errorMessage = _loginPage.GetErrorMessage();
            Console.WriteLine($"Сообщение об ошибке: '{errorMessage}'");
            if (string.IsNullOrEmpty(errorMessage))
            {
                _loginPage.IsDisplayed().Should().BeTrue("окно входа должно оставаться открытым");
                return;
            }
            errorMessage.Should().NotBeNullOrEmpty("должно появиться сообщение об ошибке");
        }
    }
}