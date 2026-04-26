// Badge 3ndha 1-1 relationship m3 Attendee
namespace EventHub.Models;

public class Badge
{
    public int Id { get; set; }
    public string BadgeNumber { get; set; } = string.Empty;
    public DateTime IssuedDate { get; set; }
    public BadgeTier Tier { get; set; }

    // Forigen key to Attendee
    public int AttendeeId { get; set; }
    public Attendee Attendee { get; set; } = null!;
}
