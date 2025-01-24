using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep2 World!");
        Console.WriteLine("Hello Prep2 World!");
        Console.WriteLine("This is in C#");
        Console.Write("Enter number:");
        String valuefromuser = Console.ReadLine();



        int x = int.Parse(valuefromuser);
        int y = 2;
        if (x > y )
        {
            Console.WriteLine("greater");
        }
        else if (x < y)
        {
            Console.WriteLine("Equal");
        }
        
        Console.WriteLine("Please enter Your grade");
        string value = Console.ReadLine();
        int value1 = int.Parse(value);

        if (value1 >=  90)
        {
            Console.WriteLine ("Your Grade is A");
        }
        else if (value1 >= 80)
        {
            Console.WriteLine (" Your Grade is B");
        }
        else if (value1 >= 70)
        {
            Console.WriteLine("Your Grade is C");
        }
        else if (value1 >=60)
        {
            Console.WriteLine("Your Grade is D");
        }

        else if  (value1 < 60)
        {
            Console.WriteLine ("Your Grade is F");
        }

    }
}