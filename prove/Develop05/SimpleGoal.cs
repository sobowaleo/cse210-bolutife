class SimpleGoal : Goal
{
    public SimpleGoal(string description, int points) : base(description, points) { }

    public override void RecordGoal()
    {
        IsDone = true;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[{(IsDone ? "X" : " ")}] {GoalDescription}");
    }
}