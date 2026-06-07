using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Customer;

namespace AlmacenEconomia.Application.Command.Customer.AddPermissions;
public class AddPermissionsCustomerCommand : UpdateGenericEntityCommand<CustomerEntity>
{
    public required List<string> Permissions {get ; set ;}
}