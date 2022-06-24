using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class Expenses
    {
        [Key]
        public int id { get; set; }
        public int ExpensesId { get; set; }
        public string ExpensesDetails { get; set; }
        public double Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public string ReleasedBy { get; set; }
        public string ProjectName { get; set; }

        public string FilePath { get; set; }

    }
}
