using FluentValidation;
using Library.Dtos;

namespace Library.Validators
{
    public class RatingSaveDtoValidator : AbstractValidator<RatingSaveDto>, IRatingSaveDtoValidator
    {
        public RatingSaveDtoValidator()
        {
            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5");
        }
    }
}