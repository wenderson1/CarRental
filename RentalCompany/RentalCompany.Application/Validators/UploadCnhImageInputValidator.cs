using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalCompany.Application.Models.Input;

namespace RentalCompany.Application.Validators
{
    public class UploadCnhImageInputValidator : AbstractValidator<UploadCnhImageInput>
    {
        public UploadCnhImageInputValidator()
        {
            RuleFor(x => x.CnhImage)
                       .Must(BeValidImage)
                       .WithMessage("The file is not a valid PNG or BMP image.");
        }
        private bool BeValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            byte[] buffer = new byte[2];

            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                buffer = ms.ToArray();
            }

            return (buffer[0] == 0x89 && buffer[1] == 0x50) || (buffer[0] == 0x42 && buffer[1] == 0x4D);
        }
    }
}
