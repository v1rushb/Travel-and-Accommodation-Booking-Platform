using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TAB.Domain.Constants.Discount;
using TABP.Domain.Constants.City;
using TABP.Domain.Constants.Hotel;
using TABP.Domain.Constants.Review;
using TABP.Domain.Constants.User;
using TABP.Domain.Entities;
using TABP.Domain.Enums;

namespace TABP.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    private static readonly DateTime _fixedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static ModelBuilder MapViews(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableRoom>()
        .ToView("vw_AvailableRooms")
        .HasKey(r => r.Id);

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

    public static void SeedTables(this ModelBuilder modelBuilder)
    {
        SeedCities(modelBuilder);
        var hotels = SeedHotels(modelBuilder);
        SeedRooms(modelBuilder);
        SeedDiscounts(modelBuilder, hotels);
        SeedBookings(modelBuilder);
        SeedHotelVisits(modelBuilder, hotels);
        SeedHotelReviews(modelBuilder, hotels);
    }

    private static void SeedCities(ModelBuilder modelBuilder)
    {
        var cities = new List<City>
        {
            new City { 
                Id = Guid.Parse("7CA4B1AA-FA9C-40A2-5601-08DD46916B70"), 
                Name = "Paris", 
                CountryName = "France", 
                CreationDate = _fixedDate.AddYears(-2), 
                ModificationDate = _fixedDate.AddMonths(-1) 
            },
            new City { 
                Id = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096"), 
                Name = "London", 
                CountryName = "UK", 
                CreationDate = _fixedDate.AddYears(-3), 
                ModificationDate = _fixedDate.AddMonths(-2) 
            },
            new City { 
                Id = Guid.Parse("FE33E674-6C47-49DC-C1F7-08DD46922384"), 
                Name = "New York", 
                CountryName = "USA", 
                CreationDate = _fixedDate.AddYears(-1), 
                ModificationDate = _fixedDate.AddMonths(-3) 
            },
            new City {
                Id = Guid.Parse("E6554375-0932-462C-0207-08DD388DC7B9"),
                Name = "Hebron",
                CountryName = "Palestine",
                CreationDate = DateTime.Parse("2025-01-19 13:32:54.831"),
                ModificationDate = DateTime.Parse("2025-01-19 13:32:54.831")
            },
            new City {
                Id = Guid.Parse("F3EBB6DE-2C8F-42FC-0208-08DD388DC7B9"),
                Name = "Safad",
                CountryName = "Palestine",
                CreationDate = DateTime.Parse("2025-01-19 13:33:39.429"),
                ModificationDate = DateTime.Parse("2025-01-19 13:33:39.429")
            },
            new City {
                Id = Guid.Parse("25EECA82-C189-4BBB-0209-08DD388DC7B9"),
                Name = "Amman",
                CountryName = "Jordan",
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187")
            },
        };

        modelBuilder.Entity<City>().HasData(cities);
    }

    private static List<Hotel> SeedHotels(ModelBuilder modelBuilder)
    {
        var hotels = new List<Hotel>
        {
            new Hotel
            {
                Id = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096"),
                Name = "citizenM Tower Of London hotel",
                BriefDescription = "A 4-star hotel located above Tower Hill Underground Station.",
                DetailedDescription = "Experience luxury and convenience...",
                StarRating = 4,
                OwnerName = "citizenM",
                Geolocation = "51.510223410295524,-0.07644353237381915",
                CreationDate = DateTime.Parse("2024-02-09 18:54:53.6850569"),
                ModificationDate = DateTime.Parse("2024-02-09 20:45:21.0122021"),
                CityId = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096")
            },
            new Hotel
            {
                Id = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                Name = "Pullman Paris Tour Eiffel",
                BriefDescription = "A 4-star hotel offering panoramic views of Paris.",
                DetailedDescription = "The 4-star Pullman Paris Tour Eiffel hotel...",
                StarRating = 4.4m,
                OwnerName = "Pullman",
                Geolocation = "48.85567419020331,2.2928680490125637",
                CreationDate = DateTime.Parse("2024-02-09 18:56:58.1733565"),
                ModificationDate = DateTime.Parse("2024-02-09 18:56:58.1733570"),
                CityId = Guid.Parse("7CA4B1AA-FA9C-40A2-5601-08DD46916B70")
            },
            new Hotel
            {
                Id = Guid.Parse("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"),
                Name = "Hilton New York Times Square",
                BriefDescription = "A 4-star hotel in the heart of Times Square.",
                DetailedDescription = "Stay in the heart of Times Square...",
                StarRating = 4.2m,
                OwnerName = "Hilton",
                Geolocation = "40.7566480079687,-73.98881546193508",
                CreationDate = DateTime.Parse("2024-02-09 19:03:10.5885700"),
                ModificationDate = DateTime.Parse("2024-02-09 19:03:10.5885704"),
                CityId = Guid.Parse("FE33E674-6C47-49DC-C1F7-08DD46922384")
            },
            new Hotel
            {
                Id = Guid.Parse("75ae0504-974c-4ff2-ab13-c30374ac8558"),
                Name = "Abu Mazen",
                BriefDescription = "A 4-star hotel",
                DetailedDescription = "Stay in the heart of Hebron",
                StarRating = 4.2m,
                OwnerName = "Abu Mazen maybe",
                Geolocation = "42.7566480079687,-74.98881546193508",
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                CityId = Guid.Parse("E6554375-0932-462C-0207-08DD388DC7B9")
            },
            new Hotel
            {
                Id = Guid.Parse("d9123022-25c0-4493-b5eb-b11cfd829554"),
                Name = "Burj Herbawi",
                BriefDescription = "A 4-star hotel in Amman",
                DetailedDescription = "Stay in the heart of Amman",
                StarRating = 4.4m,
                OwnerName = "Bashar",
                Geolocation = "40.7566480079687,-73.98881546193508",
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                CityId = Guid.Parse("25EECA82-C189-4BBB-0209-08DD388DC7B9")
            },
            new Hotel
            {
                Id = Guid.Parse("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"),
                Name = "Burj Herbawi 2",
                BriefDescription = "A 4-star hotel in Hebron",
                DetailedDescription = "Stay in the heart of Hebron",
                StarRating = 4.1m,
                OwnerName = "Bashar",
                Geolocation = "20.7566480079687,-7.98881546193508",
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                CityId = Guid.Parse("E6554375-0932-462C-0207-08DD388DC7B9")
            }
        };

        modelBuilder.Entity<Hotel>().HasData(hotels);

        return hotels;
    }

    private static void SeedDiscounts(ModelBuilder modelBuilder, List<Hotel> hotels)
    {
        var discounts = new List<Discount>
        {
            new Discount
            {
                Id = Guid.Parse("f0a22dda-4769-4509-913a-1be8a8d5b88f"),
                Reason = "New season",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                AmountPercentage = 10,
                roomType = RoomType.Luxury,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                HotelId = hotels[0].Id
            },
            new Discount
            {
                Id = Guid.Parse("335126f5-ea9a-49a0-978a-9503d04449db"),
                Reason = "New season",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                AmountPercentage = 15,
                roomType = RoomType.Luxury,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                HotelId = hotels[1].Id
            },
            new Discount
            {
                Id = Guid.Parse("3329cf25-8cc8-4034-b032-6e1db78e4dd9"),
                Reason = "New season",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                AmountPercentage = 17,
                roomType = RoomType.Luxury,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-03-19 13:34:10.187"),
                HotelId = hotels[2].Id
            },
            new Discount
            {
                Id = Guid.Parse("892944e1-6c20-4530-a865-250989e23248"),
                Reason = "New season",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                AmountPercentage = 15,
                roomType = RoomType.Luxury,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                HotelId = hotels[3].Id
            },
            new Discount
            {
                Id = Guid.Parse("027e91be-e337-4b48-ac33-a6ee6992d708"),
                Reason = "New season",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-19 13:34:10.187"),
                AmountPercentage = 30,
                roomType = RoomType.Luxury,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-02-09 19:06:19.6610000"),
                HotelId = hotels[4].Id
            },
            new Discount
            {
                Id = Guid.Parse("d7a473e6-ec2f-48d6-852a-2e68c9993f9b"),
                Reason = "Why not",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-09 19:06:19.6610000"),
                AmountPercentage = 30,
                roomType = RoomType.Budget,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                HotelId = hotels[4].Id
            },
            new Discount
            {
                Id = Guid.Parse("cda35c4b-d597-4186-afe7-26bd3af94397"),
                Reason = "New season",
                StartingDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                EndingDate = DateTime.Parse("2025-04-09 19:06:19.6610000"),
                AmountPercentage = 30,
                roomType = RoomType.Boutique,
                CreationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 13:34:10.187"),
                HotelId = hotels[4].Id
            }
        };

        modelBuilder.Entity<Discount>().HasData(discounts);
    }

    private static void SeedRooms(ModelBuilder modelBuilder)
    {
        var rooms = new List<Room>
        {
            new Room
            {
                Id = Guid.Parse("28E17D69-9E0E-44A3-2947-08DC29A4DE37"),
                Number = 1,
                Type = RoomType.Budget,
                AdultsCapacity = 2,
                ChildrenCapacity = 3,
                PricePerNight = 50,
                CreationDate = DateTime.Parse("2024-03-09 18:54:53.6850569"),
                ModificationDate = DateTime.Parse("2024-03-09 19:25:25.9245743"),
                HotelId = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096")
            },
            new Room
            {
                Id = Guid.Parse("A914BE7E-C545-4784-2948-08DC29A4DE37"),
                Number = 2,
                Type = RoomType.Luxury,
                AdultsCapacity = 2,
                ChildrenCapacity = 0,
                PricePerNight = 100,
                CreationDate = DateTime.Parse("2024-02-09 19:25:46.0762941"),
                ModificationDate = DateTime.Parse("2024-02-09 19:25:46.0762945"),
                HotelId = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096")
            },
            new Room
            {
                Id = Guid.Parse("B3F07752-2D97-4B2C-2949-08DC29A4DE37"),
                Number = 3,
                Type = RoomType.Budget,
                AdultsCapacity = 3,
                ChildrenCapacity = 2,
                PricePerNight = 75,
                CreationDate = DateTime.Parse("2024-02-09 19:26:03.5942376"),
                ModificationDate = DateTime.Parse("2024-02-09 19:26:03.5942379"),
                HotelId = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096")
            },
            new Room
            {
                Id = Guid.Parse("F15BD95C-8746-4236-294A-08DC29A4DE37"),
                Number = 1,
                Type = RoomType.Budget,
                AdultsCapacity = 3,
                ChildrenCapacity = 2,
                PricePerNight = 75,
                CreationDate = DateTime.Parse("2024-02-09 19:26:22.3839118"),
                ModificationDate = DateTime.Parse("2024-02-09 19:26:22.3839124"),
                HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F")
            },
            new Room
            {
                Id = Guid.Parse("E8A1A3E6-D8DA-4928-294B-08DC29A4DE37"),
                Number = 2,
                Type = RoomType.Luxury,
                AdultsCapacity = 2,
                ChildrenCapacity = 0,
                PricePerNight = 100,
                CreationDate = DateTime.Parse("2024-02-09 19:26:39.9723640"),
                ModificationDate = DateTime.Parse("2024-02-09 19:26:39.9723645"),
                HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F")
            },
            new Room
            {
                Id = Guid.Parse("B3F52184-3330-4750-294C-08DC29A4DE37"),
                Number = 1,
                Type = RoomType.Budget,
                AdultsCapacity = 2,
                ChildrenCapacity = 3,
                PricePerNight = 50,
                CreationDate = DateTime.Parse("2024-02-09 19:26:58.9308602"),
                ModificationDate = DateTime.Parse("2024-02-09 19:26:58.9308604"),
                HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
            },
            new Room
            {
                Id = Guid.Parse("F339B369-2A05-4EEA-294D-08DC29A4DE37"),
                Number = 1,
                Type = RoomType.Budget,
                AdultsCapacity = 2,
                ChildrenCapacity = 3,
                PricePerNight = 50,
                CreationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
            },
            new Room
            {
                Id = Guid.Parse("601A0D84-0435-4221-294E-08DC29A4DE37"),
                Number = 2,
                Type = RoomType.Budget,
                AdultsCapacity = 2,
                ChildrenCapacity = 0,
                PricePerNight = 100,
                CreationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
            },
            new Room
            {
                Id = Guid.Parse("3914316B-1B87-46F7-294F-08DC29A4DE37"),
                Number = 3,
                Type = RoomType.Budget,
                AdultsCapacity = 2,
                ChildrenCapacity = 2,
                PricePerNight = 75,
                CreationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
            },

            new Room
            {
                Id = Guid.Parse("C9A9F0D0-1111-4444-8888-1234567890AB"),
                Number = 4,
                Type = RoomType.Luxury,
                AdultsCapacity = 4,
                ChildrenCapacity = 1,
                PricePerNight = 200,
                CreationDate = DateTime.Parse("2025-01-09 20:00:00"),
                ModificationDate = DateTime.Parse("2025-01-09 20:00:00"),
                HotelId = Guid.Parse("45E0DCB1-62AF-409A-8349-08DD4691B096")
            },

            new Room
            {
                Id = Guid.Parse("D1E2F3A4-B5C6-7890-D1E2-F3A4B5C67890"),
                Number = 3,
                Type = RoomType.Luxury,
                AdultsCapacity = 2,
                ChildrenCapacity = 1,
                PricePerNight = 180,
                CreationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F")
            },

            new Room
            {
                Id = Guid.Parse("E2F3A4B5-C6D7-8901-E2F3-A4B5C6D78901"),
                Number = 4,
                Type = RoomType.Luxury,
                AdultsCapacity = 3,
                ChildrenCapacity = 1,
                PricePerNight = 220,
                CreationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                ModificationDate = DateTime.Parse("2025-01-19 14:34:10.187"),
                HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
            }

        };

        modelBuilder.Entity<Room>().HasData(rooms);
    }

    private static void SeedBookings(ModelBuilder modelBuilder)
    {
        var bookings = new List<RoomBooking>()
        {
            new RoomBooking
            {
                Id = Guid.Parse("F7422CE9-5E89-4B49-08CA-08DD47669C80"),
                CreationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                ModificationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                CheckInDate = DateTime.Parse("2025-02-10 12:12:54.295"),
                CheckOutDate = DateTime.Parse("2025-02-19 13:08:36.295"),
                RoomId = Guid.Parse("D1E2F3A4-B5C6-7890-D1E2-F3A4B5C67890"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("61237e1a-928b-494a-be63-d9b562a65896"),
                CreationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                ModificationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                CheckInDate = DateTime.Parse("2025-03-06 12:12:54.295"),
                CheckOutDate = DateTime.Parse("2025-03-09 13:08:36.295"),
                RoomId = Guid.Parse("E2F3A4B5-C6D7-8901-E2F3-A4B5C6D78901"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("97d8a55f-ac24-4e02-87f7-48b2a101c0c1"),
                CreationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                ModificationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                CheckInDate = DateTime.Parse("2025-04-06 12:12:54.295"),
                CheckOutDate = DateTime.Parse("2025-05-09 13:08:36.295"),
                RoomId = Guid.Parse("D1E2F3A4-B5C6-7890-D1E2-F3A4B5C67890"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("6401b407-35d4-4fe6-a1ba-ecf945440fbf"),
                CreationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                ModificationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                CheckInDate = DateTime.Parse("2025-01-06 12:12:54.295"),
                CheckOutDate = DateTime.Parse("2025-01-09 13:08:36.295"),
                RoomId = Guid.Parse("C9A9F0D0-1111-4444-8888-1234567890AB"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("bdb25542-aad6-41fc-9c5d-61debda238e0"),
                CreationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                ModificationDate = DateTime.Parse("2025-02-07 11:00:19.389"),
                CheckInDate = DateTime.Parse("2025-01-06 12:12:54.295"),
                CheckOutDate = DateTime.Parse("2025-01-10 13:08:36.295"),
                RoomId = Guid.Parse("C9A9F0D0-1111-4444-8888-1234567890AB"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("A1B2C3D4-E5F6-4789-1011-121314151617"),
                CreationDate = DateTime.Parse("2024-11-15 09:30:00"),
                ModificationDate = DateTime.Parse("2024-11-15 09:30:00"),
                CheckInDate = DateTime.Parse("2024-12-20 14:00:00"),
                CheckOutDate = DateTime.Parse("2024-12-24 11:00:00"),
                RoomId = Guid.Parse("28E17D69-9E0E-44A3-2947-08DC29A4DE37"),
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("B7C8D9E0-F1A2-4B5C-1314-151617181920"),
                CreationDate = DateTime.Parse("2024-12-01 14:45:00"),
                ModificationDate = DateTime.Parse("2024-12-01 14:45:00"),
                CheckInDate = DateTime.Parse("2025-01-15 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-01-22 11:00:00"),
                RoomId = Guid.Parse("A914BE7E-C545-4784-2948-08DC29A4DE37"),
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("C3D4E5F6-A7B8-4D9E-1617-181920212223"),
                CreationDate = DateTime.Parse("2025-01-10 10:10:00"),
                ModificationDate = DateTime.Parse("2025-01-10 10:10:00"),
                CheckInDate = DateTime.Parse("2025-02-28 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-03-02 11:00:00"),
                RoomId = Guid.Parse("F15BD95C-8746-4236-294A-08DC29A4DE37"),
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("D5E6F7A8-B9C0-4E1F-2021-222324252627"),
                CreationDate = DateTime.Parse("2025-03-15 16:20:00"),
                ModificationDate = DateTime.Parse("2025-03-15 16:20:00"),
                CheckInDate = DateTime.Parse("2025-04-10 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-04-15 11:00:00"),
                RoomId = Guid.Parse("E8A1A3E6-D8DA-4928-294B-08DC29A4DE37"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("E1F2A3B4-C5D6-4F7A-2324-252627282930"),
                CreationDate = DateTime.Parse("2025-04-01 11:55:00"),
                ModificationDate = DateTime.Parse("2025-04-01 11:55:00"),
                CheckInDate = DateTime.Parse("2025-05-01 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-05-08 11:00:00"),
                RoomId = Guid.Parse("B3F52184-3330-4750-294C-08DC29A4DE37"),
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("F9A0B1C2-D3E4-418B-2627-282930313233"),
                CreationDate = DateTime.Parse("2025-05-20 08:00:00"),
                ModificationDate = DateTime.Parse("2025-05-20 08:00:00"),
                CheckInDate = DateTime.Parse("2025-06-10 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-06-12 11:00:00"),
                RoomId = Guid.Parse("601A0D84-0435-4221-294E-08DC29A4DE37"),
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("1A2B3C4D-E5F6-4C92-3031-323334353637"),
                CreationDate = DateTime.Parse("2025-06-15 19:15:00"),
                ModificationDate = DateTime.Parse("2025-06-15 19:15:00"),
                CheckInDate = DateTime.Parse("2025-07-01 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-07-05 11:00:00"),
                RoomId = Guid.Parse("3914316B-1B87-46F7-294F-08DC29A4DE37"),
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("2B3C4D5E-F6A7-4D0A-3334-353637383940"),
                CreationDate = DateTime.Parse("2025-07-01 13:00:00"),
                ModificationDate = DateTime.Parse("2025-07-01 13:00:00"),
                CheckInDate = DateTime.Parse("2025-08-15 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-08-25 11:00:00"),
                RoomId = Guid.Parse("C9A9F0D0-1111-4444-8888-1234567890AB"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("3C4D5E6F-A7B8-4E1B-3637-383940414243"),
                CreationDate = DateTime.Parse("2025-08-10 21:40:00"),
                ModificationDate = DateTime.Parse("2025-08-10 21:40:00"),
                CheckInDate = DateTime.Parse("2025-09-01 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-09-04 11:00:00"),
                RoomId = Guid.Parse("D1E2F3A4-B5C6-7890-D1E2-F3A4B5C67890"),
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("4D5E6F7A-B8C9-4F2C-3940-414243444546"),
                CreationDate = DateTime.Parse("2025-09-01 07:20:00"),
                ModificationDate = DateTime.Parse("2025-09-01 07:20:00"),
                CheckInDate = DateTime.Parse("2025-10-20 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-10-30 11:00:00"),
                RoomId = Guid.Parse("E2F3A4B5-C6D7-8901-E2F3-A4B5C6D78901"),
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93"),
                Notes = "Yoink"
            },
             new RoomBooking
            {
                Id = Guid.Parse("5E6F7A8B-C9D0-4D3D-4243-444546474849"),
                CreationDate = DateTime.Parse("2025-10-15 15:50:00"),
                ModificationDate = DateTime.Parse("2025-10-15 15:50:00"),
                CheckInDate = DateTime.Parse("2025-11-05 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-11-07 11:00:00"),
                RoomId = Guid.Parse("28E17D69-9E0E-44A3-2947-08DC29A4DE37"),
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("6F7A8B9C-D1E2-4E4E-4546-474849505152"),
                CreationDate = DateTime.Parse("2025-11-01 18:30:00"),
                ModificationDate = DateTime.Parse("2025-11-01 18:30:00"),
                CheckInDate = DateTime.Parse("2025-12-22 14:00:00"),
                CheckOutDate = DateTime.Parse("2025-12-25 11:00:00"),
                RoomId = Guid.Parse("A914BE7E-C545-4784-2948-08DC29A4DE37"),
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("7A8B9C0D-E2F3-4F5F-4849-505152535455"),
                CreationDate = DateTime.Parse("2025-12-10 22:00:00"),
                ModificationDate = DateTime.Parse("2025-12-10 22:00:00"),
                CheckInDate = DateTime.Parse("2026-01-10 14:00:00"),
                CheckOutDate = DateTime.Parse("2026-01-12 11:00:00"),
                RoomId = Guid.Parse("F15BD95C-8746-4236-294A-08DC29A4DE37"),
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1"),
                Notes = "Yoink"
            },
             new RoomBooking
            {
                Id = Guid.Parse("8B9C0D1E-F3A4-416A-5152-535455565758"),
                CreationDate = DateTime.Parse("2026-01-05 11:11:00"),
                ModificationDate = DateTime.Parse("2026-01-05 11:11:00"),
                CheckInDate = DateTime.Parse("2026-02-01 14:00:00"),
                CheckOutDate = DateTime.Parse("2026-02-05 11:00:00"),
                RoomId = Guid.Parse("E8A1A3E6-D8DA-4928-294B-08DC29A4DE37"),
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93"),
                Notes = "Yoink"
            },
            new RoomBooking
            {
                Id = Guid.Parse("9C0D1E2F-A4B5-4B7B-5455-565758596061"),
                CreationDate = DateTime.Parse("2026-02-20 17:45:00"),
                ModificationDate = DateTime.Parse("2026-02-20 17:45:00"),
                CheckInDate = DateTime.Parse("2026-03-15 14:00:00"),
                CheckOutDate = DateTime.Parse("2026-03-20 11:00:00"),
                RoomId = Guid.Parse("B3F52184-3330-4750-294C-08DC29A4DE37"),
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75"),
                Notes = "Yoink"
            }
        };
        modelBuilder.Entity<RoomBooking>().HasData(bookings);
    }

    private static void SeedHotelVisits(ModelBuilder modelBuilder, List<Hotel> hotels)
    {
        var hotelVisits = new List<HotelVisit>
        {
            new HotelVisit
            {
                Id = Guid.Parse("B1A2C3D4-E5F6-4789-9A0B-C1D2E3F4A5B6"),
                CreationDate = DateTime.Parse("2024-12-18 10:00:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit
            {
                Id = Guid.Parse("C7D8E9F0-1A2B-4C5D-3E4F-5A6B7C8D9E0F"),
                CreationDate = DateTime.Parse("2025-01-10 14:30:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit
            {
                Id = Guid.Parse("D3E4F5A6-B7C8-4D9E-5F0A-1B2C3D4E5F6A"),
                CreationDate = DateTime.Parse("2025-02-01 09:15:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("E9F0A1B2-C3D4-4E5F-6A7B-8C9D0E1F2A3B"),
                CreationDate = DateTime.Parse("2025-03-15 16:45:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit
            {
                Id = Guid.Parse("F5A6B7C8-D9E0-4F1A-7B8C-9D0E1F2A3B4C"),
                CreationDate = DateTime.Parse("2025-04-20 11:20:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit
            {
                Id = Guid.Parse("1B2C3D4E-F5A6-487B-8C9D-0E1F2A3B4C5D"),
                CreationDate = DateTime.Parse("2025-05-10 19:50:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit
            {
                Id = Guid.Parse("2C3D4E5F-6A7B-498C-9D0E-1F2A3B4C5D6E"),
                CreationDate = DateTime.Parse("2024-12-19 12:30:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit
            {
                Id = Guid.Parse("3D4E5F6A-7B8C-4A9D-0E1F-2A3B4C5D6E7F"),
                CreationDate = DateTime.Parse("2025-03-01 15:40:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("4E5F6A7B-8C9D-4B0E-1F2A-3B4C5D6E7F8A"),
                CreationDate = DateTime.Parse("2025-05-02 17:00:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit
            {
                Id = Guid.Parse("5F6A7B8C-9D0E-4C1F-2A3B-4C5D6E7F8A9B"),
                CreationDate = DateTime.Parse("2025-07-10 08:55:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit
            {
                Id = Guid.Parse("6A7B8C9D-0E1F-4D2A-3B4C-5D6E7F8A9B0C"),
                CreationDate = DateTime.Parse("2025-09-03 13:25:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("7B8C9D0E-1F2A-4E3B-4C5D-6E7F8A9B0C1D"),
                CreationDate = DateTime.Parse("2025-11-18 20:10:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit
            {
                Id = Guid.Parse("8C9D0E1F-2A3B-4F4C-5D6E-7F8A9B0C1D2E"),
                CreationDate = DateTime.Parse("2025-01-18 11:40:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit
            {
                Id = Guid.Parse("9D0E1F2A-3B4C-455D-6E7F-8A9B0C1D2E3F"),
                CreationDate = DateTime.Parse("2025-02-25 09:00:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("0E1F2A3B-4C5D-466E-7F8A-9B0C1D2E3F4A"),
                CreationDate = DateTime.Parse("2025-04-12 14:20:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit
            {
                Id = Guid.Parse("1F2A3B4C-5D6E-477F-8A9B-0C1D2E3F4A5B"),
                CreationDate = DateTime.Parse("2025-06-05 18:10:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("2A3B4C5D-6E7F-488A-9B0C-1D2E3F4A5B6C"),
                CreationDate = DateTime.Parse("2025-08-01 22:50:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit
            {
                Id = Guid.Parse("3B4C5D6E-7F8A-499B-0C1D-2E3F4A5B6C7D"),
                CreationDate = DateTime.Parse("2025-10-25 10:30:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit
            {
                Id = Guid.Parse("4C5D6E7F-8A9B-4A0C-1D2E-3F4A5B6C7D8E"),
                CreationDate = DateTime.Parse("2025-02-20 13:55:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit
            {
                Id = Guid.Parse("5D6E7F8A-9B0C-4B1D-2E3F-4A5B6C7D8E9F"),
                CreationDate = DateTime.Parse("2025-04-05 16:10:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("6E7F8A9B-0C1D-4C2E-3F4A-5B6C7D8E9F0A"),
                CreationDate = DateTime.Parse("2025-06-12 09:45:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit
            {
                Id = Guid.Parse("7F8A9B0C-1D2E-4D3F-4A5B-6C7D8E9F0A1B"),
                CreationDate = DateTime.Parse("2025-08-18 15:30:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("8A9B0C1D-2E3F-4E4A-5B6C-7D8E9F0A1B2C"),
                CreationDate = DateTime.Parse("2025-10-01 21:00:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("a1a1a1a1-b2b2-c3c3-d4d4-e5e5e5e5e5e5"),
                CreationDate = DateTime.Parse("2026-01-15 17:20:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("b1b1b1b1-c2c2-d3d3-e4e4-f5f5f5f5f5f5"),
                CreationDate = DateTime.Parse("2026-02-10 08:30:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("c1c1c1c1-d2d2-e3e3-f4f4-a5a5a5a5a5a5"),
                CreationDate = DateTime.Parse("2026-03-01 19:40:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("d1d1d1d1-e2e2-f3f3-a4a4-b5b5b5b5b5b5"),
                CreationDate = DateTime.Parse("2026-01-20 14:50:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("e1e1e1e1-f2f2-a3a3-b4b4-c5c5c5c5c5c5"),
                CreationDate = DateTime.Parse("2026-02-15 11:00:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("f1f1f1f1-a2a2-b3b3-c4c4-d5d5d5d5d5d5"),
                CreationDate = DateTime.Parse("2026-01-25 09:10:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelVisit()
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                CreationDate = DateTime.Parse("2026-02-28 16:30:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
        };

        modelBuilder.Entity<HotelVisit>().HasData(hotelVisits);
    }

    private static void SeedHotelReviews(ModelBuilder modelBuilder, List<Hotel> hotels)
    {
        var hotelReviews = new List<HotelReview>
        {
            new HotelReview
            {
                Id = Guid.Parse("A1B2C3D4-E5F6-4789-1A2B-C3D4E5F6A7B8"),
                Feedback = "Great location, modern and clean rooms.",
                Rating = HotelRating.Excellent,
                CreationDate = DateTime.Parse("2025-03-10 14:20:00"),
                ModificationDate = DateTime.Parse("2025-03-10 14:20:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelReview
            {
                Id = Guid.Parse("B2C3D4E5-F6A7-489A-2B3C-D4E5F6A7B8C9"),
                Feedback = "Sleek hotel with friendly staff and amazing views.",
                Rating = HotelRating.VeryGood,
                CreationDate = DateTime.Parse("2025-04-15 18:30:00"),
                ModificationDate = DateTime.Parse("2025-04-15 18:30:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelReview
            {
                Id = Guid.Parse("C3D4E5F6-A7B8-49AB-3C4D-E5F6A7B8C9D0"),
                Feedback = "Excellent stay, breakfast was delicious.",
                Rating = HotelRating.Excellent,
                CreationDate = DateTime.Parse("2025-05-22 09:45:00"),
                ModificationDate = DateTime.Parse("2025-05-22 09:45:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelReview
            {
                Id = Guid.Parse("D4E5F6A7-B8C9-4BCD-4D5E-F6A7B8C9D0E1"),
                Feedback = "A bit noisy but overall a good experience.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-06-01 16:00:00"),
                ModificationDate = DateTime.Parse("2025-06-01 16:00:00"),
                HotelId = hotels[0].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelReview
            {
                Id = Guid.Parse("E5F6A7B8-C9D0-4CDE-5E6F-A7B8C9D0E1F2"),
                Feedback = "Unbeatable views of the Eiffel Tower, luxurious.",
                Rating = HotelRating.Excellent,
                CreationDate = DateTime.Parse("2025-04-01 12:10:00"),
                ModificationDate = DateTime.Parse("2025-04-01 12:10:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelReview
            {
                Id = Guid.Parse("F6A7B8C9-D0E1-4DEF-6F7A-B8C9D0E1F2A3"),
                Feedback = "Wonderful location, rooms are very comfortable.",
                Rating = HotelRating.VeryGood,
                CreationDate = DateTime.Parse("2025-05-10 20:40:00"),
                ModificationDate = DateTime.Parse("2025-05-10 20:40:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelReview
            {
                Id = Guid.Parse("1A2B3C4D-E5F6-4DEF-7A8B-C9D0E1F2A3B4"),
                Feedback = "Service was top-notch, highly recommend.",
                Rating = HotelRating.Excellent,
                CreationDate = DateTime.Parse("2025-06-18 11:30:00"),
                ModificationDate = DateTime.Parse("2025-06-18 11:30:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelReview
            {
                Id = Guid.Parse("2B3C4D5E-F6A7-4FAE-8B9C-0D1E2F3A4B5C"),
                Feedback = "A bit pricey but worth it for the view.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-07-05 17:15:00"),
                ModificationDate = DateTime.Parse("2025-07-05 17:15:00"),
                HotelId = hotels[1].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelReview
            {
                Id = Guid.Parse("3C4D5E6F-A7B8-4FBF-9C0D-1E2F3A4B5C6D"),
                Feedback = "Perfect location for exploring Times Square.",
                Rating = HotelRating.VeryGood,
                CreationDate = DateTime.Parse("2025-05-01 08:50:00"),
                ModificationDate = DateTime.Parse("2025-05-01 08:50:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelReview
            {
                Id = Guid.Parse("4D5E6F7A-B8C9-4CC0-0D1E-2F3A4B5C6D7E"),
                Feedback = "Comfortable and clean rooms, great amenities.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-06-12 13:25:00"),
                ModificationDate = DateTime.Parse("2025-06-12 13:25:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelReview
            {
                Id = Guid.Parse("5E6F7A8B-C9D0-4CD1-1E2F-3A4B5C6D7E8F"),
                Feedback = "Good for a short stay, a bit busy though.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-07-20 10:10:00"),
                ModificationDate = DateTime.Parse("2025-07-20 10:10:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelReview
            {
                Id = Guid.Parse("6F7A8B9C-D0E1-4CE2-2F3A-4B5C6D7E8F9A"),
                Feedback = "Location is amazing but hotel feels dated.",
                Rating = HotelRating.VeryGood,
                CreationDate = DateTime.Parse("2025-08-01 19:55:00"),
                ModificationDate = DateTime.Parse("2025-08-01 19:55:00"),
                HotelId = hotels[2].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelReview
            {
                Id = Guid.Parse("7A8B9C0D-E1F2-4CF3-3A4B-5C6D7E8F9A0B"),
                Feedback = "Authentic experience, great hospitality.",
                Rating = HotelRating.VeryGood,
                CreationDate = DateTime.Parse("2025-06-01 15:40:00"),
                ModificationDate = DateTime.Parse("2025-06-01 15:40:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelReview
            {
                Id = Guid.Parse("8B9C0D1E-F2A3-4D0A-4B5C-6D7E8F9A0B1C"),
                Feedback = "Simple but clean, friendly staff.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-07-12 21:00:00"),
                ModificationDate = DateTime.Parse("2025-07-12 21:00:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelReview
            {
                Id = Guid.Parse("9C0D1E2F-A3B4-4D1B-5C6D-7E8F9A0B1C2D"),
                Feedback = "Value for money, good for budget travelers.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-08-19 12:50:00"),
                ModificationDate = DateTime.Parse("2025-08-19 12:50:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelReview
            {
                Id = Guid.Parse("0D1E2F3A-B4C5-4D2C-6D7E-8F9A0B1C2D3E"),
                Feedback = "Location is not ideal for tourists, but okay for business.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-09-02 18:20:00"),
                ModificationDate = DateTime.Parse("2025-09-02 18:20:00"),
                HotelId = hotels[3].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelReview
            {
                Id = Guid.Parse("1E2F3A4B-C5D6-4D3D-7E8F-9A0B1C2D3E4F"),
                Feedback = "Great location in Amman, close to everything.",
                Rating = HotelRating.VeryGood,
                CreationDate = DateTime.Parse("2025-07-01 11:15:00"),
                ModificationDate = DateTime.Parse("2025-07-01 11:15:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelReview
            {
                Id = Guid.Parse("2F3A4B5C-D6E7-4D4E-8F9A-0B1C2D3E4F5A"),
                Feedback = "Helpful staff and comfortable rooms.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-08-10 15:55:00"),
                ModificationDate = DateTime.Parse("2025-08-10 15:55:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelReview
            {
                Id = Guid.Parse("3A4B5C6D-E7F8-4D5F-9A0B-1C2D3E4F5A6B"),
                Feedback = "Breakfast was okay, but overall a pleasant stay.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-09-25 09:05:00"),
                ModificationDate = DateTime.Parse("2025-09-25 09:05:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelReview
            {
                Id = Guid.Parse("4B5C6D7E-F8A9-4D6A-0B1C-2D3E4F5A6B7C"),
                Feedback = "Could be cleaner, but decent for the price.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-10-05 14:30:00"),
                ModificationDate = DateTime.Parse("2025-10-05 14:30:00"),
                HotelId = hotels[4].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            },
            new HotelReview
            {
                Id = Guid.Parse("5C6D7E8F-9A0B-4D7B-1C2D-3E4F5A6B7C8D"),
                Feedback = "Quiet location, good for relaxing.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-08-01 10:00:00"),
                ModificationDate = DateTime.Parse("2025-08-01 10:00:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("5E91FD72-53C3-43EE-3D87-08DD3498AAA5")
            },
            new HotelReview
            {
                Id = Guid.Parse("6D7E8F9A-0B1C-4D8C-2D3E-4F5A6B7C8D9E"),
                Feedback = "Basic amenities, suitable for a short stay.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-09-15 16:40:00"),
                ModificationDate = DateTime.Parse("2025-09-15 16:40:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("39AD172A-602E-4118-29D9-08DD398180C1")
            },
            new HotelReview
            {
                Id = Guid.Parse("7E8F9A0B-1C2D-4D9D-3E4F-5A6B7C8D9E0F"),
                Feedback = "Friendly staff, but facilities are limited.",
                Rating = HotelRating.Good,
                CreationDate = DateTime.Parse("2025-10-22 11:20:00"),
                ModificationDate = DateTime.Parse("2025-10-22 11:20:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("923043B5-E9C8-4DED-9CFA-08DD3E211F93")
            },
            new HotelReview
            {
                Id = Guid.Parse("8F9A0B1C-2D3E-4DAE-4F5A-6B7C8D9E0F1A"),
                Feedback = "Not the best, needs improvement.",
                Rating = HotelRating.Poor,
                CreationDate = DateTime.Parse("2025-11-01 20:00:00"),
                ModificationDate = DateTime.Parse("2025-11-01 20:00:00"),
                HotelId = hotels[5].Id,
                UserId = Guid.Parse("F82BFF97-D6D4-4D47-424E-08DD3E217C75")
            }
        };
        modelBuilder.Entity<HotelReview>().HasData(hotelReviews);
    }
}