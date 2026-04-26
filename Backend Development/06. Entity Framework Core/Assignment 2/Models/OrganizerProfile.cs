// 💡 OrganizerProfile has a one-to-one relationship with Organizer 
// fah mish 7tb2a mwgoda mn  8iro

namespace EventHub.Models;

public class OrganizerProfile
{
    public int Id { get; set; }
    public string? Biography { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? LogoUrl { get; set; }

    // Forigen key lel Organizer
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; } = null!;
}
