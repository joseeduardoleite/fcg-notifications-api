
namespace FiapCloudGames.Notifications.Domain.Services.v1;

public interface IEmailService
{
    Task EnviarEmailBoasVindasAsync(Guid usuarioId);
    Task EnviarEmailPagamentoAprovadoAsync(Guid usuarioId, Guid jogoId);
    Task EnviarEmailPagamentoRejeitadoAsync(Guid usuarioId, Guid jogoId, string motivo);
}