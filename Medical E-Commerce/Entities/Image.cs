namespace Medical_E_Commerce.Entities;

public class Image
{
    public Guid Id { get; set; } = Guid.CreateVersion7(); 
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileExtenstions { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;
}
