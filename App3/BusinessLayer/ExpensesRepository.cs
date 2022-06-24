using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer.Repository
{
    public class ExpensesRepository : IExpenses
    {
        private readonly AppDbContext _context;

        public ExpensesRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Expenses Add(Expenses expenses)
        {
            _context.Add(expenses);
            _context.SaveChanges();
            return expenses;
        }

        public Expenses Delete(Expenses expenses)
        {
            Expenses expenses1 = _context.ExpensesDb.Find(expenses.ExpensesId);
            if(expenses1!=null)
            {
                _context.Remove(expenses1);
                _context.SaveChanges();
            }
            return expenses;
        }

        public List<Expenses> GetAllExpenses()
        {
            return _context.ExpensesDb?.ToList();
        }

        public Expenses GetExpenses(int Id)
        {
            return _context.ExpensesDb.Find(Id);
        }

        public Expenses GetExpensesByExpensesId(int ExpensesId)
        {
            return _context.ExpensesDb.Where(i=> i.ExpensesId == ExpensesId).FirstOrDefault();
        }

        public Expenses Update(Expenses expenses)
        {
            var expenses1 = _context.ExpensesDb.Attach(expenses);
            expenses1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return expenses;
        }
    }
}
