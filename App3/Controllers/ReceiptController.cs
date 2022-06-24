using App3.BusinessLayer;
using App3.Models;
using App3.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Controllers
{
    public class ReceiptController : Controller
    {

        public IIncome _income;
        public IClient _client;
        public IProject _project;
        public IReceipt _receipt;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ReceiptController(IReceipt receipt,  IIncome income, IClient client, IProject project, IWebHostEnvironment hostingEnvironment)
        {
            _receipt = receipt;
            _income = income;
            _client = client;
            _project = project;
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Receipts()
        {
            List<Receipt> receipts = new List<Receipt>();
            receipts = _receipt.GetAllProjects().ToList();
            return View(receipts);
        }

        public IActionResult CreateReceipt()
        {
            ReceiptsViewModel receipt = new ReceiptsViewModel() { lstInvoiceID = _income.GetListOfInvoiceIDs() };
            return View(receipt);
        }

        [HttpPost]
        public IActionResult CreateReceipt(ReceiptsViewModel model)
        {
            if(ModelState.IsValid)
            {
                Receipt receipt = new Receipt()
                {
                    InvoiceId = model.InvoiceId,
                    ReceiptID = model.ReceiptID,
                    Amount = model.Amount,
                    AmountSource = model.AmountSource,
                    AmountReceivedBy = model.AmountReceivedBy,
                    ReceivedDate = model.ReceivedDate,
                    ReceiptFilePath = ProcessUploadFile(model),
                    Comments = model.Comments
                };
                _receipt.Add(receipt);
                return RedirectToAction("Receipts");
            }
            return View(model);
        }


        public IActionResult EditReceipt(int id)
        {
            Receipt receipt = _receipt.GetReceipt(id);
            ReceiptsViewModel receiptsViewModel = new ReceiptsViewModel()
            {
                lstInvoiceID = _income.GetListOfInvoiceIDs(),
                InvoiceId = receipt.InvoiceId,
                ReceiptID = receipt.ReceiptID,
                Amount = receipt.Amount,
                AmountSource = receipt.AmountSource,
                AmountReceivedBy = receipt.AmountReceivedBy,
                Comments = receipt.Comments,
                ReceivedDate = receipt.ReceivedDate
            };
            return View(receiptsViewModel);
        }

        [HttpPost]
        public IActionResult EditReceipt(ReceiptsViewModel model)
        {
            Receipt receipt = _receipt.GetProjectByReceiptID(model.ReceiptID);
            if (ModelState.IsValid)
            {
               
                if (receipt!=null)
                {
                    receipt.InvoiceId = model.InvoiceId;
                    receipt.ReceiptID = model.ReceiptID;
                    receipt.ReceivedDate = model.ReceivedDate;
                    receipt.Comments = model.Comments;
                    receipt.Amount = model.Amount;
                    receipt.AmountSource = model.AmountSource;
                    receipt.AmountReceivedBy = model.AmountReceivedBy;


                    _receipt.Update(receipt);
                    return RedirectToAction("Receipts");
                }
                return View("EditReceipt", model);
            }
            return View(model);
        }


        private string ProcessUploadFile(ReceiptsViewModel receiptsViewModel)
        {
            string uniqueFileName = null;
            if (receiptsViewModel.ReceiptFile != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath + @"\Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + receiptsViewModel.ReceiptFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    receiptsViewModel.ReceiptFile.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }
    }
}
