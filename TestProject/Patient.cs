using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_CaloriesCalculator
{
    public abstract class Patient
    {
        private double _heightInInches;
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double HeightInInches
        {
            get => _heightInInches;
            set
            {
                if (value <= 60)
                    throw new ArgumentOutOfRangeException("Heigth has to be greater than five feet (60 inches).");
                _heightInInches = value;
            }
        }

        public double WeightInPounds { get; set; }
        public double Age { get; set; }

        public abstract double DailyCaloriesRecommended();
        public abstract double IdealBodyWeight();
        
        public double DistanceFromIdealWeight()
        {
            return WeightInPounds - IdealBodyWeight();
        }
    }
}
