using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

#region Goal Classes

enum Recurrence
{
    Daily,
    Weekly
}

class EventRecord
{

    public DateTime Timestamp { get; set; }

    public string GoalName { get; set; }

    public string EventDescription { get; set; }

    public int PointsEarned { get; set; }

    public int TotalPoints { get; set; }

    public EventRecord() { }

    public EventRecord(DateTime timestamp, string goalName, string eventDescription, int pointsEarned, int totalPoints)
    {
        Timestamp = timestamp;
        GoalName = goalName;
        EventDescription = eventDescription;
        PointsEarned = pointsEarned;
        TotalPoints = totalPoints;
    }
}

abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public string Name => _name;
    public string Description => _description;
    public int BasePoints => _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();

    public abstract bool IsComplete { get; }

    public abstract string GetStatus();

    public abstract string GetSaveString();
}

class SimpleGoal : Goal
{
    private bool _completed = false;

    public override bool IsComplete => _completed;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!_completed)
        {
            _completed = true;
            return BasePoints;
        }
        return 0;
    }

    public override string GetStatus() => _completed ? "[X]" : "[ ]";

    public override string GetSaveString()
    {
        return $"Simple|{Name}|{Description}|{BasePoints}|{_completed}";
    }

    public static SimpleGoal FromString(string[] parts)
    {
        var g = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
        if (bool.Parse(parts[4]))
            g.RecordEvent();
        return g;
    }
}

class EternalGoal : Goal
{
    public override bool IsComplete => false;

    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => BasePoints;

    public override string GetStatus() => "[∞]";

    public override string GetSaveString()
    {
        return $"Eternal|{Name}|{Description}|{BasePoints}";
    }

    public static EternalGoal FromString(string[] parts)
    {
        return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
    }
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount = 0;
    private int _bonusPoints;

    public Recurrence Period { get; }

    public int TargetCount => _targetCount;
    public int BonusPoints => _bonusPoints;
    public Recurrence PeriodType => Period;

    public override bool IsComplete => _currentCount >= _targetCount;

    public ChecklistGoal(string name,
                         string description,
                         int pointsPer,
                         int targetCount,
                         int bonus,
                         Recurrence period)
        : base(name, description, pointsPer)
    {
        _targetCount = targetCount;
        _bonusPoints = bonus;
        Period = period;
    }

    public override int RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            return _currentCount == _targetCount
                ? BasePoints + _bonusPoints
                : BasePoints;
        }
        return 0;
    }

    public override string GetStatus() => $"[{_currentCount}/{_targetCount}]";

    public override string GetSaveString() =>
        $"Checklist|{Name}|{Description}|{BasePoints}|{_currentCount}|{_targetCount}|{_bonusPoints}|{Period}";

    public void RecalculateCount(IEnumerable<EventRecord> allEvents)
    {
        DateTime periodStart;
        var now = DateTime.Now;

        if (Period == Recurrence.Daily)
        {
            periodStart = now.Date;
        }
        else
        {
            int diff = (int)now.DayOfWeek - (int)DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            periodStart = now.Date.AddDays(-diff);
        }

        _currentCount = allEvents
            .Count(e => e.GoalName.Equals(Name, StringComparison.OrdinalIgnoreCase)
                     && e.Timestamp >= periodStart);
    }

    public static ChecklistGoal FromCsv(string[] parts)
    {
        var name = parts[1];
        var desc = parts[2];
        var pointsPer = int.Parse(parts[3]);
        var targetCount = int.Parse(parts[4]);
        var bonus = int.Parse(parts[5]);
        var period = Enum.Parse<Recurrence>(parts[6], ignoreCase: true);

        return new ChecklistGoal(name, desc, pointsPer, targetCount, bonus, period);
    }
}

#endregion

