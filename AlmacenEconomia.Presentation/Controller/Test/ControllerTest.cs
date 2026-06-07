using AlmacenEconomia.Application.Interfaces.Email;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ISendEmailService _emailService;
    public TestController(ISendEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("send-test-email")]
    public async Task<IActionResult> SendTestEmail(CancellationToken ct)
    {
        var result = await _emailService.SendEmailAsync(
            toEmail: "yaselbarrioscarrillo@gmail.com", // Cambia por tu correo real
            toName: "Prueba",
            Subject: "Correo de prueba desde AlmacenEconomia",
            htmlBody: "<h1>Hola</h1><p>Este es un correo de prueba enviado desde mi API.</p>",
            cancellationToken: ct
        );

        if (result.IsFailure)
            return BadRequest(new { error = result.error?.Message });

        return Ok(new { message = "Correo enviado correctamente" });
    }
}