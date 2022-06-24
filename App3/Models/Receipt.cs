using App3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class Receipt
    {
        public int id { get; set; }
        public string InvoiceId { get; set; }
        public string ReceiptID { get; set; }
        public double Amount { get; set; }
        public ReceiptType AmountSource { get; set; }
        public string AmountReceivedBy { get; set; }
        public string Comments { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string  ReceiptFilePath { get; set; }

    }
}
