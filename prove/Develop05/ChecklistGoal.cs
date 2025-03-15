class ChecklistGoal : Goal
{
    public int TargetNum { get; private set; }
    public int CurrentProgress { get; private set; }
    public int BrushPoints { get; private set; }

    public ChecklistGoal(string description, int points, int targetNum, int brushPoints) : base(description, points)
    {
        TargetNum = targetNum;
        BrushPoints = brushPoints;
    }

    public override void RecordGoal()
    {
        CurrentProgress++;
        if (CurrentProgress >= TargetNum)
        {
            IsDone = true;
            PointValue += BrushPoints;
        }
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[{(IsDone ? "X" : " ")}] {GoalDescription} Completed {CurrentProgress}/{TargetNum} times");
    }

    public override void SaveGoal(StreamWriter writer)
    {
        base.SaveGoal(writer);
        writer.WriteLine($"{TargetNum}|{CurrentProgress}|{BrushPoints}");
    }
}