using App3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class BioMetricInfoViewModel
    {
        public string  UserName { get; set; }
        public int UserID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm}")]
        public DateTime TimeIn { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm}")]
        public DateTime TimeOut { get; set; }
        public double RegularHours { get; set; }
        public double ExtraHours { get; set; }
        public string strRegularHours { get; set; }
        public string strExtraHours { get; set; }
        public string strCompleteHours { get; set; }
        public double OTHours { get; set; }
        public double OTMin { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public string Issues { get; set; }
        public List<string> Projects { get; set; } = new List<string>();
        public string Project { get; set; }
        public bool isLogin { get; set; }
        public bool isLogout { get; set; }

        public Employee Employee { get; set; }

        public List<BioMetricInfo> bioMetricInfos { get; set; } = new List<BioMetricInfo>();

    }
}
