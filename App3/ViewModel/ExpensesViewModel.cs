using App3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class ExpensesViewModel
    {
        public string strExpensesDetail { get; set; }
        public string strProject { get; set; }
        public string ExpenseId { get; set; }
        public List<ExpensesDetails> ExpensesDetail { get; set; }
        public List<Projects> Projects { get; set; }
        public double Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AmountSpentDate { get; set; }
        public string SpentBy { get; set; }
        public int ExpId { get; set; }
        public IFormFile File { get; set; }
    }
}
