﻿using Mapster;
using Medical_E_Commerce.Contracts.Auth;
using Medical_E_Commerce.Entities;

namespace Medical_E_Commerce.Mapping;

public class MappingConfigration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //typing your all mapping configurations
        config.NewConfig<ApplicationUser, AuthResponse>()
            .Map(des => des.UserAddress, sour => sour.UserAddress);
    }
}
