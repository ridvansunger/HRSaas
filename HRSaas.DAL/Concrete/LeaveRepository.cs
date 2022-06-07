using HRSaas.DAL.Abstract;
using HRSaas.DAL.Context;
using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.DAL.Concrete
{
    public class LeaveRepository : GenericRepository<Leave>, ILeaveRepository
    {
        private HRSaasContext _context;
        public LeaveRepository(HRSaasContext Db) : base(Db)
        {
            _context = Db;
        }

        public int DaysLeftFromAnnualLeave(int personalId)
        {
            int kalanIzin = 0;
            if (personalId != 0)
            {
                TimeSpan timeSpan = new TimeSpan();
                var annualLeaveEntitlement = Convert.ToInt32(_context.Personals.FirstOrDefault(t0 => t0.Id == personalId).AnnualLeave);
                var annualLeaveUsed = from t0 in _context.Leaves
                                      join t1 in _context.LeaveTypes
                                      on t0.LeaveTypeId equals t1.Id
                                      where t1.LeaveTypeName == "Yıllık" && t0.PersonalId == personalId
                                      select new
                                      {
                                          timeSpan = t0.LeaveEndDate - t0.LeaveStartDate
                                      };
                int toplamKullanilanYillikİzin = 0;
                foreach (var item in annualLeaveUsed)
                {
                    toplamKullanilanYillikİzin += item.timeSpan.Days;
                }
                kalanIzin = annualLeaveEntitlement - toplamKullanilanYillikİzin;
            }

            return kalanIzin;
        }
    }
}
