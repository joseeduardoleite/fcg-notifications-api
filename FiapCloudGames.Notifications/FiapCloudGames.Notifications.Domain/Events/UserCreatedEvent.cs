using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Contracts.Events;

[ExcludeFromCodeCoverage]
public class UserCreatedEvent
{
    public Guid UsuarioId { get; init; }
}