using System;
using System.IO;

abstract class Goal
{
    public bool IsDone { get; protected set; }
    public int PointValue { get; protected set; }
    public string GoalDescription { get; protected set; }

    public Goal(string description, int points)
    {
        GoalDescription = description;
        PointValue = points;
        IsDone = false;
    }

    public abstract void RecordGoal();
    public abstract void DisplayGoal();

    public virtual void SaveGoal(StreamWriter writer)
    {
        writer.WriteLine($"{GetType().Name}|{GoalDescription}|{PointValue}|{IsDone}");
    }

    public static Goal LoadGoal(StreamReader reader)
    {
        string[] parts = reader.ReadLine().Split('|');
        string type = parts[0];
        string description = parts[1];
        int points = int.Parse(parts[2]);
        bool isDone = bool.Parse(parts[3]);

        switch (type)
        {
            case nameof(SimpleGoal):
                return new SimpleGoal(description, points) { IsDone = isDone };
            case nameof(EternalGoal):
                return new EternalGoal(description, points);
            case nameof(ChecklistGoal):
                return new ChecklistGoal(description, points, int.Parse(parts[4]), int.Parse(parts[5])) { IsDone = isDone };
            case nameof(NegativeGoal):
                return new NegativeGoal(description, points) { IsDone = isDone };
            default:
                throw new InvalidOperationException("Unknown goal type");
        }
    }
}