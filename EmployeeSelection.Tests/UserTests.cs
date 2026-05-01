using EmployeeSelection.Core.Models;

public class UserTests
{
    [Fact]
    public void PasswordHash_ShouldNotEqualPassword()
    {
        var user = new User(1, "admin", "123456", "HR");

        Assert.NotEqual("123456", user.PasswordHash);
    }

    [Fact]
    public void ValidatePassword_ShouldReturnTrue()
    {
        var user = new User(1, "admin", "123456", "HR");

        Assert.True(user.ValidatePassword("123456"));
    }
}