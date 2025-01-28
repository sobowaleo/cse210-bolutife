using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep4 World!");
        
 // Initialize the list to store numbers
        List<int> value = new List<int>();
        int userinput;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect numbers from the user
        do
        {
            Console.Write("Enter number: ");
            userinput = int.Parse(Console.ReadLine());

            if (userinput != 0)
            {
                value.Add(userinput);
            }
        } while (userinput != 0);

        // Compute the sum of the numbers
        int sum = 0;
        foreach (int value1 in value)
        {
            sum += value1;
        }

        // Compute the average of the numbers
        double average = value.Count > 0 ? (double)sum / value.Count : 0;

        // Find the maximum number
        int max = int.MinValue;
        foreach (int number1 in value)
        {
            if (number1 > max)
            {
                max = number1;
            }
        }

        // Display results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");



    }
}