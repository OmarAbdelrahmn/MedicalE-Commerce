namespace Medical_E_Commerce.Abstractions.Errors;

public class RolesErrors
{
    public static readonly Error ROleIsExcists = new("Role.RoleIsExcists", "Role already in the system", StatusCodes.Status401Unauthorized);
    public static readonly Error NotFound = new("Role.NotFound", "Role not in the system", StatusCodes.Status401Unauthorized);
    public static readonly Error DaplicatedRole = new("Role.DaplicatedRole", "Daplicated Role ", StatusCodes.Status401Unauthorized);

}
