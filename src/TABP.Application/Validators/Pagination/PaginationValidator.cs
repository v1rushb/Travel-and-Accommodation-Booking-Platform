using FluentValidation;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Validators.Pagination;

internal class PaginationValidator : AbstractValidator<PaginationDTO>
{
    public PaginationValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}