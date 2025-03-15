using System;
using System.Collections.Generic;
using System.IO;

class GoalTracker
{
    private List<Goal> goals = new List<Goal>();
    public int Score { get; private set; }
    public int Level { get; private set; } = 1; // Creativity: Leveling System

    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            goal.DisplayGoal();
        }
    }

    public void RecordGoals()
    {
        foreach (var goal in goals)
        {
            goal.RecordGoal();
            if (goal.IsDone)
            {
                Score += goal.PointValue;
                if (Score >= Level * 1000) // Level up every 1000 points
                {
                    Level++;
                    Console.WriteLine($"Congratulations! You leveled up to Level {Level}!");
                }
            }
        }
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(Score);
            writer.WriteLine(Level);
            foreach (var goal in goals)
            {
                goal.SaveGoal(writer);
            }
        }
    }

    public void LoadGoals(string filename)
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            Score = int.Parse(reader.ReadLine());
            Level = int.Parse(reader.ReadLine());
            while (!reader.EndOfStream)
            {
                goals.Add(Goal.LoadGoal(reader));
            }
        }
    }
}