using FluentValidation;
using HRSaas.Entities.Dtos;

namespace HRSaas.Business.ValidationRules
{
    public class EmployeeUpdateVMValidator:AbstractValidator<EmployeeUpdateVM>
    {
        public EmployeeUpdateVMValidator()
        {
            RuleFor(t0 => t0.Phone).NotEmpty().WithMessage("Telefon girmelisiniz");

            RuleFor(t0 => t0.Phone).Must((rootObject,list)=> {
                char[] dizi = "0123456789".ToCharArray(); 
                int number = 0;
                
                for (int i = 0; i < rootObject.Phone.Length; i++)
                    for (int j = 0; j < dizi.Length; j++)
                        if (dizi[j] == rootObject.Phone[i])
                            number+=1;
                if (number == 10)
                    return true;
                return false;
            }).When(t0=>t0.Phone!=null).WithMessage("Telefon numarasını eksik girmeyin");
            
            RuleFor(t0 => t0.CityId).Must((rootObject, list) =>
            {
                if (rootObject.CityId != 0)
                    return true;
                return false;
            }).WithMessage("İl seçimi yapmadınız");

            RuleFor(t0 => t0.CountyId).Must((rootObject, list) =>
            {
                if (rootObject.CountyId != 0)
                    return true;
                return false;
            }).WithMessage("İlçe seçimi yapmadınız");
            
            RuleFor(t0 => t0.AddressName).NotEmpty().WithMessage("Adres başlığı girmelisiniz");
            RuleFor(t0 => t0.AddressDescription).NotEmpty().WithMessage("Adres detayını girmelisiniz");
            RuleFor(t0 => t0.PostalCode).NotEmpty().WithMessage("Posta kodu girmelisiniz");
        }
    }
}
