using AlmacenEconomia.Application.Interfaces.Email;
using AlmacenEconomia.Domain.Events.Code.Update;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Code;

public class UpdateCodeEntityEventHandler : INotificationHandler<UpdateCodeEntityEvent>
{
    private readonly ISendEmailService _emailService;
    private readonly ILogger<UpdateCodeEntityEventHandler> _logger;

    public UpdateCodeEntityEventHandler(ISendEmailService emailService, ILogger<UpdateCodeEntityEventHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(UpdateCodeEntityEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var subject = "🔐 Código de verificación - Almacén Economía";

            // Construir un cuerpo HTML profesional
            var htmlBody = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #2c3e50; color: white; padding: 20px; text-align: center; border-radius: 8px 8px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 8px 8px; }}
                    .code {{ font-size: 32px; font-weight: bold; text-align: center; letter-spacing: 5px; background-color: #ecf0f1; padding: 15px; border-radius: 6px; font-family: monospace; }}
                    .footer {{ font-size: 12px; text-align: center; margin-top: 20px; color: #7f8c8d; }}
                    .button {{ display: inline-block; background-color: #3498db; color: white; text-decoration: none; padding: 10px 20px; border-radius: 5px; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>Delivery's Home</h2>
                    </div>
                    <div class='content'>
                        <p>Hola <strong>{System.Net.WebUtility.HtmlEncode(notification.Email)}</strong>,</p>
                        <p>Hemos recibido una solicitud para restablecer tu contraseña. Usa el siguiente código de verificación para continuar:</p>
                        <div class='code'>{notification.Code}</div>
                        <p>Este código es válido hasta <strong>{notification.ExpirationUtc:dd/MM/yyyy HH:mm} UTC</strong>.</p>
                        <p>Si no solicitaste este cambio, puedes ignorar este mensaje de forma segura. Tu cuenta no se verá afectada.</p>
                        <p>Gracias por confiar en nosotros.</p>
                        <p>Saludos cordiales,<br>Equipo de Almacén Economía</p>
                    </div>
                    <div class='footer'>
                        <p>© {DateTime.UtcNow.Year} Almacén Economía. Todos los derechos reservados.</p>
                        <p>Este es un mensaje automático, por favor no respondas a este correo.</p>
                    </div>
                </div>
            </body>
            </html>";

            var result = await _emailService.SendEmailAsync(
                notification.Email,
                notification.Email,
                subject,
                htmlBody,
                cancellationToken
            );

            if (result.IsFailure)
                _logger.LogWarning("Error al enviar correo de código a {Email}: {Error}", notification.Email, result.error?.Message);
            else
                _logger.LogInformation("Código de verificación enviado a {Email}", notification.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción al manejar evento de código para {Email}", notification.Email);
        }
    }
}