using EventHub;
using EventHub.Models;

using var context = new EventHubContext();
context.Database.EnsureCreated();
Console.WriteLine("EventHub database has been created successfully el mafrod!");

var organizer = new Organizer
{
    Name        = "ismail tarek",
    CompanyName = "Alprotein",
    IsVerified  = true,
    Profile = new OrganizerProfile
    {
        Biography  = "Embedded linux engineer",
        WebsiteUrl = "",
        LogoUrl    = ""
    }
};
context.Organizers.Add(organizer);
context.SaveChanges();

var conference = new Event
{
    Title        = "2y 7aga",
    Description  = "conference for gaming",
    StartDate    = new DateTime(2026, 4, 1),
    EndDate      = new DateTime(2026, 4, 5),
    MaxAttendees = 500,
    OrganizerId  = organizer.Id,
    Audit        = new EventAudit { CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
    Sessions = new List<Event>
    {
        new Event
        {
            Title        = "Workshop",
            Description  = "gamming session",
            StartDate    = new DateTime(2026, 4, 1, 10, 0, 0),
            MaxAttendees = 50,
            OrganizerId  = organizer.Id,
            Audit        = new EventAudit { CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        }
    }
};
context.Events.Add(conference);
context.SaveChanges();

var attendee = new Attendee
{
    FullName = "jomana salah",
    Email    = "jojo@gmail.com",
    HomeAddress = new Address
    {
        Street     = "123 Nile St",
        City       = "Cairo",
        Country    = "Egypt",
        PostalCode = "11511"
    },
    Badge = new Badge
    {
        BadgeNumber = "BADGE-267",
        IssuedDate  = DateTime.UtcNow,
        Tier        = BadgeTier.VIP
    }
};
context.Attendees.Add(attendee);
context.SaveChanges();

var registration = new Registration
{
    AttendeeId   = attendee.Id,
    EventId      = conference.Id,
    Note         = "misa misa",
    RegisteredAt = DateTime.UtcNow
};
context.Registrations.Add(registration);
context.SaveChanges();

Console.WriteLine($"Organizers  : {context.Organizers.Count()}");
Console.WriteLine($"Events      : {context.Events.Count()}");
Console.WriteLine($"Attendees   : {context.Attendees.Count()}");
Console.WriteLine($"Badges      : {context.Badges.Count()}");
Console.WriteLine($"Registrations: {context.Registrations.Count()}");
Console.WriteLine("\nAll done! Database Integrated successfully.");
