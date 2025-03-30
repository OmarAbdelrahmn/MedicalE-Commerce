using Medical_E_Commerce.Contracts.Admin;
using Medical_E_Commerce.Contracts.Item;
using Medical_E_Commerce.Contracts.Pharmacy;

namespace Medical_E_Commerce.Mapping;

public class MappingConfigration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //typing your all mapping configurations
        config.NewConfig<ApplicationUser, AuthResponse>()
            .Map(des => des.UserAddress, sour => sour.UserAddress);

        config.NewConfig<Pharmacy, PharmacyResponse>()
            .Map(des => des.ImageURL, sour => sour.ImageURL);

        config.NewConfig<Item, ItemResponse>()
            .Map(des => des.ImageURL, sour => sour.ImageURL);


        config.NewConfig<(ApplicationUser user, IList<string> userroles), UserResponse>()
            .Map(des => des, src => src.user)
            .Map(des => des.Roles, src => src.userroles);
    }
}
