using FluentValidation;
using HRSaas.Entities.Dtos;
using System;

namespace HRSaas.Business.ValidationRules
{
    public class PersonalValidator : AbstractValidator<PersonalDetailVm>
    {
        public PersonalValidator()
        {
            RuleFor(t0 => t0.FirstName).NotNull().WithMessage("Ad alanı boş geçilemez");
            RuleFor(t0 => t0.LastName).NotNull().WithMessage("Soyad alanı boş geçilemez");
            RuleFor(x => x.Phone).NotNull().WithMessage("Telefon numarası boş geçilemez");
            RuleFor(t0 => t0.Phone).MinimumLength(14).WithMessage("Telefon 10 karakterden az olamaz");
            RuleFor(t0 => t0.Phone).MaximumLength(14).WithMessage("Telefon 10 karakterden fazla olamaz");
           

            RuleFor(t0 => t0.Tc).NotNull().WithMessage("TC alanı boş geçilemez")
                 .MaximumLength(11).WithMessage("TC Kimlik No 11 karakterden fazla olamaz")
                 .MinimumLength(11).WithMessage("TC Kimlik No 11 karakterden az olamaz");

            RuleFor(t0 => t0.BirthDate).NotEmpty().WithMessage("Doğum tarihi boş geçilemez");

            RuleFor(t0 => t0.BirthDate).Must(t0 => t0 > DateTime.Now ? false : true).WithMessage("Geçerli doğım tarihi giriniz.")
                .Must(t0 => t0 > DateTime.Now.AddYears(-18) ? false : true).WithMessage("Personel 18 yaşından küçük olamaz")
                .Must(t0 => t0 < DateTime.Now.AddYears(-70) ? false : true).WithMessage("Personel 70 yaşından büyük olamaz");

            RuleFor(t0 => t0.StartDate).NotEmpty().WithMessage("İşe başlama tarihi boş geçilemez");

            RuleFor(t0 => t0.StartDate).Must(t0 =>
            {
                if (t0.DayOfYear > DateTime.Now.DayOfYear + 30)
                    return false;
                return true;
            }).WithMessage("Maximum 1 ay sonrasına işe başlangıç tarihi verilebilir");



            RuleFor(t1 => t1.EndDate).Must((rootObject, list) =>
            {
                if (rootObject.StartDate > rootObject.EndDate)
                    return false;
                return true;
            }).WithMessage("İşten çıkış tarihi,işe giriş tarihinden önce olamaz"); ;

            RuleFor(t0 => t0.Salary).NotEmpty().WithMessage("Maaş bilgisi boş geçilemez");
            RuleFor(t0 => t0.Salary).Must(t0 => t0 > 4249 ? true : false).WithMessage("Maaş 4250 den az olamaz");

            // işe g1iriş çıkış tarihi kontrolü
            //RuleFor(t0 => t0.StartDate).Must(t0 =>
            //{
            //    if (t0.Date < DateTime.)
            //    {
            //        return false;
            //    }
            //    else
            //        return true;
            //}).WithMessage("işe giriş tarihi çıkış tarihinden sonra olamaz");



            RuleFor(t0 => t0.AddressDescription).NotEmpty().WithMessage("Adress alanı boş geçilemez");
            RuleFor(t0 => t0.AddressName).NotEmpty().WithMessage("Adres adı alanı boş geçilemez");
            RuleFor(t0 => t0.CityId).Must((rootObject, list) =>
            {
                if (rootObject.CityId != 0)
                    return true;
                return false;
            }).WithMessage("İl seçimi yapmadınız.");
            RuleFor(t0 => t0.CountyId).Must((rootObject, list) =>
            {
                if (rootObject.CountyId != 0)
                    return true;
                return false;
            }).WithMessage("İlçe seçimi yapmadınız.");

            RuleFor(t0 => t0.DepartmentId).Must((rootObject, List) =>
            {
                if (rootObject.DepartmentId != 0)
                    return true;
                return false;
            
            }).WithMessage("Departman seçimi yapınız.");

            RuleFor(t0 => t0.TitleId).Must((rootObject, list) =>
            {
                if (rootObject.TitleId != 0)
                    return true;
                return false;
            }).WithMessage("Ünvan seçimi yapınız.");
            RuleFor(t0 => t0.Gender).NotEmpty().WithMessage("Cinsiyet alanı boş geçilemez");
            RuleFor(t0 => t0.PostalCode).NotEmpty().WithMessage("Ad alanı boş geçilemez");

        }
    }
}
