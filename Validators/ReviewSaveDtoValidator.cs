using FluentValidation;
using Library.Dtos;

namespace Library.Validators
{
    public class ReviewSaveDtoValidator : AbstractValidator<ReviewSaveDto>, IReviewSaveDtoValidator
    {
        public ReviewSaveDtoValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required")
                .MaximumLength(1000).WithMessage("Message must be at most 1000 characters");

            RuleFor(x => x.Reviewer)
                .NotEmpty().WithMessage("Reviewer is required")
                .MaximumLength(100).WithMessage("Reviewer must be at most 100 characters");
        }
    }
}