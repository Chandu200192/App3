using App3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class IncomeViewModel
    {
        public string InvoiceNumber { get; set; }
        public List<Client> ClientNameList { get; set; }
        public List<Projects> ProjectNameList { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public double AmountRecieved { get; set; }
        public int Tax { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AmountRecievedDate { get; set; }
        public string AmountRecivedBy { get; set; }

        public IFormFile File { get; set; }
    }
}
