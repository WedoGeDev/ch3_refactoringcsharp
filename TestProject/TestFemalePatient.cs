using Refactoring_CaloriesCalculator;

namespace TestProject;

[TestFixture]
public class TestFemalePatient
{
    [Test]
    public void TestIdealBodyWeight()
    {
        var femalePatient = new FemalePatient();
        femalePatient.HeightInInches = 72;
        femalePatient.WeightInPounds = 110;
        femalePatient.Age = 30;

        double expectedResult = 161.15626;
        double realResult = femalePatient.IdealBodyWeight();

        Assert.AreEqual(expectedResult, realResult);
    }

    [Test]
    public void TestDailyCaloriesRecomended()
    {
        var femalePatient = new FemalePatient();
        femalePatient.HeightInInches = 72;
        femalePatient.WeightInPounds = 110;
        femalePatient.Age = 30;

        double expectedResult = 1325.4;
        double realResult = femalePatient.DailyCaloriesRecommended();

        Assert.AreEqual(expectedResult, realResult);
    }

}