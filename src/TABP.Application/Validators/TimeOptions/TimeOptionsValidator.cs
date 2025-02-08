using FluentValidation;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Enums;

namespace TABP.Application.Validators.TimeOption;

internal class TimeOptionsValidator : AbstractValidator<VisitTimeOptionQuery>
{
    public TimeOptionsValidator()
    {
        RuleFor(query => query.TimeOption)
            .Must(BeValidTimeOption)
            .WithMessage(query => 
                @$"Invalid Time field: '{query.TimeOption}'. Try: {string.Join(", ", Enum.GetNames(typeof(TimeOptions)))}");

    }

    private bool BeValidTimeOption(string? timeOption)
    {
        if(!string.IsNullOrEmpty(timeOption))
            return Enum.TryParse(typeof(TimeOptions), timeOption, true, out _);
        
        return true;
    }
}