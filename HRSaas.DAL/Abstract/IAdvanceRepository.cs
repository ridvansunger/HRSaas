using HRSaas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.DAL.Abstract
{
    public interface IAdvanceRepository : IGenericRepository<Advance>
    {
        decimal GetTotalAdvanceReceivedMonthly(int personalId);
    }
}