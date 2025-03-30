using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.User;

namespace Medical_E_Commerce.Service.UserService;

public class UserService(UserManager<ApplicationUser> manager
    , ApplicationDbcontext dbcontext
    , IWebHostEnvironment webHostEnvironment) : IUserService
{
    private readonly string Imageepath = $"{webHostEnvironment.WebRootPath}/Images";

    public async Task<Result> ChangePassword(string id, ChangePasswordRequest request)
    {
        var user = await manager.FindByIdAsync(id);

        var result = await manager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassord);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    public async Task<Result<UserProfileResponse>> GetUserProfile(string id)
    {
        var user = await manager.Users
            .Where(i => i.Id == id)
            .ProjectToType<UserProfileResponse>()
            .SingleAsync();
        ;

        return Result.Success(user);
    }
    public async Task<Result> UpdateUserProfile(string id, UpdateUserProfileRequest request)
    {
        //var user = await manager.FindByIdAsync(id);

        //user = request.Adapt(user);

        //await manager.UpdateAsync(user!);
        await manager.Users
            .Where(i => i.Id == id)
            .ExecuteUpdateAsync(set =>
            set.SetProperty(x => x.UserAddress, request.UserAddress)
               .SetProperty(x => x.UserFullName, request.UserFullName));

        return Result.Success();
    }
    public async Task<(FileStream? fileStream, string contentType, string fileName)> FileStream(string id)
    {
        var user = await manager.FindByIdAsync(id);

        var Id = user!.ImageId;

        var file = await dbcontext.Images.FindAsync(Id);

        if (file == null)
            return (null, string.Empty, string.Empty);

        var path = Path.Combine(Imageepath, file.FileName);

        var filestream = File.OpenRead(path);

        return (filestream, file.ContentType, file.FileName);
    }
    public async Task<Guid> UpoadImage(string id, IFormFile file)
    {
        var user = await manager.FindByIdAsync(id);


        var uploadedfile = new Image
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileExtenstions = Path.GetExtension(file.FileName),
            UserId = id
        };

        user!.ImageId = uploadedfile.Id;
        var path = Path.Combine(Imageepath, uploadedfile.FileName);

        using var stream = File.Create(path);

        await file.CopyToAsync(stream);

        await manager.UpdateAsync(user);

        await dbcontext.AddAsync(uploadedfile);

        await dbcontext.SaveChangesAsync();

        return uploadedfile.Id;
    }
}
