using System;

public class Address
{
    private readonly string _street;
    private readonly string _city;
    private readonly string _state;
    private readonly string _country;

    public Address(string street, string city, string state, string country)
    {
        _street = street ?? throw new ArgumentNullException(nameof(street));
        _city = city ?? throw new ArgumentNullException(nameof(city));
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _country = country ?? throw new ArgumentNullException(nameof(country));
    }

    public string GetFullAddress() => $"{_street}, {_city}, {_state}, {_country}";
    public bool IsInUSA() => string.Equals(_country, "USA", StringComparison.OrdinalIgnoreCase);
}

public abstract class Event
{
    private readonly string _title;
    private readonly string _description;
    private readonly DateTime _date;
    private readonly TimeSpan _time;
    private readonly Address _address;

    protected Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        _title = title ?? throw new ArgumentNullException(nameof(title));
        _description = description ?? throw new ArgumentNullException(nameof(description));
        _date = date;
        _time = time;
        _address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public string Title => _title;
    public string Description => _description;
    public DateTime Date => _date;
    public TimeSpan Time => _time;
    public Address Location => _address;

    public virtual string GetStandardDetails()
    {
        return $"Event: {_title}\n" +
               $"Description: {_description}\n" +
               $"Date: {_date:yyyy-MM-dd}\n" +
               $"Time: {_time:hh\\:mm tt}\n" +
               $"Location: {_address.GetFullAddress()}";
    }

    public abstract string GetFullDetails();

    public virtual string GetShortDescription()
    {
        return $"{GetType().Name}: {_title}\n" +
               $"Date: {_date:yyyy-MM-dd}";
    }
}

public class Lecture : Event
{
    private readonly string _speaker;
    private readonly int _capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, 
                  Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        _speaker = speaker ?? throw new ArgumentNullException(nameof(speaker));
        _capacity = capacity > 0 ? capacity : throw new ArgumentException("Capacity must be positive", nameof(capacity));
    }

    public string Speaker => _speaker;
    public int Capacity => _capacity;

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\n" +
               $"Type: Lecture\n" +
               $"Speaker: {_speaker}\n" +
               $"Capacity: {_capacity} attendees";
    }
}

public class Reception : Event
{
    private readonly string _rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, 
                   Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        _rsvpEmail = IsValidEmail(rsvpEmail) ? rsvpEmail 
                    : throw new ArgumentException("Invalid email format", nameof(rsvpEmail));
    }

    public string RsvpEmail => _rsvpEmail;

    private bool IsValidEmail(string email)
    {
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch {
            return false;
        }
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\n" +
               $"Type: Reception\n" +
               $"RSVP Email: {_rsvpEmail}";
    }
}

public class OutdoorGathering : Event
{
    private readonly string _weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, 
                          Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        _weatherForecast = weatherForecast ?? throw new ArgumentNullException(nameof(weatherForecast));
    }

    public string WeatherForecast => _weatherForecast;

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\n" +
               $"Type: Outdoor Gathering\n" +
               $"Weather Forecast: {_weatherForecast}";
    }
}

public class Program
{
    public static void Main()
    {
        var usAddress = new Address("123 Park St", "Sunnyville", "CA", "USA");
        var canadaAddress = new Address("456 Mountain View", "Vancouver", "BC", "Canada");

        var events = new Event[]
        {
            new Lecture("Tech Innovations", "Exploring future technologies", 
                       new DateTime(2023, 11, 15), new TimeSpan(10, 0, 0), 
                       usAddress, "Dr. Sarah Johnson", 150),
            
            new Reception("Annual Gala", "Company anniversary celebration", 
                        new DateTime(2023, 12, 10), new TimeSpan(18, 30, 0), 
                        usAddress, "rsvp@company.com"),
            
            new OutdoorGathering("Summer Picnic", "Employee appreciation event", 
                               new DateTime(2023, 7, 15), new TimeSpan(12, 0, 0), 
                               canadaAddress, "Sunny, 25°C")
        };

        foreach (var ev in events)
        {
            Console.WriteLine("=== Standard Details ===");
            Console.WriteLine(ev.GetStandardDetails());
            
            Console.WriteLine("\n=== Full Details ===");
            Console.WriteLine(ev.GetFullDetails());
            
            Console.WriteLine("\n=== Short Description ===");
            Console.WriteLine(ev.GetShortDescription());
            
            Console.WriteLine("\n" + new string('-', 40) + "\n");
        }
    }
}