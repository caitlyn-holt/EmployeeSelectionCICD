using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Threading;

#pragma warning disable CS8600, CS8602, CS8603, CS8618

namespace EmployeeSelection.UITests.PageObjects
{
    public class LoginWindowPage : IDisposable
    {
        private Application _app;
        private UIA3Automation _automation;
        private Window _window;

        public LoginWindowPage(string appPath)
        {
            _app = Application.Launch(appPath);
            Thread.Sleep(3000);

            _automation = new UIA3Automation();
            _window = _app.GetMainWindow(_automation);
            Thread.Sleep(2000);
        }

        private TextBox UsernameTextBox =>
            _window.FindFirstDescendant(cf => cf.ByAutomationId("txtUsername"))?.AsTextBox();

        private TextBox PasswordBoxElement =>
            _window.FindFirstDescendant(cf => cf.ByAutomationId("txtPassword"))?.AsTextBox();

        private Button LoginButton =>
            _window.FindFirstDescendant(cf => cf.ByAutomationId("btnLogin"))?.AsButton();

        private Button RegisterButton =>
            _window.FindFirstDescendant(cf => cf.ByAutomationId("btnRegister"))?.AsButton();

        private Label ErrorLabel =>
            _window.FindFirstDescendant(cf => cf.ByAutomationId("lblError"))?.AsLabel();

        public void EnterUsername(string username)
        {
            var textBox = UsernameTextBox;
            if (textBox != null)
            {
                textBox.Click();
                Thread.Sleep(100);
                textBox.Text = username;
            }
        }

        public void EnterPassword(string password)
        {
            var passwordBox = PasswordBoxElement;
            if (passwordBox != null)
            {
                passwordBox.Click();
                Thread.Sleep(100);
                passwordBox.Text = password;
            }
        }

        public void ClickLogin()
        {
            LoginButton?.Click();
            Thread.Sleep(1000);
        }

        public void ClickRegister()
        {
            RegisterButton?.Click();
            Thread.Sleep(1000);
        }

        public string GetErrorMessage()
        {
            try
            {
                Thread.Sleep(500);
                var errorLabel = _window.FindFirstDescendant(cf => cf.ByAutomationId("lblError"));

                if (errorLabel != null)
                {
                    return errorLabel.AsLabel()?.Text ?? string.Empty;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void LoginAs(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public void RegisterAs(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickRegister();
        }

        public bool IsDisplayed()
        {
            try
            {
                return UsernameTextBox != null;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            try
            {
                _app?.Close();
                _automation?.Dispose();
            }
            catch
            {
                
            }
        }
        public void ClearFields()
        {
            try
            {
                if (UsernameTextBox != null)
                {
                    UsernameTextBox.Click();
                    UsernameTextBox.Text = "";
                }

                if (PasswordBoxElement != null)
                {
                    PasswordBoxElement.Click();
                    PasswordBoxElement.Text = "";
                }

                Thread.Sleep(300);
            }
            catch
            {
                
            }
        }
        public bool AreFieldsCleared()
        {
            // Проверяем, что поля пустые после успешного входа
            string username = UsernameTextBox?.Text ?? "";
            string password = PasswordBoxElement?.Text ?? "";
            return string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password);
        }

        public bool HasErrorMessage()
        {
            try
            {
                // Ищем элемент с сообщением об ошибке
                var errorLabel = _window.FindFirstDescendant(cf => cf.ByAutomationId("lblError"));

                if (errorLabel != null)
                {
                    string text = errorLabel.AsLabel()?.Text ?? "";
                    bool hasText = !string.IsNullOrEmpty(text);

                    if (hasText)
                    {
                        Console.WriteLine($"Найдено сообщение: '{text}'");
                    }

                    return hasText;
                }
                var allElements = _window.FindAllDescendants();
                foreach (var element in allElements)
                {
                    try
                    {
                        var label = element.AsLabel();
                        if (label != null && label.SetForeground != null)
                        {
                            string text = label.Text ?? "";
                            if (text.Length > 0)
                            {
                                Console.WriteLine($"Найден Label с текстом: '{text}'");
                                return true;
                            }
                        }
                    }
                    catch { }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}