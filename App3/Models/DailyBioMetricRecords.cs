using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class DailyBioMetricRecords
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public int UserLoginIndex { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeOut { get; set; }
        public double Hours { get; set; }
        public double Minutes { get; set; }
          
    }
}
