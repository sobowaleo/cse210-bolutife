using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep5 World!");
        
 DisplayWelcome();
        string userName = PromptUsername();
        int favorite = Promptnumber();
        int squaredNumber  = squareNumber(favorite);

        DisplayResult (userName,squaredNumber);

    }
    static void DisplayWelcome()
    {
        Console.Write("Welcome to the program ");
    }
    static string PromptUsername()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();

    }
    static int Promptnumber()
    {
        Console.Write("Please enter your favortie number: ");
        return int.Parse(Console.ReadLine()); 
    }
    static int squareNumber(int number)
    {
        return number *number;
    }
    static void DisplayResult(string userName, int squaredNumber)
    {
        Console.WriteLine($"Brother {userName}, the square of your number is {squaredNumber}");
    }
}

