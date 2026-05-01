using EmployeeSelection.Core.Services;
using EmployeeSelection.Core.Candidates;
using EmployeeSelection.Core.Updaters;
using Xunit;
using System.IO;
using System.Text.Json;

namespace EmployeeSelection.Tests
{
    public class SystemTests
    {

        [Fact]
        public void System_RegisterAndLogin_Success()
        {
            var auth = new AuthenticationService();
            var user = auth.RegisterUser("test_user", "123456", "HR");

            Assert.NotNull(user);

            var loggedIn = auth.Login("test_user", "123456");
            Assert.Equal("test_user", loggedIn.Username);
        }

        [Fact]
        public void System_CandidateFiltering_Success()
        {
            var candidateService = new CandidateService();
            var path = "TestData/candidates.json";

            if (!File.Exists(path))
            {
                var testData = new[] {
                    new Candidate { Name = "Alex", Skill = "C#", Experience = 5 },
                    new Candidate { Name = "Anna", Skill = "Java", Experience = 3 }
                };
                File.WriteAllText(path, JsonSerializer.Serialize(testData));
            }

            var candidates = candidateService.LoadFromFile(path);
            var filtered = candidateService.FilterBySkill(candidates, "C#");

            Assert.NotEmpty(candidates);
            Assert.NotEmpty(filtered);
            Assert.All(filtered, c => Assert.Equal("C#", c.Skill));
        }

        [Fact]
        public void System_UserUpdater_FullCycle()
        {
            var auth = new AuthenticationService();
            var updater = new UserUpdater(auth);

            var user = updater.Update("manager", "pass123", "HR");
            Assert.Equal("HR", user.Role);
            var updated = updater.Update("manager", "pass123", "Admin");
            Assert.Equal("Admin", updated.Role);
        }


        [Fact]
        public void LoadTest_SimulateMultipleUsers()
        {
            int userCount = 1000;
            var auth = new AuthenticationService();
            var errors = 0;

            var startTime = DateTime.Now;

            for (int i = 0; i < userCount; i++)
            {
                try
                {
                    auth.RegisterUser($"load_user_{i}", "123456", "User");
                    auth.Login($"load_user_{i}", "123456");
                }
                catch
                {
                    errors++;
                }
            }

            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            Assert.Equal(0, errors);
            Assert.Equal(userCount, auth.GetUsers().Count);
            Assert.True(duration.TotalMilliseconds < 5000);
        }
    }
}
