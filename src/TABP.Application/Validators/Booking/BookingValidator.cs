using System.Data;
using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.Booking;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Validators.Booking;

internal class BookingValidator : AbstractValidator<RoomBookingDTO>
{
    public BookingValidator(
        IUserRepository userRepository,
        IRoomRepository roomRepository,
        IRoomBookingRepository bookingRepository)
    {
        RuleFor(booking => booking.UserId)
            .NotNull()
            .MustAsync(async (userId, cancellation) => await userRepository.ExistsAsync(userId))
            .WithMessage("{PropertyName} does not exist."); // try that later.
        
        RuleFor(booking => booking.RoomId)
            .NotNull()
            .MustAsync(async (roomId, cancellation) =>
                await roomRepository.ExistsAsync(roomId))
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(booking => booking)
            .Must(booking => booking.CheckInDate < booking.CheckOutDate)
            .WithMessage("Check-in date must be before Check-out date.")
            .WithName("Booking");
        
        RuleFor(booking => booking)
            .MustAsync(async (booking, cancellation) => 
                !await bookingRepository.RoomIsBookedBetween(
                    booking.RoomId,
                    booking.CheckInDate,
                    booking.CheckOutDate))
            .WithMessage("The room is already booked for the selected dates.")
            .WithName("Booking");
        
        RuleFor(booking => booking)
            .Must(booking => booking.CheckInDate >= DateTime.UtcNow)
            .WithMessage("Check-in date must be in the future.")
            .WithName("Booking");

        RuleFor(booking => booking)
            .Must(booking => (booking.CheckOutDate - booking.CheckInDate).Days 
                            <= BookingConstants.MaxBookingDurationDays)
            .WithMessage("Booking duration can not exceed 30 days.");
        
        RuleFor(booking => booking.Notes)
            .MaximumLength(BookingConstants.MaxNotesLength)
            .WithMessage("{PropertyName} can not exceed 500 characters.");

        //  RuleFor(booking => booking.Status)
        //     .IsInEnum()
        //     .WithMessage("Invalid booking status."); // check how can u do this.

    }
}