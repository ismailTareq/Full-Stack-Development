public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MaxAttendees { get; set; }

    // Self-referencing
    public int? ParentEventId { get; set; }
    public Event? ParentEvent { get; set; }
    public ICollection<Event> Sessions { get; set; } = new List<Event>();

    // timestamps
    public EventAudit Audit { get; set; } = new();

    // Forigen key lel Organizer
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; } = null!;

    // Navigation lel registrations
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}

// maped to the same table as Event
public class EventAudit
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
