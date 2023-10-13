using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DigitalTours.Create;

internal sealed class CreatePaymentRequestEventHandler
    : INotificationHandler<ReservationCreatedEvent>
{
    private readonly ILogger<CreatePaymentRequestEventHandler> _logger;

    public CreatePaymentRequestEventHandler(ILogger<CreatePaymentRequestEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting payment request {@ReservationId}", notification.ReservationId);

        await Task.Delay(2000, cancellationToken);

        _logger.LogInformation("Payment request started {@ReservationId}", notification.ReservationId);
    }
}
