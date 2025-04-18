namespace Medical_E_Commerce.Contracts;

public record OrderResopnse
(
    int Id,
    string UserId,
    IList<int>? ItemsId,
    decimal TotalPrice,
    int PharmacyId
);
