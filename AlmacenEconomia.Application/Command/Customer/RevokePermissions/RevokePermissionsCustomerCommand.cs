using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Customer;

namespace AlmacenEconomia.Application.Command.Customer.RevokePermissions;
public class RevokePermissionsCustomerCommand : UpdateGenericEntityCommand<CustomerEntity>
{
    public required List<string> Permissions {get ; set ;}
}