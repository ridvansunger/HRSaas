using FluentValidation;
using HRSaas.Business.Abstract;
using HRSaas.Entities.Dtos;

namespace HRSaas.Business.ValidationRules
{
    public class AdvanceVMValidator : AbstractValidator<AdvanceVM>
    {
        private readonly IAdvanceBusiness _advanceBusiness;
        private readonly IPersonalBusiness _personalBusiness;
        public AdvanceVMValidator(IAdvanceBusiness advanceBusiness, IPersonalBusiness personalBusiness)
        {
            _advanceBusiness = advanceBusiness;
            _personalBusiness = personalBusiness;

            RuleFor(t0 => t0.AdvanceAmount).NotEmpty().WithMessage("Avans miktarını boş geçmeyin");
            RuleFor(t0 => t0.Comment).NotEmpty().WithMessage("Açıklama kısmını boş geçmeyin");

            RuleFor(t0 => t0.AdvanceAmount).Must((rootObject, list) =>
            {
                decimal personalSalary = (decimal)_advanceBusiness.GetTotalAdvanceReceivedMonthly(rootObject.PersonalId);
                if (rootObject.AdvanceAmount + personalSalary > _personalBusiness.GetById(rootObject.PersonalId).Result.Salary*3/10)
                    return false;
                return true;
            }).When(t0 => t0.PersonalId!=0).WithMessage($"Talep ettiğiniz avans miktarı ile birlikte bu ayki toplam avans tutarınız maaşınızın %30 unu geçtiği için daha düşük bir avans talep ediniz");
            
            RuleFor(t0 => t0.AdvanceAmount).Must((rootObject, list) =>
            {
                decimal salary = (decimal)_personalBusiness.GetById(rootObject.PersonalId).Result.Salary*5/100;
                if (rootObject.AdvanceAmount < salary)
                    return false;
                return true;
            }).When(t0=>t0.PersonalId!=0).WithMessage("Talep ettiğiniz avans miktarı maaşınız %5 inden düşük olamaz.");
        }
    }
}