class Program
{
    static string GoalsFile = "goals.csv";
    static string EventsFile = "events.csv";
    static List<Goal> _goals = new List<Goal>();
    static List<EventRecord> _eventLog = new List<EventRecord>();
    static int _score = 0;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nEternal Quest - Main Menu");
            Console.WriteLine("1. Create new goal");
            Console.WriteLine("2. Record an event");
            Console.WriteLine("3. Display goals");
            Console.WriteLine("4. Display score & level");
            Console.WriteLine("5. Save goals");
            Console.WriteLine("6. Load goals");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            switch (Console.ReadLine())
            {
                case "1": CreateNewGoal(); break;
                case "2": RecordEvent(); break;
                case "3": DisplayGoals(); break;
                case "4": DisplayScoreAndLevel(); break;
                case "5": SaveGoals(); break;
                case "6": LoadGoals(); break;
                case "7": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void CreateNewGoal()
    {
        Console.WriteLine("\nSelect goal type:");
        Console.WriteLine("1. Simple goal");
        Console.WriteLine("2. Eternal goal");
        Console.WriteLine("3. Checklist goal");
        Console.Write("Choice: ");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        int pts = ReadInt("Points for each completion: ");

        switch (type)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, desc, pts));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, desc, pts));
                break;
            case "3":
                int target = ReadInt("How many times to complete? ");
                int bonus = ReadInt("Bonus on final completion: ");
                Console.Write("Period (D)aily or (W)eekly? ");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                var period = (key == ConsoleKey.W)
                    ? Recurrence.Weekly
                    : Recurrence.Daily;
                _goals.Add(new ChecklistGoal(
                    name, desc, pts,
                    target, bonus,
                    period
                ));
                break;
            default:
                Console.WriteLine("Unknown type.");
                return;
        }

        Console.WriteLine("Goal created!");
    }

    static string Escape(string s)
    {
        if (s.Contains(",") || s.Contains("\""))
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        return s;
    }

    static string[] SplitCsv(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var cur = new System.Text.StringBuilder();
        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }
            if (c == ',' && !inQuotes)
            {
                result.Add(cur.ToString());
                cur.Clear();
            }
            else
            {
                cur.Append(c);
            }
        }
        result.Add(cur.ToString());
        return result.ToArray();
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var n))
                return n;
            Console.WriteLine("Please enter a valid number.");
        }
    }

    static void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals in memory—please load your goals first.");
            return;
        }

        Console.WriteLine("\nWhich goal did you complete? (type its exact name)");
        foreach (var g in _goals)
            Console.WriteLine($" • {g.Name}   {g.GetStatus()}");

        Console.Write("Goal name: ");
        string choice = Console.ReadLine() ?? "";
        var goal = _goals
            .FirstOrDefault(g => g.Name.Equals(choice, StringComparison.OrdinalIgnoreCase));

        if (goal == null)
        {
            Console.WriteLine("No matching goal found.");
            return;
        }

        if (goal is SimpleGoal && goal.IsComplete)
        {
            Console.WriteLine("That simple goal is already completed.");
            return;
        }

        Console.Write("Describe the event: ");
        string evDesc = Console.ReadLine() ?? "";

        int earned = goal.RecordEvent();
        if (earned == 0)
        {
            Console.WriteLine("No points awarded.");
            return;
        }

        _score += earned;

        var rec = new EventRecord
        {
            Timestamp = DateTime.Now,
            GoalName = goal.Name,
            EventDescription = evDesc,
            PointsEarned = earned,
            TotalPoints = _score
        };
        _eventLog.Add(rec);

        using var writer = new StreamWriter(EventsFile, append: true);
        if (new FileInfo(EventsFile).Length == 0)
            writer.WriteLine("Timestamp,GoalName,EventDescription,PointsEarned,TotalPoints");

        writer.WriteLine($"{rec.Timestamp:o},{Escape(rec.GoalName)},{Escape(rec.EventDescription)},{rec.PointsEarned},{rec.TotalPoints}");

        Console.WriteLine($"Event recorded! You earned {earned} points (Total: {_score}).");
    }

    static void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        if (_goals.Count == 0)
        {
            Console.WriteLine(" (none)");
            return;
        }
        foreach (var g in _goals)
        {
            Console.WriteLine($"{g.GetStatus()} {g.Name} – {g.Description}");
        }
    }

    static void DisplayScoreAndLevel()
    {
        int level = (_score / 1000) + 1;  // Creative feature: level up every 1000 pts
        Console.WriteLine($"\nTotal Score: {_score}   Level: {level}");
    }

    static void SaveGoals()
    {
        Console.Write("Filename to save goal definitions to (e.g. goals.csv): ");
        var file = Console.ReadLine() ?? GoalsFile;

        using var writer = new StreamWriter(file);
        writer.WriteLine("Type,Name,Description,Points,TargetCount,BonusPoints,Period");
        foreach (var g in _goals)
        {
            if (g is SimpleGoal sg)
            {
                writer.WriteLine($"Simple,{Escape(sg.Name)},{Escape(sg.Description)},{sg.BasePoints},,,");
            }
            else if (g is EternalGoal eg)
            {
                writer.WriteLine($"Eternal,{Escape(eg.Name)},{Escape(eg.Description)},{eg.BasePoints},,,");
            }
            else if (g is ChecklistGoal cg)
            {
                writer.WriteLine($"Checklist," + $"{Escape(cg.Name)}," + $"{Escape(cg.Description)}," + $"{cg.BasePoints}," + $"{cg.TargetCount}," + $"{cg.BonusPoints}," + $"{cg.PeriodType}");
            }
        }

        Console.WriteLine($"Saved {_goals.Count} goals to {file}");
    }

    static void LoadEvents()
    {
        if (!File.Exists(EventsFile))
        {
            Console.WriteLine("No past events to load.");
            return;
        }

        var lines = File.ReadAllLines(EventsFile);
        if (lines.Length < 2)
        {
            Console.WriteLine("No event data found in CSV.");
            return;
        }

        _eventLog.Clear();
        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = SplitCsv(line);

            var rec = new EventRecord
            {
                Timestamp = DateTime.Parse(parts[0]),
                GoalName = parts[1],
                EventDescription = parts[2],
                PointsEarned = int.Parse(parts[3]),
                TotalPoints = int.Parse(parts[4])
            };
            _eventLog.Add(rec);
        }

        foreach (var rec in _eventLog)
        {
            var goal = _goals
                .FirstOrDefault(g =>
                    g.Name.Equals(rec.GoalName, StringComparison.OrdinalIgnoreCase));
            if (goal is SimpleGoal sg)
            {
                sg.RecordEvent();
            }
        }
        foreach (var cg in _goals.OfType<ChecklistGoal>())
        {
            cg.RecalculateCount(_eventLog);
        }

        _score = _eventLog.Count > 0
            ? _eventLog[^1].TotalPoints
            : 0;

        Console.WriteLine($"Loaded {_eventLog.Count} event(s). Current score: {_score}");
    }

    static void LoadGoals()
    {
        Console.Write("Filename to load goal definitions from (e.g. goals.csv): ");
        var file = Console.ReadLine() ?? GoalsFile;
        if (!File.Exists(file))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _goals.Clear();
        foreach (var line in File.ReadAllLines(file).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = SplitCsv(line);
            var type = parts[0];
            var name = parts[1];
            var desc = parts[2];
            var pts = int.Parse(parts[3]);

            switch (type)
            {
                case "Simple":
                    _goals.Add(new SimpleGoal(name, desc, pts));
                    break;
                case "Eternal":
                    _goals.Add(new EternalGoal(name, desc, pts));
                    break;
                case "Checklist":
                    int target = int.Parse(parts[4]);
                    int bonus = int.Parse(parts[5]);
                    _goals.Add(ChecklistGoal.FromCsv(parts));
                    break;
            }
        }

        Console.WriteLine($"Loaded {_goals.Count} goals from {file}");

        LoadEvents();
    }
}