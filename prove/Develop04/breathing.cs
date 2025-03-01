namespace MindfulnessApp
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public override void Run()
        {
            Start();
            int interval = 5; // 5 seconds for each breath in/out
            int cycles = Duration / (interval * 2);

            for (int i = 0; i < cycles; i++)
            {
                Console.WriteLine("Breathe in...");
                ShowCountdown(interval);
                Console.WriteLine("Breathe out...");
                ShowCountdown(interval);
            }
            End();
        }
    }
}