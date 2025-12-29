using backend.Domains.Entities;
using backend.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AvailabilitySlot> AvailabilitySlots => Set<AvailabilitySlot>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<IssueItem> Faq => Set<IssueItem>();
    public DbSet<TwoFactorCode> TwoFactorCodes => Set<TwoFactorCode>();
    public DbSet<Treatment> Treatments => Set<Treatment>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Appointment>().Property(x => x.RowVersion).IsRowVersion();
        b.Entity<Appointment>().HasIndex(x => new { x.UserId, x.StartTime });
        b.Entity<Appointment>().HasIndex(x => new { x.ProviderId, x.StartTime });
        b.Entity<AvailabilitySlot>().HasIndex(x => new { x.ProviderId, x.StartTime });
        b.Entity<Notification>().HasIndex(x => new { x.UserId, x.IsRead, x.CreatedTime });
    }
}
