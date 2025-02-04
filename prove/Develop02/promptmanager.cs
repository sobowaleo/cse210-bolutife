class PromptManager
{
    private List<string> prompts;

    public PromptManager()
    {
        prompts = new List<string>
        {
            "What new things did you like, and what new things did you try and dislike?",
            "What was the best part of my day?",
            "Why do you think you won't try new things?",
            "What was the strongest emotion I felt today?",
            "Who influenced you to finally try the new thing?"
        };
    }

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}