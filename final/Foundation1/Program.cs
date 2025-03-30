using System;
using System.Collections.Generic;

// Abstracted comment system
public interface ICommentSystem
{
    void AddComment(string commenterName, string commentText);
    int GetCommentCount();
    IEnumerable<(string Name, string Text)> GetAllComments();
}

// Core video entity with abstracted comment functionality
public class YouTubeVideo
{
    // Properties with private setters to enforce controlled modification
    public string Title { get; private set; }
    public string Author { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime UploadDate { get; private set; }
    
    // Using composition for comments (better than inheritance)
    private readonly ICommentSystem _commentSystem;

    public YouTubeVideo(string title, string author, TimeSpan duration, ICommentSystem commentSystem)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Duration = duration;
        UploadDate = DateTime.Now;
        _commentSystem = commentSystem ?? throw new ArgumentNullException(nameof(commentSystem));
    }

    // Public interface for comment operations
    public void AddComment(string commenterName, string commentText) 
        => _commentSystem.AddComment(commenterName, commentText);

    public int GetCommentCount() => _commentSystem.GetCommentCount();
    
    public IEnumerable<(string Name, string Text)> GetAllComments() 
        => _commentSystem.GetAllComments();

    // Display method with formatting options
    public void DisplayInfo(bool showComments = true)
    {
        Console.WriteLine($"Video: {Title}");
        Console.WriteLine($"By: {Author}");
        Console.WriteLine($"Duration: {Duration:mm\\:ss}");
        Console.WriteLine($"Uploaded: {UploadDate:yyyy-MM-dd}");
        Console.WriteLine($"Comments: {GetCommentCount()}");

        if (showComments && GetCommentCount() > 0)
        {
            Console.WriteLine("\nComments:");
            foreach (var (name, text) in GetAllComments())
            {
                Console.WriteLine($"- {name}: {text}");
            }
        }
    }
}

// Concrete implementation of comment system
public class VideoCommentSystem : ICommentSystem
{
    private readonly List<(string Name, string Text)> _comments = new List<(string, string)>();

    public void AddComment(string commenterName, string commentText)
    {
        if (string.IsNullOrWhiteSpace(commenterName))
            throw new ArgumentException("Commenter name cannot be empty", nameof(commenterName));
        
        if (string.IsNullOrWhiteSpace(commentText))
            throw new ArgumentException("Comment text cannot be empty", nameof(commentText));

        _comments.Add((commenterName, commentText));
    }

    public int GetCommentCount() => _comments.Count;

    public IEnumerable<(string Name, string Text)> GetAllComments() => _comments;
}

// Video repository for managing collections of videos
public class VideoRepository
{
    private readonly List<YouTubeVideo> _videos = new List<YouTubeVideo>();

    public void AddVideo(YouTubeVideo video) => _videos.Add(video);

    public IEnumerable<YouTubeVideo> GetVideos() => _videos;

    public void DisplayAllVideos(bool showComments = true)
    {
        foreach (var video in _videos)
        {
            video.DisplayInfo(showComments);
            Console.WriteLine(new string('-', 40));
        }
    }
}

class Program
{
    static void Main()
    {
        var repository = new VideoRepository();
        
        // Create videos with their own comment systems
        var video1 = new YouTubeVideo(
            "C# Advanced Patterns", 
            "CodeMaster", 
            TimeSpan.FromMinutes(15), 
            new VideoCommentSystem());
        
        var video2 = new YouTubeVideo(
            "ASP.NET Core Fundamentals", 
            "WebDev Guru", 
            TimeSpan.FromMinutes(22), 
            new VideoCommentSystem());

        // Add comments
        video1.AddComment("DevFan42", "Great explanation of design patterns!");
        video1.AddComment("NewbieCoder", "Could you make a video about DI containers?");
        video1.AddComment("SeniorDev", "Nice content, but needs more real-world examples.");
        
        video2.AddComment("WebEnthusiast", "Perfect timing for my new project!");
        video2.AddComment("BackendDev", "When will you cover authentication?");

        // Add to repository
        repository.AddVideo(video1);
        repository.AddVideo(video2);

        // Display all videos with comments
        repository.DisplayAllVideos();

        // Optional: Display without comments
        // repository.DisplayAllVideos(showComments: false);
    }
}