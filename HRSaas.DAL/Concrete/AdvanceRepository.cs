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
    public class AdvanceRepository : GenericRepository<Advance>, IAdvanceRepository
    {
        private HRSaasContext _context;
        public AdvanceRepository(HRSaasContext db) : base(db)
        {
            _context = db;
        }

        public decimal GetTotalAdvanceReceivedMonthly(int personalId)
        {
            decimal totalAdvance = 0;
            _context.Advances.Where(t0 => t0.PersonalId == personalId && t0.SeenByManager == true && t0.RequestDate.Month==DateTime.Now.Month)
                .ToList().ForEach(t0 =>totalAdvance += t0.AdvanceAmount);
            return totalAdvance;
        }
    }
}
