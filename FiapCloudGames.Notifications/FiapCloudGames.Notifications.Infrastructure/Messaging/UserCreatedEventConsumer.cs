using FiapCloudGames.Notifications.Domain.Events;
using FiapCloudGames.Notifications.Domain.Services.v1;
using MassTransit;

namespace FiapCloudGames.Notifications.Infrastructure.Messaging;

public class UserCreatedEventConsumer(IEmailService emailService): IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        await emailService.EnviarEmailBoasVindasAsync(context.Message.UsuarioId);
    }
}