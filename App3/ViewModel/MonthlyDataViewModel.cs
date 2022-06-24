using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class MonthlyDataViewModel
    {
        public int UserId { get; set; }
        public int CurYear { get; set; }
        public int CurMonth { get; set; }
        public string CurMonthName { get; set; }
        public double RegularHours { get; set; }
        public double RegularMin { get; set; }
        public double ExtraHours { get; set; }
        public double ExtraMin { get; set; }

    }
}
