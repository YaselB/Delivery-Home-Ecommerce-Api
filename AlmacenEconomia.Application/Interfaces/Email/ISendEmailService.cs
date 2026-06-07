using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Interfaces.Email;
public interface ISendEmailService
{
    public Task<Result<Unit>> SendEmailAsync(string toEmail , string toName , string Subject , string htmlBody , CancellationToken cancellationToken);
}