namespace Medical_E_Commerce.Abstractions.Errors;

public class UserErrors
{
    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);
    public static readonly Error UserAlreayExists = new("User.UserAlreadyExists", "User with this Email exists", StatusCodes.Status401Unauthorized);
    public static readonly Error Disableuser = new("User.UserIsDisable", "This user is disable", StatusCodes.Status401Unauthorized);
    public static readonly Error EmailNotConfirmed = new("User.EmailNotConfirmed", "This is not confirmed ", StatusCodes.Status401Unauthorized);
    public static readonly Error userLockedout = new("User.userLockedout", "user Lockedout , contact the admin", StatusCodes.Status401Unauthorized);
    public static readonly Error UserNotFound = new("User.UserNotFound", "user Not Found", StatusCodes.Status401Unauthorized);

}
