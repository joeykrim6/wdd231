
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        if (!double.TryParse(Console.ReadLine(), out double score))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return;
        }

        if (score >= 90)
            Console.WriteLine("Your letter grade is A");
        else if (score >= 80)
            Console.WriteLine("Your letter grade is B");
        else if (score >= 70)
            Console.WriteLine("Your letter grade is C");
        else if (score >= 60)
            Console.WriteLine("Your letter grade is D");
        else
            Console.WriteLine("Your letter grade is F");

        if (score >= 70)
            Console.WriteLine("Congratulations! You passed the course.");
        else
            Console.WriteLine("Some more studying and you go this!");
    }
}
