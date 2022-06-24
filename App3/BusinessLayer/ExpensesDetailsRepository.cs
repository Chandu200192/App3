using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer.Repository
{
    public class ExpensesDetailsRepository :IExpensesDetails
    {
        private readonly AppDbContext _context;

        public ExpensesDetailsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public ExpensesDetails Add(ExpensesDetails expensesDetails)
        {
            _context.Add(expensesDetails);
            _context.SaveChanges();
            return expensesDetails;
        }

        public ExpensesDetails Delete(ExpensesDetails expensesDetails)
        {
            ExpensesDetails expensesDetails1 = _context.ExpensesDetailsDb.Find(expensesDetails.ExpenseID);
            if(expensesDetails1 != null)
            {
                _context.Remove(expensesDetails1);
                _context.SaveChanges();
            }
            return expensesDetails;
        }

        public List<ExpensesDetails> GetAllExpensesDetails()
        {
            return _context.ExpensesDetailsDb?.ToList();
        }

        public ExpensesDetails GetExpensesDetails(int Id)
        {
            return _context.ExpensesDetailsDb.Find(Id);
        }

        public ExpensesDetails Update(ExpensesDetails expensesDetails)
        {
            var expensesDetails1 = _context.ExpensesDetailsDb.Attach(expensesDetails);
            expensesDetails1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return expensesDetails;
        }
    }
}
