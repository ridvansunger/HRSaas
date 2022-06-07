using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HRSaas.Entities.Dtos;
using System;

namespace HRSaas.Business.ValidationRules
{
    public class PackageValidator: AbstractValidator<PackageVM>
    {

        public PackageValidator()
        {
            RuleFor(t0 => t0.PackageName).NotEmpty().WithMessage("Paket adı boş geçilemez");
            RuleFor(t0 => t0.UserCount).NotEmpty().WithMessage("Kulanıcı sayısı boş geçilemez");
            RuleFor(x => x.DurationTime).NotEmpty().WithMessage("Paket süresi boş geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Paket ücreti boş geçilemez");
        }

    }
}
