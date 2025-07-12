
using System;

namespace JournalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal");
                Console.WriteLine("4. Load the journal");
                Console.WriteLine("5. Quit");
                Console.Write("Choose an option (1-5): ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        var prompt = PromptGenerator.GetRandomPrompt();
                        Console.WriteLine($"\nPrompt: {prompt}");
                        Console.Write("Your response: ");
                        var response = Console.ReadLine();
                        var date = DateTime.Now.ToShortDateString();
                        journal.AddEntry(new Entry(date, prompt, response));
                        Console.WriteLine("Entry recorded.");
                        break;

                    case "2":
                        Console.WriteLine();
                        journal.Display();
                        break;

                    case "3":
                        Console.Write("Enter enter the name of the journal to save to: ");
                        var saveFile = Console.ReadLine();
                        journal.Save(saveFile);
                        break;

                    case "4":
                        Console.Write("Enter the name of the journal you want to view: ");
                        var loadFile = Console.ReadLine();
                        journal.Load(loadFile);
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose 1-5.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
