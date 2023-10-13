using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DigitalTours.Create;

internal sealed class SendReservationConfirmationEventHandler
    : INotificationHandler<ReservationCreatedEvent>
{
    private readonly ILogger<SendReservationConfirmationEventHandler> _logger;

    public SendReservationConfirmationEventHandler(ILogger<SendReservationConfirmationEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending order confirmation {@ReservationId}", notification.ReservationId);

        await Task.Delay(2000, cancellationToken);

        _logger.LogInformation("Order confirmation sent {@ReservationId}", notification.ReservationId);
    }
}
