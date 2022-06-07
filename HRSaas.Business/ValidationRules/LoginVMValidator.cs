using FluentValidation;
using HRSaas.Entities.Dtos;

namespace HRSaas.Business.ValidationRules
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(t0 => t0.Email).NotEmpty().WithMessage("Mail adresinizi girmelisiniz");
            RuleFor(t0 => t0.Password).NotEmpty().WithMessage("Şifrenizi girmelisiniz");
            RuleFor(t0 => t0.PasswordConfirm).NotEmpty().When(t0 => t0.FirstSingin == true).WithMessage("Şifre tekrarını girmediniz");
            RuleFor(t0 => t0.PasswordConfirm).Must((rootObject, list) =>
            {
                if (rootObject.Password != rootObject.PasswordConfirm)
                    return false;
                return true;
            }).When(t0 => t0.Password != null && t0.PasswordConfirm != null && t0.FirstSingin == true).WithMessage("Girilen şifreler eşit değil. Tekrar giriniz");
        }
    }
}
