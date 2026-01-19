using FiapCloudGames.Notifications.Domain.Services.v1;
using Microsoft.Extensions.Logging;

namespace FiapCloudGames.Notifications.Application.Services.v1;

public sealed class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public Task EnviarEmailBoasVindasAsync(Guid usuarioId)
    {
        logger.LogInformation("Email de boas-vindas enviado para {UsuarioId}", usuarioId);
        return Task.CompletedTask;
    }

    public Task EnviarEmailPagamentoAprovadoAsync(Guid usuarioId, Guid jogoId)
    {
        logger.LogInformation("Compra aprovada: {UsuarioId} comprou {JogoId}", usuarioId, jogoId);
        return Task.CompletedTask;
    }

    public Task EnviarEmailPagamentoRejeitadoAsync(Guid usuarioId, Guid jogoId, string motivo)
    {
        logger.LogWarning(
            "Compra rejeitada: {UsuarioId}, Jogo {JogoId}, Motivo: {Motivo}",
            usuarioId, jogoId, motivo
        );

        return Task.CompletedTask;
    }
}