using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
     public interface IExpensesDetails
    {
        public ExpensesDetails Add(ExpensesDetails expensesDetails);
        public ExpensesDetails Delete(ExpensesDetails expensesDetails);
        public List<ExpensesDetails> GetAllExpensesDetails();
        public ExpensesDetails GetExpensesDetails(int Id);
        public ExpensesDetails Update(ExpensesDetails expensesDetails);
    }
}
