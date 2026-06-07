using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlmacenEconomia.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiredPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string permissionRequired;
    public RequiredPermissionAttribute(string PermissionRequired)
    {
        permissionRequired = PermissionRequired;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if(user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                error = "No autenticado",
                mensaje = "Debes iniciar sesion para acceder a este recurso"
            });
            return;
        }
        var hasPermissions = user.Claims.Any( h => h.Type == "permission" && h.Value == permissionRequired);
        if (!hasPermissions)
        {
            context.Result = new ObjectResult(new
            {
                error = "No tiene permiso para realizar esta accion",
                statusCode = 403
            })
            {
                StatusCode = 403
            };
            return;
        }
    }
}