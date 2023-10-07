using Application.Data;
using Domain.Curators;
using Domain.DigitalTours;
using Domain.EstateExhibits;
using Domain.Participants;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Participant> Participants { get; set; }

    public DbSet<MultimediaContent> MultimediaContents { get; set; }

    public DbSet<HistoricalEvent> HistoricalEvents { get; set; }

    public DbSet<Estate> Estates { get; set; }

    public DbSet<VirtualTour> VirtualTours { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<InteractiveSession> InteractiveSessions { get; set; }

    public DbSet<Highlight> Highlights { get; set; }

    public DbSet<Expert> Experts { get; set; }
}