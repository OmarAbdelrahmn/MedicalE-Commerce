namespace Medical_E_Commerce.Entities;

public class ApplicationRoles : IdentityRole
{
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}

