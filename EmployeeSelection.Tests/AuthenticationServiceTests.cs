using EmployeeSelection.Core.Services;

public class AuthenticationServiceTests
{
    [Fact]
    public void Register_ShouldCreateUser()
    {
        var service = new AuthenticationService();

        var user = service.RegisterUser("alex", "123456", "HR");

        Assert.Equal("alex", user.Username);
    }

    [Fact]
    public void Login_ShouldReturnUser()
    {
        var service = new AuthenticationService();

        service.RegisterUser("alex", "123456", "HR");

        var user = service.Login("alex", "123456");

        Assert.NotNull(user);
    }
}

