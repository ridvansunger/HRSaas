using FluentValidation;
using HRSaas.Entities.Dtos;
using System;

namespace HRSaas.Business.ValidationRules
{
    public class PersonalExpenditureValidator: AbstractValidator<PersonalExpenditureVM>
    {
        public PersonalExpenditureValidator()
        {
            RuleFor(t0 => t0.ExDescription).NotNull().WithMessage("Harcama detayı girmediniz");
            RuleFor(t0 => t0.ExpenditureDate).NotNull().WithMessage("Harcama tarihini seçmediniz");
            RuleFor(t0 => t0.ExpenditureDate).Must((a,b)=> {
                if (a.ExpenditureDate > DateTime.Now)
                    return false;
                return true;
            }).WithMessage("Harcama geçmişteki bir tarihte yapılmalı");
            RuleFor(x => x.ExpenditureAmount).NotNull().WithMessage("Harcama tutarını girmediniz");
            RuleFor(x => x.File).NotNull().WithMessage("Harcama belgesini girmediniz.");
        }
    }
}
