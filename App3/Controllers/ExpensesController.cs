using App3.BusinessLayer;
using App3.Models;
using App3.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Controllers
{
    public class ExpensesController : Controller
    {

        public IExpenses _expenses;
        public IExpensesDetails _expensesDetails; 
        public ExpensesController(IExpenses expenses, IExpensesDetails expensesDetails)
        {

            _expenses = expenses;
            _expensesDetails = expensesDetails;
        }

        public IActionResult CreateExpenses()
        {
            List<ExpensesDetails> expensesDetails =  _expensesDetails.GetAllExpensesDetails();

            var expenses = _expenses.GetAllExpenses();
            var expId = from i in expenses
                                 orderby i.ExpensesId ascending
                                 select i.ExpensesId;
            int curExpId = 0;
            if (expId != null)
            {
                curExpId = expId.Count() > 0 ? expId.Last() + 1 : 1;
            }
            else
            {
                curExpId = 1;
            }
            string expenseId = "EXP" + string.Format("{0, 0:D5}", curExpId);

            ExpensesViewModel expensesViewModel = new ExpensesViewModel() { ExpensesDetail = expensesDetails, ExpenseId = expenseId};
            return View("Expenses",expensesViewModel);
        }
        [HttpPost]
        public IActionResult CreateExpenses(ExpensesViewModel expenses)
        {
            if(ModelState.IsValid)
            {
                string strExpID = expenses.ExpenseId[3..];
                Expenses expenses1 = new Expenses()
                {
                    ExpensesDetails = expenses.strExpensesDetail,
                    Amount = expenses.Amount,
                    Date = expenses.AmountSpentDate,
                    ReleasedBy = expenses.SpentBy,
                    ExpensesId = Convert.ToInt32(strExpID)
                };
                _expenses.Add(expenses1);
                return RedirectToAction("Expenses");
            }
            return View("Expenses", expenses);
        }

        public IActionResult EditExpenses(int id)
        {
            Expenses expenses =  _expenses.GetExpenses(id);
            List<ExpensesDetails> expensesDetails = _expensesDetails.GetAllExpensesDetails();
            ExpensesViewModel expensesViewModel = new ExpensesViewModel()
            {
                ExpenseId = "EXP" + string.Format("{0, 0:D5}", expenses.ExpensesId),
                strExpensesDetail = expenses.ExpensesDetails,
                ExpensesDetail = expensesDetails,
                Amount = expenses.Amount,
                AmountSpentDate = expenses.Date,
                SpentBy = expenses.ReleasedBy
            };
            return View("EditExpenses", expensesViewModel);
        }

        [HttpPost]
        public IActionResult EditExpenses(ExpensesViewModel model)
        {
            string strExpId = model.ExpenseId[3..];
            Expenses expenses = _expenses.GetExpensesByExpensesId(Convert.ToInt32(strExpId));
            if (ModelState.IsValid)
            {
                if (expenses != null)
                {
                    expenses.ExpensesId = Convert.ToInt32(strExpId);
                    expenses.ExpensesDetails = model.strExpensesDetail;
                    expenses.Amount = model.Amount;
                    expenses.ReleasedBy = model.SpentBy;
                    expenses.Date = model.AmountSpentDate;

                    return RedirectToAction("Expenses");
                }              
            }
            return View("EditReceipt", model);
        }

        public IActionResult ExpensesDetail()
        {
            return View("ExpensesDetails", new ExpensesDetails());
        }

        [HttpPost]
        public IActionResult ExpensesDetail(ExpensesDetails expensesDetails)
        {
            if(ModelState.IsValid)
            {
                _expensesDetails.Add(expensesDetails);
                return View("ExpensesDetail", new ExpensesDetails());
            }
            return View("ExpensesDetails", expensesDetails);
        }

        public IActionResult AllExpenses()
        {
            List<Expenses> expenses = _expenses.GetAllExpenses();
            return View(expenses);
        }
    }
}
