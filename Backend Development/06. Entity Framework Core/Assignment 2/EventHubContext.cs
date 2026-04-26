// Dah el manager for the Data base
using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Configurations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.ToTable("Registrations");
        builder.HasKey(r => new { r.AttendeeId, r.EventId });
        builder.Property(r => r.Note).HasMaxLength(500).IsRequired(false);
        builder.Property(r => r.RegisteredAt).IsRequired();

        // M-M relationships
        builder.HasOne(r => r.Attendee).WithMany(a => a.Registrations).HasForeignKey(r => r.AttendeeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.Event).WithMany(e => e.Registrations).HasForeignKey(r => r.EventId).OnDelete(DeleteBehavior.Restrict);
    }
}
