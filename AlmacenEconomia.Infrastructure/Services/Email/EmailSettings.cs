namespace AlmacenEconomia.Infrastructure.Services.Email;
public class EmailSetting
{
    public string SenderEmail {get ; set ; } = string.Empty;
    public string SenderName {get ; set ; } = string.Empty;
    public string SmtpServer {get ; set ; } = string.Empty;
    public int SmtpPort {get ; set ; }
    public string Username {get ; set ;} = string.Empty;
    public string Password {get ; set ; } = string.Empty;

}