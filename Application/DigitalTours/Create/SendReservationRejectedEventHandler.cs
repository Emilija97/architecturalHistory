using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DigitalTours.Create;

internal sealed class SendReservationRejectedEventHandler : INotificationHandler<ReservationRejectedEvent>
{

    private readonly ILogger<SendReservationRejectedEventHandler> _logger;

    public SendReservationRejectedEventHandler(ILogger<SendReservationRejectedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ReservationRejectedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending booking rejected {@ReservationId}", notification.ReservationId);

        await Task.Delay(2000, cancellationToken);

        _logger.LogInformation("Booking rejected sent {@ReservationId}", notification.ReservationId);
    }
}
