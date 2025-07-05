
using System;

class Program
{
    static void Main(string[] args)
    {
        var numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        while (true)
        {
            Console.Write("Enter number: ");
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
                break;
            numbers.Add(input);
        }

        int sum = 0;
        foreach (int n in numbers)
            sum += n;
        Console.WriteLine($"The sum is: {sum}");

        double average = sum / (double)numbers.Count;
        Console.WriteLine($"The average is: {average}");

        int max = numbers[0];
        foreach (int n in numbers)
            if (n > max) max = n;
        Console.WriteLine($"The largest number is: {max}");
    }
}
