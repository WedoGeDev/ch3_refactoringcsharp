using Refactoring_CaloriesCalculator;
using NUnit.Framework;

namespace TestProject;

[TestFixture]
public class TestFemalePatient
{
    private FemalePatient femalePatient;

    [SetUp]
    public void CreateFemalePatientInstance()
    {
        femalePatient = new FemalePatient();

        femalePatient.HeightInInches = 72;
        femalePatient.WeightInPounds = 110;
        femalePatient.Age = 30;
    }

    [Test]
    public void TestIdealBodyWeight()
    {
        double expectedResult = 161.15626;
        double realResult = femalePatient.IdealBodyWeight();

        Assert.AreEqual(expectedResult, realResult);
    }

    [Test]
    public void TestDailyCaloriesRecomended()
    {
        double expectedResult = 1325.4;
        double realResult = femalePatient.DailyCaloriesRecommended();

        Assert.AreEqual(expectedResult, realResult);
    }

    [Test]
    public void HeigthLessThan5Ft()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => femalePatient.HeightInInches = 59);
    }
}