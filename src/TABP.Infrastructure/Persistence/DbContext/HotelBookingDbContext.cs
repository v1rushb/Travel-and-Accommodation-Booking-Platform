using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TABP.Domain.Entities;
using TABP.Domain.Models.Configurations;
using TABP.Infrastructure.Extensions;

namespace TABP.Infrastructure;

public class HotelBookingDbContext : DbContext
{
    private readonly ConnectionStrings _connectionStrings;
    
    public HotelBookingDbContext(IOptions<ConnectionStrings> connectionStrings)
    {
        _connectionStrings = connectionStrings.Value;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<HotelReview> HotelReviews { get; set; }
    public DbSet<HotelVisit> HotelVisits { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<RoomBooking> RoomBookings { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<RoomWithAvailability> RoomsWithAvailability { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_connectionStrings.SQLString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .MapViews()
            .ConfigureEntities()
            .ConfigureRelations()
            .SeedTables();
    }
}