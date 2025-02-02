using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Constants.Room;
using TABP.Domain.Enums;
using TABP.Domain.Models.Room;

namespace TABP.Application.Validators.Room;

public class RoomValidator : AbstractValidator<RoomDTO>
{
    public RoomValidator(IHotelRepository hotelRepository)
    {
        RuleFor(room => room.Number)
            .NotNull();
        

        RuleFor(room => room.AdultsCapacity)
            .NotNull()
            .InclusiveBetween(RoomConstants.MinAdultsCapacity, RoomConstants.MaxAdultsCapacity); 

        RuleFor(room => room.ChildrenCapacity)
            .NotNull()
            .InclusiveBetween(RoomConstants.MinChildrenCapacity, RoomConstants.MaxChildrenCapacity);

        RuleFor(room => room.PricePerNight)
            .NotNull()
            .GreaterThanOrEqualTo(RoomConstants.MinPricePerNight);
        
        RuleFor(room => room.HotelId)
            .NotNull()
            .MustAsync(async (id, cancellation) =>
                await hotelRepository.ExistsAsync(id))
            .WithMessage("{PropertyName} does not exist");
        
        RuleFor(room => room.Type)
            .Must(type => Enum.IsDefined(typeof(RoomType), type))
            .WithMessage("Invalid RoomType. Allowed values: Pending, Approved, Rejected.");
    }
}