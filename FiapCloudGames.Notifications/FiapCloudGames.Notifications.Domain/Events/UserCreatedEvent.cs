using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Notifications.Domain.Events;

[ExcludeFromCodeCoverage]
public class UserCreatedEvent
{
    public Guid UsuarioId { get; set; }
}