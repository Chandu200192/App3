using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IIncome
    {
        public Income Add(Income income);
        public Income Delete(Income income);
        public List<Income> GetAllIncomes();
        public Income GetIncome(int Id);
        public Income GetIncomeByInvoiceID(int Id);
        public Income Update(Income income);

        public List<string> GetListOfInvoiceIDs();
    }
}
