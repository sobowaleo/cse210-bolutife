using System;
using System.Threading;

namespace MindfulnessApp
{
    public abstract class Activity
    {
        protected string Name { get; set; }
        protected string Description { get; set; }
        protected int Duration { get; set; }

        public Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        // Common starting message
        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {Name} Activity.");
            Console.WriteLine(Description);
            Console.Write("Enter the duration of the activity in seconds: ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Prepare to begin...");
            ShowSpinner(3);
        }

        // Common ending message
        public void End()
        {
            Console.WriteLine("Good job!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed the {Name} activity for {Duration} seconds.");
            ShowSpinner(3);
        }

        // Show a spinner animation
        protected void ShowSpinner(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Show a countdown animation
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Abstract method to be implemented by derived classes
        public abstract void Run();
    }
}