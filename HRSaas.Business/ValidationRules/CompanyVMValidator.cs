using FluentValidation;
using HRSaas.Entities.Dtos;

namespace HRSaas.Business.ValidationRules
{
    public class CompanyVMValidator:AbstractValidator<CompanyVM>
    {
        public CompanyVMValidator()
        {
            RuleFor(t0 => t0.CompanyName).NotEmpty().WithMessage("Şirket adı boş geçilemez");
            RuleFor(t0 => t0.Phone).NotEmpty().WithMessage("Şirket telefonu boş geçilemez");
            RuleFor(t0 => t0.CompanyMail).NotEmpty().WithMessage("Şirket maili boş geçilemez");
            RuleFor(t0 => t0.TaxIdNumber).NotEmpty().WithMessage("Şirket vergi numarası boş geçilemez");
            RuleFor(t0 => t0.Address).NotEmpty().WithMessage("Şirket adresi boş geçilemez");
            RuleFor(t0 => t0.Description).NotEmpty().WithMessage("Şirket tanımı boş geçilemez");

            RuleFor(t0 => t0.Phone).Must((rootObject, list) =>
            {
                if (rootObject.Phone.Length == 10 && rootObject.Phone.ToCharArray()[0] !='0')
                    return true;
                return false;
            }).When(t0 => t0.Phone != null).WithMessage("Şirket telefon numarasını başında 0 olmadan ve 10 karakter giriniz.") ;
        }
    }
}
