using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class ExpensesDetails
    {
        [Key]
        public int id { get; set; }
        public int ExpenseID { get; set; }
        public string Name { get; set; }
      

    }
}
