using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public  interface IReceipt
    {
        public Receipt Add(Receipt project);
        public Receipt Delete(Receipt project);
        public List<Receipt> GetAllProjects();
        public Receipt GetReceipt(int Id);
        public Receipt Update(Receipt project);
        public Receipt GetProjectByReceiptID(string id);
        public Receipt GetProjectByInvoiceID(string id);
    }
}
