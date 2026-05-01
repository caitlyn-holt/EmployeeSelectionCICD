using EmployeeSelection.Core.Services;
using EmployeeSelection.Core.Updaters;

public class UserUpdaterTests
{
    [Fact]
    public void Update_ShouldRegisterNewUser()
    {
        // Arrange
        var auth = new AuthenticationService();
        var updater = new UserUpdater(auth);

        // Act
        var user = updater.Update("alex", "123456", "HR");

        // Assert
        Assert.Equal("alex", user.Username);
        Assert.Equal("HR", user.Role);
    }

    [Fact]
    public void Update_ShouldChangeUserRole()
    {
        // Arrange
        var auth = new AuthenticationService();
        var updater = new UserUpdater(auth);

        updater.Update("alex", "123456", "HR");

        // Act
        var user = updater.Update("alex", "123456", "Admin");

        // Assert
        Assert.Equal("Admin", user.Role);
    }

    [Fact]
    public void Update_ShouldChangePassword()
    {
        // Arrange
        var auth = new AuthenticationService();
        var updater = new UserUpdater(auth);

        updater.Update("alex", "123456", "HR");

        // Act
        var user = updater.Update("alex", "654321", "HR");

        // Assert
        Assert.True(user.ValidatePassword("654321"));
    }
}