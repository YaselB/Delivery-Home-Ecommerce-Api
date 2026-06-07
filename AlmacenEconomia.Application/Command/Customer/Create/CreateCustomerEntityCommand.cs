using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Domain.Entity.Customer;

namespace AlmacenEconomia.Application.Command.Customer.Create;
public class CreateCustomerEntityCommand : CreateGenericEntityCommand<CustomerEntity>
{
    public string Email {get ; set ;} = string.Empty;
    public string Password {get ; set ;} = string.Empty;
}