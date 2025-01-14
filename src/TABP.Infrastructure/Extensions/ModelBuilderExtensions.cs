using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureUserRolesEntity();
    }

    private static ModelBuilder ConfigureUserRolesEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity(junction => junction.ToTable("UserRoles"));
        
        return modelBuilder;
    }
    
    private static ModelBuilder ConfigureCartItemEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>()
            .HasIndex(cartItem => cartItem.UserId);
        
        return modelBuilder;
    }
}