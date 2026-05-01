using EmployeeSelection.Core.Candidates;
using System.Text.Json;

public class IntegrationTests
{
    private readonly CandidateService _service = new();
    private readonly string _testFile = "candidates.json";

    public IntegrationTests()
    {
        if (!File.Exists(_testFile))
        {
            var testData = new[]
            {
                new Candidate { Name = "Alex", Skill = "C#", Experience = 5 },
                new Candidate { Name = "Anna", Skill = "Java", Experience = 3 },
                new Candidate { Name = "John", Skill = "C#", Experience = 4 },
                new Candidate { Name = "Kate", Skill = "Python", Experience = 2 }
            };
            File.WriteAllText(_testFile, JsonSerializer.Serialize(testData));
        }
    }

    // МЕТОД БОЛЬШОГО ВЗРЫВА 
    [Fact]
    public void BigBang_Test()
    {
        var candidates = _service.LoadFromFile(_testFile);
        var csharpDevs = _service.FilterBySkill(candidates, "C#");

        Assert.Equal(2, csharpDevs.Count);
        Assert.Contains(csharpDevs, c => c.Name == "Alex");
        Assert.Contains(csharpDevs, c => c.Name == "John");
    }

    // МЕТОД СНИЗУ ВВЕРХ
    [Fact]
    public void BottomUp_Test()
    {
        var candidates = _service.LoadFromFile(_testFile);
        Assert.Equal(4, candidates.Count);

        var csharpDevs = _service.FilterBySkill(candidates, "C#");
        Assert.Equal(2, csharpDevs.Count);
    }
}