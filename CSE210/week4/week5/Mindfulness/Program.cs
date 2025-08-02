using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

abstract class Activity
{
    protected string Name;
    protected string Description;
    protected int DurationSeconds;

    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Run()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Starting {Name}");
        Console.WriteLine();
        Console.WriteLine(Description);
        Console.WriteLine();
        Console.Write("Enter duration in seconds: ");
        while (!int.TryParse(Console.ReadLine(), out DurationSeconds) || DurationSeconds <= 0)
        {
            Console.Write("Please enter a positive integer: ");
        }
        Console.WriteLine("Get ready...");
        PauseWithSpinner(3);
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        Console.WriteLine($"You have completed the {Name} for {DurationSeconds} seconds.");
        PauseWithSpinner(3);
    }

    protected void PauseWithSpinner(int seconds)
    {
        var spinner = new[] { '|', '/', '-', '\\' };
        int idx = 0;
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < seconds)
        {
            Console.Write(spinner[idx]);
            Thread.Sleep(250);
            Console.Write("\b");
            idx = (idx + 1) % spinner.Length;
        }
        Console.WriteLine();
    }

    protected abstract void PerformActivity();
}
class BreathingActivity : Activity
{
    private const int DefaultInterval = 4;

    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through deep breathing. Clear your mind and focus on your breath.")
    {
    }

    protected override void PerformActivity()
    {
        int interval = DurationSeconds < DefaultInterval * 2
            ? Math.Max(1, DurationSeconds / 2)
            : DefaultInterval;

        var sw = Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < DurationSeconds)
        {
            Console.Write("Breathe in...");
            Console.WriteLine();
            PauseCountdown(interval);
            Console.WriteLine();

            Console.Write("Breathe out...");
            Console.WriteLine();
            PauseCountdown(interval);
            Console.WriteLine();
        }
    }

    private void PauseCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($" {i}");
            Thread.Sleep(1000);
            Console.Write("\r");      
            Console.Write(new string(' ', i.ToString().Length + 1)); 
            Console.Write("\r");   
        }
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    protected override void PerformActivity()
    {
        var rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        PauseWithSpinner(3);

        var sw = Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < DurationSeconds)
        {
            Console.WriteLine(questions[rand.Next(questions.Count)]);
            PauseWithSpinner(5);
        }
    }
}

class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by listing as many things as you can.")
    {
    }

    protected override void PerformActivity()
    {
        var rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        Console.WriteLine("You will have 5 seconds to think...");
        PauseWithSpinner(5);
        Console.WriteLine("Start listing items. Press Enter after each.");

        var items = new List<string>();
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < DurationSeconds)
        {
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                items.Add(input);
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            Activity activity = null;
            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    continue;
            }

            activity.Run();
            Console.WriteLine("Press any key to return to menu.");
            Console.ReadKey();
        }
    }
}