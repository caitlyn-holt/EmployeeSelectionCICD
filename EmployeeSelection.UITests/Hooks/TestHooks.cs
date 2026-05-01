using EmployeeSelection.UITests.PageObjects;
using System;
using TechTalk.SpecFlow;

[Binding]
public class TestHooks
{
    private static LoginWindowPage _loginPage;

    [BeforeScenario]
    public static void BeforeScenario()
    {
        string exePath = @"D:\учёба\8 семестр\покпо\EmployeeSelection.UI\bin\Debug\net8.0-windows\EmployeeSelection.UI.exe";
        _loginPage = new LoginWindowPage(exePath);
    }

    [AfterScenario]
    public static void AfterScenario()
    {
        try
        {
            _loginPage?.Dispose();

            var processes = System.Diagnostics.Process.GetProcessesByName("EmployeeSelection.UI");
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(1000);
                }
                catch { }
            }
        }
        catch { }
        finally
        {
            _loginPage = null;
        }
    }

    public static LoginWindowPage LoginPage => _loginPage ?? throw new InvalidOperationException("LoginPage не инициализирован");
}