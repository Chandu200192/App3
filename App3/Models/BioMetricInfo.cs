using App3.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class BioMetricInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeOut { get; set; }
        public double RegularHours { get; set; }
        public double RegularMin { get; set; }
        public double ExtraHours { get; set; }
        public double ExtraMin { get; set; }
        public double OTHours { get; set; }
        public double OTMin { get; set; }
        public ApprovalType approvalType { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public string Issues { get; set; }
        public string Project { get; set; }
        public bool isLogin { get; set; }
    }
}
