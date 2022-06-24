using App3.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class ReceiptsViewModel
    {
        public List<String> lstInvoiceID { get; set; }
        public string InvoiceId { get; set; }
        public string ReceiptID { get; set; }
        public double Amount { get; set; }
        public ReceiptType AmountSource { get; set; }
        public string AmountReceivedBy { get; set; }
        public string Comments { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReceivedDate { get; set; }

        public IFormFile ReceiptFile { get; set; }

    }
}
