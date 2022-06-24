using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class BioRecordReportViewModel
    {
        public int UserID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string reportRadios { get; set; }

    }
}
