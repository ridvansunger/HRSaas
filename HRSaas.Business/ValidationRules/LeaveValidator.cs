using FluentValidation;
using HRSaas.DAL.Abstract;
using HRSaas.Entities.Dtos;
using System;
using System.Collections.Generic;

namespace HRSaas.Business.ValidationRules
{
    public class LeaveValidator:AbstractValidator<LeaveVM>
    {
        private ILeaveRepository _leaveRepository;
        public LeaveValidator(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;

            RuleFor(t0 => t0.LeaveStartDate).NotEmpty().WithMessage("İzin başlangıç tarihi boş geçilemez");

            RuleFor(t0=>t0.LeaveStartDate).Must((rootObject, list) =>
            {
                if (rootObject.LeaveStartDate >= DateTime.Now)
                    return true;
                return false;
            }).WithMessage("İzin başlangıç tarihi önceki bir tarih olamaz.");

            RuleFor(t0 => t0.LeaveEndDate).NotEmpty().WithMessage("İzin bitiş tarihi boş geçilemez");

            RuleFor(t0 => t0.LeaveEndDate).Must((rootObject, list) =>
            {
                if (rootObject.LeaveStartDate > rootObject.LeaveEndDate)
                    return false;
                return true;
            }).WithMessage("İzin bitiş tarihi izin başlangıcından önceki bir tarih olamaz");

            RuleFor(t0 => t0.LeaveTypeId).Must((rootObject, list) =>
            {
                if (rootObject.LeaveTypeId != -1)
                    return true;
                return false;
            }).WithMessage("Lütfen seçim yapınız");


            RuleFor(t0 => t0.Comment).Must((rootObject,context) =>
            {
                int daysLeft = _leaveRepository.DaysLeftFromAnnualLeave(rootObject.PersonalId);
                int requestedDayOff = rootObject.LeaveEndDate.DayOfYear - rootObject.LeaveStartDate.DayOfYear;
                if (requestedDayOff > daysLeft)
                    return false;
                return true;
            }).When(t0=>t0.PersonalId!=0).When(t0=>t0.LeaveTypeId==2).WithMessage("Yıllık izin sürenizi aştığınız için açıklama girmeniz gerekmektedir");
            
        }
    }
}
