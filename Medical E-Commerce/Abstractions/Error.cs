namespace Medical_E_Commerce.Abstractions;

public record Error(string Code, string Description, int? StatuesCode)
{
    public static Error non => new(string.Empty, string.Empty, null);
}
