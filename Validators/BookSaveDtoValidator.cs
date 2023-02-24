using FluentValidation;
using Library.Dtos;

namespace Library.Validators
{
    public class BookSaveDtoValidator : AbstractValidator<BookSaveDto>, IBookSaveDtoValidator
    {
        public BookSaveDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters");

            RuleFor(x => x.Cover)
                .NotEmpty().WithMessage("Cover is required")
                .Must(IsBase64).WithMessage("Must be base64 string");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre is required");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .MaximumLength(100).WithMessage("Author must be at most 100 characters");
        }
        private bool IsBase64(string value)
        {
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}