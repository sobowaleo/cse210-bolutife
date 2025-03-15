class EternalGoal : Goal
{
    public EternalGoal(string description, int points) : base(description, points) { }

    public override void RecordGoal()
    {
        // Eternal goals are never done
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[ ] {GoalDescription}");
    }
}