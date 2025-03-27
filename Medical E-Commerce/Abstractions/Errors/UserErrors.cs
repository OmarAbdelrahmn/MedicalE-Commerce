namespace Medical_E_Commerce.Abstractions.Errors;

public class UserErrors
{
    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);
    public static readonly Error UserAlreayExists = new("User.UserAlreadyExists", "User with this Email exists", StatusCodes.Status401Unauthorized);

}
