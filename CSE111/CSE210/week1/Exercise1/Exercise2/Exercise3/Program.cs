using System;

class Program
{
    static void Main(string[] args)
    {
        var rng = new Random();
        int magic = rng.Next(1, 101);

        Console.WriteLine("A random number between 1 and 100 has been chosen.");

        Console.Write("What is your guess? ");
        int guess = int.Parse(Console.ReadLine());

        while (guess != magic)
        {
            if (guess < magic)
                Console.WriteLine("Higher");
            else
                Console.WriteLine("Lower");

            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("You guessed it!");
    }
}