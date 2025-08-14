using System;

namespace FitnessTrackerApp
{
    abstract class Activity
    {
        private DateTime _date;
        private int _length;

        public DateTime Date  => _date;
        public int      Length => _length;

        protected Activity(DateTime date, int length)
        {
            _date   = date;
            _length = length;
        }

        public abstract double GetDistance();

        public abstract double GetSpeed();

        public abstract double GetPace();

        public virtual string GetSummary()
        {
            string dateStr  = _date.ToString("dd MMM yyyy");
            string typeName = GetType().Name.Replace("Activity", "");
            return $"{dateStr} {typeName} ({_length} min): " +
                   $"Distance {GetDistance():F1} miles, " +
                   $"Speed {GetSpeed():F1} mph, " +
                   $"Pace: {GetPace():F1} min per mile";
        }
    }
}