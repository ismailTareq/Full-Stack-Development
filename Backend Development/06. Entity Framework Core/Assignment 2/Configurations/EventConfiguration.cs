using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(e => e.EndDate).IsRequired(false);
        builder.HasOne(e => e.ParentEvent).WithMany(e => e.Sessions).HasForeignKey(e => e.ParentEventId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        builder.OwnsOne(e => e.Audit, audit =>
        {
            audit.Property(a => a.CreatedAt).HasColumnName("CreatedAt").IsRequired();

            audit.Property(a => a.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        });
        // Forigen key to Organizer
        builder.HasOne(e => e.Organizer).WithMany(o => o.Events).HasForeignKey(e => e.OrganizerId).OnDelete(DeleteBehavior.Cascade);
    }
}
