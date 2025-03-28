using Medical_E_Commerce.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Medical_E_Commerce.Persistence;

public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : 
    IdentityDbContext<ApplicationUser,ApplicationRoles,string>(options)
{


    public DbSet<Pharmacy> Pharmacies { get; set; } = default!;
    public DbSet<Item> Items{ get; set; } = default!;
    public DbSet<CartItem> CartItems{ get; set; } = default!;
    public DbSet<Cart> Carts{ get; set; } = default!;
    public DbSet<Image> Images{ get; set; } = default!;
    public DbSet<Article> Articles{ get; set; } = default!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // to make intities configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // to chagne delete behavior to restrict
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        modelBuilder.Entity<ApplicationUser>()
        .HasOne(a => a.Image)
        .WithOne(i => i.User)
        .HasForeignKey<Image>(i => i.UserId);



        base.OnModelCreating(modelBuilder);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

}
