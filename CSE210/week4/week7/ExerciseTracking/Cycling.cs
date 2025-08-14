using System;

namespace FitnessTrackerApp
{
    class Cycling : Activity
    {
        private double _speed;

        public Cycling(DateTime date, int length, double speed)
            : base(date, length)
        {
            _speed = speed;
        }

        public override double GetDistance()
        {
            double hours = Length / 60.0;
            return _speed * hours;
        }

        public override double GetSpeed() => _speed;

        public override double GetPace()
        {
            double dist = GetDistance();
            return Length / dist;
        }
    }
}