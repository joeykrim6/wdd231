using System;

namespace FitnessTrackerApp
{
    class Running : Activity
    {
        private double _distance;

        public Running(DateTime date, int length, double distance)
            : base(date, length)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;

        public override double GetSpeed()
        {
            double hours = Length / 60.0;
            return _distance / hours;
        }

        public override double GetPace()
        {
            return Length / _distance;
        }
    }
}