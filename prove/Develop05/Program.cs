using System;

class Program
{
    static void Main(string[] args)
    {
        GoalTracker tracker = new GoalTracker();

        while (true)
        {
            Console.WriteLine($"You have {tracker.Score} points. Level: {tracker.Level}");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Which type of goal would you like to create?");
                    Console.WriteLine("1. Simple Goal");
                    Console.WriteLine("2. Eternal Goal");
                    Console.WriteLine("3. Checklist Goal");
                    Console.WriteLine("4. Negative Goal");
                    string goalType = Console.ReadLine();
                    Console.Write("What is the name of your goal? ");
                    string name = Console.ReadLine();
                    Console.Write("What is a short description of it? ");
                    string description = Console.ReadLine();
                    Console.Write("What is the amount of points associated with this goal? ");
                    int points = int.Parse(Console.ReadLine());

                    switch (goalType)
                    {
                        case "1":
                            tracker.AddGoal(new SimpleGoal(description, points));
                            break;
                        case "2":
                            tracker.AddGoal(new EternalGoal(description, points));
                            break;
                        case "3":
                            Console.Write("How many times must this goal be accomplished? ");
                            int target = int.Parse(Console.ReadLine());
                            Console.Write("What is the bonus points for completing it? ");
                            int bonus = int.Parse(Console.ReadLine());
                            tracker.AddGoal(new ChecklistGoal(description, points, target, bonus));
                            break;
                        case "4":
                            tracker.AddGoal(new NegativeGoal(description, points));
                            break;
                        default:
                            Console.WriteLine("Invalid goal type.");
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("The goals are:");
                    tracker.DisplayGoals();
                    break;
                case "3":
                    Console.Write("What is the filename for the goal file? ");
                    string saveFile = Console.ReadLine();
                    tracker.SaveGoals(saveFile);
                    break;
                case "4":
                    Console.Write("What is the filename for the goal file? ");
                    string loadFile = Console.ReadLine();
                    tracker.LoadGoals(loadFile);
                    break;
                case "5":
                    tracker.RecordGoals();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}