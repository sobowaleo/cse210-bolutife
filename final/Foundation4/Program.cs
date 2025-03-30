using System;
using System.Collections.Generic;

public abstract class Activity
{
    private readonly DateTime _date;
    private readonly int _durationMinutes;

    protected Activity(DateTime date, int durationMinutes)
    {
        if (durationMinutes <= 0)
            throw new ArgumentException("Duration must be positive", nameof(durationMinutes));

        _date = date;
        _durationMinutes = durationMinutes;
    }

    public DateTime Date => _date;
    public int DurationMinutes => _durationMinutes;
    public TimeSpan Duration => TimeSpan.FromMinutes(_durationMinutes);

    // Abstract methods that must be implemented by derived classes
    public abstract double GetDistance(); // in kilometers
    public abstract double GetSpeed();    // in km/h
    public abstract double GetPace();     // in min/km

    // Virtual method that can be overridden for specific activity types
    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetType().Name} ({DurationMinutes} min) - " +
               $"Distance: {GetDistance():F1} km, " +
               $"Speed: {GetSpeed():F1} kph, " +
               $"Pace: {GetPace():F1} min/km";
    }
}

public class Running : Activity
{
    private readonly double _distanceKm;

    public Running(DateTime date, int durationMinutes, double distanceKm) 
        : base(date, durationMinutes)
    {
        if (distanceKm <= 0)
            throw new ArgumentException("Distance must be positive", nameof(distanceKm));

        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;

    public override double GetSpeed() => (_distanceKm / Duration.TotalHours);

    public override double GetPace() => (Duration.TotalMinutes / _distanceKm);

    public override string GetSummary()
    {
        return $"{Date:dd MMM yyyy} Running ({DurationMinutes} min) - " +
               $"Distance: {_distanceKm:F1} km, " +
               $"Speed: {GetSpeed():F1} kph, " +
               $"Pace: {GetPace():F1} min/km";
    }
}

public class Cycling : Activity
{
    private readonly double _distanceKm;

    public Cycling(DateTime date, int durationMinutes, double distanceKm) 
        : base(date, durationMinutes)
    {
        if (distanceKm <= 0)
            throw new ArgumentException("Distance must be positive", nameof(distanceKm));

        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;

    public override double GetSpeed() => (_distanceKm / Duration.TotalHours);

    public override double GetPace() => (Duration.TotalMinutes / _distanceKm);
}

public class Swimming : Activity
{
    private readonly int _laps;
    private const double LapLengthMeters = 50;

    public Swimming(DateTime date, int durationMinutes, int laps) 
        : base(date, durationMinutes)
    {
        if (laps <= 0)
            throw new ArgumentException("Laps must be positive", nameof(laps));

        _laps = laps;
    }

    public override double GetDistance() => (_laps * LapLengthMeters) / 1000; // Convert to km

    public override double GetSpeed() => (GetDistance() / Duration.TotalHours);

    public override double GetPace() => (Duration.TotalMinutes / GetDistance());

    public override string GetSummary()
    {
        return $"{Date:dd MMM yyyy} Swimming ({DurationMinutes} min) - " +
               $"Distance: {GetDistance():F1} km ({_laps} laps), " +
               $"Speed: {GetSpeed():F1} kph, " +
               $"Pace: {GetPace():F1} min/km";
    }
}

public class ExerciseTracker
{
    private readonly List<Activity> _activities = new List<Activity>();

    public void AddActivity(Activity activity)
    {
        _activities.Add(activity ?? throw new ArgumentNullException(nameof(activity)));
    }

    public void DisplayAllActivities()
    {
        Console.WriteLine("Exercise Activity Summary:\n");
        foreach (var activity in _activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine();
        }
    }

    public void DisplayActivityStatistics()
    {
        Console.WriteLine("Exercise Statistics:\n");
        Console.WriteLine($"Total Activities: {_activities.Count}");
        Console.WriteLine($"Total Duration: {TimeSpan.FromMinutes(GetTotalDurationMinutes()):h\\:mm} hours");
        Console.WriteLine($"Total Distance: {GetTotalDistance():F1} km");
        Console.WriteLine();
    }

    private double GetTotalDistance() => _activities.Sum(a => a.GetDistance());
    private double GetTotalDurationMinutes() => _activities.Sum(a => a.DurationMinutes);
}

public class Program
{
    public static void Main()
    {
        var tracker = new ExerciseTracker();

        // Add various activities
        tracker.AddActivity(new Running(new DateTime(2023, 6, 1), 30, 5.2));
        tracker.AddActivity(new Cycling(new DateTime(2023, 6, 2), 45, 15.7));
        tracker.AddActivity(new Swimming(new DateTime(2023, 6, 3), 40, 30));
        tracker.AddActivity(new Running(new DateTime(2023, 6, 5), 25, 3.8));
        tracker.AddActivity(new Cycling(new DateTime(2023, 6, 7), 60, 22.4));

        // Display all activities
        tracker.DisplayAllActivities();

        // Display statistics
        tracker.DisplayActivityStatistics();
    }
}