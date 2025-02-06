using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TAB.Domain.Constants.Discount;
using TABP.Domain.Constants.City;
using TABP.Domain.Constants.Hotel;
using TABP.Domain.Constants.Review;
using TABP.Domain.Constants.Room;
using TABP.Domain.Constants.User;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Search.Response;

namespace TABP.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder MapViews(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableRoom>()
        .ToView("vw_AvailableRooms")
        .HasNoKey();

        return modelBuilder;
    }
    public static ModelBuilder ConfigureEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureUserEntity()
            .ConfigureHotelReviewEntity()
            .ConfigureHotelEntity()
            .ConfigureDiscountEntity()
            .ConfigureHotelVisitEntity()
            .ConfigureCityEntity()
            .ConfigureCartEntity();

        return modelBuilder;
    }

    public static ModelBuilder ConfigureRelations(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureUserRolesEntity();
        
        return modelBuilder;
    }

    private static ModelBuilder ConfigureUserRolesEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity(junction => junction.ToTable("RoleUser"));
        
        return modelBuilder;
    }

    private static ModelBuilder ConfigureUserEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurePropertyLength<User>(
            user => user.FirstName, UserConstants.MaxUsernameLength);
        modelBuilder.ConfigurePropertyLength<User>(
            user => user.LastName, UserConstants.MaxFirstNameLength);
        modelBuilder.ConfigurePropertyLength<User>(
            user => user.Username, UserConstants.MaxLastNameLength);
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Username);

        return modelBuilder;
    }

    private static ModelBuilder ConfigurePropertyLength<TEntity>(
        this ModelBuilder modelBuilder,
        Expression<Func<TEntity, string>> property,
        int maxLength) where TEntity : class
    {
        modelBuilder.Entity<TEntity>()
            .Property(property)
            .HasMaxLength(maxLength);

        return modelBuilder;
    }

    private static ModelBuilder ConfigureHotelReviewEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurePropertyLength<HotelReview>(
            review => review.Feedback, HotelReviewConstants.MaxFeedbackLength);

        modelBuilder.Entity<HotelReview>()
            .HasIndex(review => review.HotelId);

        return modelBuilder;
    }

    private static ModelBuilder ConfigureHotelEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurePropertyLength<Hotel>(
            hotel => hotel.Name, HotelConstants.MaxNameLength);
        modelBuilder.ConfigurePropertyLength<Hotel>(
            hotel => hotel.BriefDescription, HotelConstants.MaxBriefDescriptionLength);
        modelBuilder.ConfigurePropertyLength<Hotel>(
            hotel => hotel.DetailedDescription, HotelConstants.MaxDesctiptionLength);
        modelBuilder.ConfigurePropertyLength<Hotel>(
            hotel => hotel.OwnerName, HotelConstants.MaxNameLength);

        return modelBuilder;
    }

    private static ModelBuilder ConfigureDiscountEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurePropertyLength<Discount>(
            discount => discount.Reason, DiscountConstants.MaxReasonLength);
        modelBuilder.Entity<Discount>(discount =>
        {
            discount.HasIndex(discount => discount.StartingDate);
            discount.HasIndex(discount => discount.EndingDate);
        });

        return modelBuilder;
    }

    private static ModelBuilder ConfigureHotelVisitEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HotelVisit>()
            .HasIndex(visit => visit.CreationDate);

        return modelBuilder;
    }

    private static ModelBuilder ConfigureCityEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurePropertyLength<City>(
            city => city.Name, CityConstants.MaxNameLength);
        modelBuilder.ConfigurePropertyLength<City>(
            city => city.CountryName, CityConstants.MaxCountryNameLength);

        return modelBuilder;
    }

    private static ModelBuilder ConfigureCartEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>()
            .HasIndex(item => item.UserId);

        return modelBuilder;
    }
}