using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IExpenses
    {
        public Expenses Add(Expenses expenses);
        public Expenses Delete(Expenses expenses);
        public List<Expenses> GetAllExpenses();
        public Expenses GetExpenses(int Id);
        public Expenses Update(Expenses expenses);
        public Expenses GetExpensesByExpensesId(int ExpensesId);
    }
}
