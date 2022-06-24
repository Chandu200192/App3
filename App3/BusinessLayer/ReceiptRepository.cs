using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public class ReceiptRepository : IReceipt
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public Receipt Add(Receipt project)
        {
            _context.Add(project);
            _context.SaveChanges();
            return project;
        }

        public Receipt Delete(Receipt project)
        {
            Receipt project1 = _context.ReceiptDb.Find(project.id);
            if (project1 != null)
            {
                _context.Remove(project1);
                _context.SaveChanges();
            }
            return project;
        }

        public List<Receipt> GetAllProjects()
        {
            return _context.ReceiptDb?.ToList();
        }

        public Receipt GetProjectByInvoiceID(string id)
        {
            return _context.ReceiptDb.FirstOrDefault(i => i.InvoiceId == id);
        }

        public Receipt GetProjectByReceiptID(string id)
        {
            return _context.ReceiptDb.FirstOrDefault(i => i.ReceiptID == id);
        }

        public Receipt GetReceipt(int Id)
        {
            return _context.ReceiptDb.FirstOrDefault(i => i.id == Id);
        }

        public Receipt Update(Receipt project)
        {
            var project1 = _context.ReceiptDb.Attach(project);
            project1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return project;
        }
    }
}
