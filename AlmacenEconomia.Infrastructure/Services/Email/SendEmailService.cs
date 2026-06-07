using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AlmacenEconomia.Infrastructure.Services.Email;

public class SendEmailService : ISendEmailService
{
    private readonly EmailSetting emailSetting;
    private readonly ILogger<EmailSetting> logger;
    public SendEmailService(IOptions<EmailSetting> emailSetting , ILogger<EmailSetting> logger)
    {
        this.emailSetting = emailSetting.Value;
        this.logger = logger;
    }
    public async Task<Result<Unit>> SendEmailAsync(string toEmail, string toName, string Subject, string htmlBody, CancellationToken cancellationToken)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("DeliveryHome" , "yaselbarrioscarrillo@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(toName , toEmail));
            emailMessage.Subject = Subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody 
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();
            using var client = new SmtpClient();
            await client.ConnectAsync(emailSetting.SmtpServer ,emailSetting.SmtpPort ,SecureSocketOptions.StartTls , cancellationToken);
            await client.AuthenticateAsync(emailSetting.Username ,emailSetting.Password ,cancellationToken);
            await client.SendAsync(emailMessage , cancellationToken);
            await client.DisconnectAsync(true , cancellationToken);
            logger.LogInformation("Correo enviado satisfactoriamente al gmail : "+toEmail);
        }
        catch (Exception ex)
        {
            logger.LogWarning("Error al enviar el mensaje al correo : "+toEmail+" error: "+ex.Message);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}