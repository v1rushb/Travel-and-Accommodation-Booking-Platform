using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.User;

namespace TABP.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<UserRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Guid> AddAsync(UserDTO newUser)
    {
        var user = _mapper.Map<User>(newUser);

        var entityEntry = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Created User with Username: {user.Username}");
        return entityEntry.Entity.Id;
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Users.AnyAsync(user => user.Id == Id);

    public async Task<UserDTO> GetByIdAsync(Guid Id)
    {
        return _mapper.Map<UserDTO>(await _context.Users.FirstOrDefaultAsync(user => user.Id == Id));
    }

    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _context.Users.AnyAsync(user => user.Username == username);

    public async Task<UserDTO> GetByUsernameWithRolesAsync(string username) =>
       _mapper.Map<UserDTO>(await _context.Users
            .Include(users => users.Roles)
            .FirstOrDefaultAsync(user => user.Username == username));
}