using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;
using TABP.Infrastructure.Extensions.Helpers;

namespace TABP.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly ILogger<CityRepository> _logger;
    private readonly IMapper _mapper;

    public CityRepository(
        HotelBookingDbContext context,
        ILogger<CityRepository> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(CityDTO newCity) 
    {
        newCity.CreationDate = DateTime.UtcNow;
        newCity.ModificationDate = DateTime.UtcNow;

        var entityEntry = _context.Cities.Add(_mapper.Map<City>(newCity)); 

        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Cities.Remove(new City { Id = Id});  

        await _context.SaveChangesAsync();
        _logger.LogInformation($"City with discount Id: {Id} has been deleted");
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Cities.AnyAsync(city => city.Id == Id);

    public async Task<CityDTO?> GetByIdAsync(Guid Id) =>
        _mapper.Map<CityDTO>(await _context.Cities.FirstOrDefaultAsync(city => city.Id == Id));

    public async Task UpdateAsync(CityDTO updatedCity) 
    {
        _context.Cities.Update(_mapper.Map<City>(updatedCity));
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(
        Expression<Func<City, bool>> predicate,
        int pageNumber,
        int pageSize)
        {
            var cities = await _context.Cities
                .Include(city => city.Hotels)
                .Where(predicate)
                .PaginateAsync(
                    pageNumber,
                    pageSize);

            return _mapper.Map<IEnumerable<CitySearchResponseDTO>>(cities);
        }

    public async Task<IEnumerable<CityAdminResponseDTO>> SearchForAdminAsync(
        Expression<Func<City, bool>> predicate,
        int pageNumber,
        int pageSize) 
    {
        var cities = await _context.Cities
            .Where(predicate)
            .PaginateAsync(pageNumber, pageSize);

        return _mapper.Map<IEnumerable<CityAdminResponseDTO>>(cities);
    }

    public async Task<bool> ExistsByNameAndCountryAsync(string name, string country) => // check if could be replaced later.
        await _context.Cities
            .AnyAsync(city => EF.Functions.Like(city.Name, name) 
                && EF.Functions.Like(city.CountryName, country));

}