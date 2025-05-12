using Medical_E_Commerce.Contracts.Admin;
using Medical_E_Commerce.Contracts.Fav;
using Medical_E_Commerce.Contracts.Item;

namespace Medical_E_Commerce.Mapping;

public class MappingConfigration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //typing your all mapping configurations
        config.NewConfig<ApplicationUser, AuthResponse>()
            .Map(des => des.UserAddress, sour => sour.UserAddress);

        config.NewConfig<Registerrequest, ApplicationUser>()
            .Map(des => des.UserAddress, sour => sour.UserAdress);

        config.NewConfig<Pharmacy, PharmacyResponse>()
            .Map(des => des.ImageURL, sour => sour.ImageURL);

        config.NewConfig<Pharmacy, PhResponse>()
            .Map(des => des.ImageURL, sour => sour.ImageURL);

        config.NewConfig<Item, ItemResponse>()
            .Map(des => des.ImageURL, sour => sour.ImageURL);


        config.NewConfig<Item, ItemDetailsResponse>()
            .Map(des => des.ImageURL, sour => sour.ImageURL);


        config.NewConfig<List<int>, FavResponse>()
            .Map(des => des.ItemsIds, sour => sour);


        config.NewConfig<(ApplicationUser user, IList<string> userroles), UserResponse>()
            .Map(des => des, src => src.user)
            .Map(des => des.Roles, src => src.userroles);
    }
}
