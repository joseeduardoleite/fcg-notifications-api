using FiapCloudGames.Notifications.Domain.Events;
using FiapCloudGames.Notifications.Domain.Services.v1;
using MassTransit;

namespace FiapCloudGames.Notifications.Infrastructure.Messaging;

public class PaymentApprovedEventConsumer(IEmailService emailService) : IConsumer<PaymentApprovedEvent>
{
    public async Task Consume(ConsumeContext<PaymentApprovedEvent> context)
    {
        await emailService.EnviarEmailPagamentoAprovadoAsync(
            context.Message.UsuarioId,
            context.Message.JogoId
        );
    }
}