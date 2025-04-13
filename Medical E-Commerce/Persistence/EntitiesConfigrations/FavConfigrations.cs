using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical_E_Commerce.Persistence.EntitiesConfigrations;

public class FavConfigrations : IEntityTypeConfiguration<Fav>
{
    public void Configure(EntityTypeBuilder<Fav> builder)
    {
        builder.ToTable("Favourites")
            .HasKey(f => new {f.Id , f.UserId});


    }
}
