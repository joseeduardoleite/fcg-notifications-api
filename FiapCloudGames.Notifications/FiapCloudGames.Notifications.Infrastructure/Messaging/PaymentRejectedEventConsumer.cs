using FiapCloudGames.Contracts.Events;
using FiapCloudGames.Notifications.Domain.Services.v1;
using MassTransit;

namespace FiapCloudGames.Notifications.Infrastructure.Messaging;

public class PaymentRejectedEventConsumer(IEmailService emailService) : IConsumer<PaymentRejectedEvent>
{
    public async Task Consume(ConsumeContext<PaymentRejectedEvent> context)
    {
        await emailService.EnviarEmailPagamentoRejeitadoAsync(
            context.Message.UsuarioId,
            context.Message.JogoId,
            context.Message.Motivo
        );
    }
}