namespace Medical_E_Commerce.Service.UserService;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetUserProfile(string id);
    Task<Result> UpdateUserProfile(string id, UpdateUserProfileRequest request);
    Task<Result> ChangePassword(string id, ChangePasswordRequest request);
    Task<Guid> UpoadImage(string id, IFormFile image);
    Task<(FileStream? fileStream, string contentType, string fileName)> FileStream(string id);


}
