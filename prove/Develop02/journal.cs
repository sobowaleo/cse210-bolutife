class Journal
{
    public List<Entry> Entries { get; private set; }
    public bool Changed { get; private set; }

    public Journal()
    {
        Entries = new List<Entry>();
        Changed = false;
    }

    public void AddEntry(string date, string prompt, string text)
    {
        Entries.Add(new Entry(date, prompt, text));
        Changed = true;
    }

    public void Load(string filename)
    {
        if (File.Exists(filename))
        {
            Entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
            Changed = false;
        }
    }

    public void Save(string filename)
    {
        List<string> lines = new List<string>();
        foreach (Entry entry in Entries)
        {
            lines.Add($"{entry.Date}|{entry.Prompt}|{entry.Text}");
        }
        File.WriteAllLines(filename, lines);
        Changed = false;
    }

    public void DisplayEntries()
    {
        foreach (Entry entry in Entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Entry: {entry.Text}\n");
        }
    }
}

