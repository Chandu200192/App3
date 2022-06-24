using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class Income
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public double ProjectChagres { get; set; }
        public int Tax { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AmountRecievedDate { get; set; }
        public string AmountRecivedBy { get; set; }

        public string FilePath { get; set; }

    }
}
