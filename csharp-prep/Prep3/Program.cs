using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep3 World!");
        Console.Write("What is the magic number?");
        string number1 = Console.ReadLine();
        int number = int.Parse(number1);
        
        int guess= 0;
        
        
       
        while (guess != number)
        {
        Console.Write("What is your Guess? ");
        string guessinput= Console.ReadLine();
        int guess1 = int.Parse(guessinput);
            
            
                if (guess1 < number)

                {
                    Console.WriteLine("Higher");
                }
                else if (guess1 > number)
                {
                    Console.WriteLine("Lower");
                
                }
                else 
                {
                    Console.WriteLine("you guessed it right");
                }


        }
    }
}