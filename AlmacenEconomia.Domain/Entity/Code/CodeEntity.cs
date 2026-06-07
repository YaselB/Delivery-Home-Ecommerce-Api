using AlmacenEconomia.Domain.Entity.Generic;

namespace AlmacenEconomia.Domain.Entity.Code;
public class CodeEntity : GenericEntity<CodeEntity>
{
    public string Code  {get ; set ;} = string.Empty;
    public string Email {get; set ;} = string.Empty;
    public DateTime DateTimeExpiration{ get ; set ;}
    public static CodeEntity Create(string Code , string Email)
    {
        var code = new CodeEntity
        {
            Email = Email,
            Code = Code,
            DateTimeExpiration = DateTime.UtcNow.AddMinutes(30),
        };
        return code;
    }
    public void Update(string Code)
    {
        this.Code = Code;
        this.UpdatedAt = DateTime.UtcNow;
        this.DateTimeExpiration = DateTime.UtcNow.AddMinutes(30);
    }
    public void ClearCode()
    {
        this.Code = "";
        this.UpdatedAt = DateTime.UtcNow;
    }
}