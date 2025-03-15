class NegativeGoal : Goal
{
    public NegativeGoal(string description, int points) : base(description, points) { }

    public override void RecordGoal()
    {
        IsDone = true;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[{(IsDone ? "X" : " ")}] {GoalDescription} (Negative Goal: Loses {PointValue} points)");
    }
}