namespace GreatSoft.Be.Domain.Common;

public static class RoleTypeExtensions
{
    public static string ToStringValue(this RoleType roleType)
    {
        return roleType switch
        {
            RoleType.SysAdmin => RoleConstants.SysAdmin,
            RoleType.Admin => RoleConstants.Admin,
            RoleType.ResidentAdmin => RoleConstants.ResidentAdmin,
            RoleType.Resident => RoleConstants.Resident,
            RoleType.Security => RoleConstants.Security,
            RoleType.Manager => RoleConstants.Manager,
            _ => roleType.ToString()
        };
    }

    public static RoleType? FromString(string? roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return null;

        return roleName switch
        {
            RoleConstants.SysAdmin => RoleType.SysAdmin,
            RoleConstants.Admin => RoleType.Admin,
            RoleConstants.ResidentAdmin => RoleType.ResidentAdmin,
            RoleConstants.Resident => RoleType.Resident,
            RoleConstants.Security => RoleType.Security,
            RoleConstants.Manager => RoleType.Manager,
            _ => null
        };
    }
}

