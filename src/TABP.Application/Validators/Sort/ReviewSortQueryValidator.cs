using FluentValidation;
using TABP.Domain.Models.HotelReview.Sort;

namespace TABP.Application.Validators.Sort;

public class ReviewSortQueryValidator : AbstractValidator<ReviewSortQuery>
{
    private static readonly HashSet<string> SortByOptions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        "Rating",
        "CreationDate"
    };

    private static readonly HashSet<string> SortByOptionsForAdmin = 
        new(SortByOptions, StringComparer.OrdinalIgnoreCase)
    {
        "ModificationDate"
    };

    private static readonly HashSet<string> SortOrderOptions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        "Asc", "Desc"
    };

    public ReviewSortQueryValidator()
    {
        
         RuleFor(query => query.SortBy)
            .Must((query, sortBy) => BeValidSortByOption(sortBy, query.IsAdmin))
            .WithMessage((query, sortBy) => 
                query.IsAdmin 
                    ? $"Invalid sort field: '{sortBy}'. Valid fields are: {string.Join(", ", SortByOptionsForAdmin)}"
                    : $"Invalid sort field: '{sortBy}'. Valid fields are: {string.Join(", ", SortByOptions)}");

        RuleFor(query => query.SortOrder)
            .Must(BeValidSortOrderOption)
            .WithMessage("Invalid sort order: '{PropertyValue}'. Use 'asc' or 'desc'.");
    }

    private bool BeValidSortByOption(
        string sortBy,
        bool isAdmin)
    {
        return string.IsNullOrEmpty(sortBy) || 
            (isAdmin 
                ? SortByOptionsForAdmin.Contains(sortBy) : 
                SortByOptions.Contains(sortBy));
    }

    private bool BeValidSortOrderOption(string sortOrder) =>
        string.IsNullOrEmpty(sortOrder) || 
            SortOrderOptions.Contains(sortOrder);
}