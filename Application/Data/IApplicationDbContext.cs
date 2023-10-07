using Domain.Curators;
using Domain.DigitalTours;
using Domain.EstateExhibits;
using Domain.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Data;

public interface IApplicationDbContext
{ 
    DbSet<Participant> Participants { get; set; }

    DbSet<Reservation> Reservations { get; set; }

    DbSet<HistoricalEvent> HistoricalEvents { get; set; }

    DbSet<Expert> Experts { get; set; }

    DbSet<Highlight> Highlights { get; set; }

    DbSet<InteractiveSession> InteractiveSessions { get; set; }

    DbSet<MultimediaContent> MultimediaContents { get; set; }

    DbSet<Estate> Estates { get; set; }

    DbSet<VirtualTour> VirtualTours { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
