using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaas.Entities.Dtos
{
    public class LeaveVM
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public int PersonalId { get; set; }
        public int LeaveTypeId { get; set; }
        public string NameAndSurname { get; set; }
        public string DepartmentName { get; set; }
        public string TitleName { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }

        //total yıllık izin hakkı(yıllıklar toplam)
        public float AnnualLeave { get; set; }

        //kullandığı izin gün sayısı()yıllıklardan kalan 
        public float UsedDayOff { get; set; }
        public int TotalWorkDayNumber { get; set; }

        //izin nedeni- izin tipi
        public string LeaveTypeName { get; set; }
        public bool ApprovalStatus { get; set; }
        public bool SeenByManager { get; set; }

        public DateTime? ApprovalDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public string Comment { get; set; }      
    }
}
