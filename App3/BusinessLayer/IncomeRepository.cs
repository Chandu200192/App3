using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer.Repository
{
    public class IncomeRepository : IIncome
    {
        private readonly AppDbContext _context;

        public IncomeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;

        }
        public Income Add(Income income)
        {
            _context.Add(income);
            _context.SaveChanges();
            return income;
        }

        public Income Delete(Income income)
        {

            Income income1 = _context.Incomes.Find(income.Id);
            if(income1!=null)
            {
                _context.Remove(income1);
                _context.SaveChanges();
            }
            return income;
        }

        public List<Income> GetAllIncomes()
        {
            return _context.Incomes?.ToList();
        }

        public Income GetIncome(int Id)
        {
            return _context.Incomes.Find(Id);
        }

        public Income GetIncomeByInvoiceID(int Id)
        {
            return _context.Incomes.FirstOrDefault(i => i.InvoiceNumber == Id);
        }

        public List<string> GetListOfInvoiceIDs()
        {
            var lstInvoices = _context.Incomes.ToList();
            var lstInvoiceIds = from i in lstInvoices
                                let k =  "INV" + string.Format("{0, 0:D5}", i.InvoiceNumber) 
                                select k;

            return lstInvoiceIds.ToList();
        }

        public Income Update(Income income)
        {

            var income1 = _context.Incomes.Attach(income);
            income1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return income;
        }
    }
}
