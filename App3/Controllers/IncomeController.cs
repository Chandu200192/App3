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
    public class IncomeController : Controller
    {

        public IIncome _income;
        public IClient _client;
        public IProject _project;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IncomeController(IIncome income, IClient client, IProject project, IWebHostEnvironment hostingEnvironment)
        {
            _income = income;
            _client = client;
            _project = project;
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult AddIncome()
        {
            List<Client> clients = _client.GetAllClients();
            List<Projects> projects = _project.GetAllProjects();
            var incomes = _income.GetAllIncomes();
            var invoiceNumbers = from i in incomes
                                 orderby i.InvoiceNumber ascending
                                 select i.InvoiceNumber;

            int currentInvoice = 0;
            if (invoiceNumbers!= null)
            {
                currentInvoice = invoiceNumbers.Count()>0?invoiceNumbers.Last()+1:1;
            }
            else
            {
                currentInvoice = 1;
            }
            string invoiceNum = "INV" + string.Format("{0, 0:D5}", currentInvoice);
            IncomeViewModel incomeViewModel = new IncomeViewModel() { InvoiceNumber = invoiceNum, ClientNameList = clients, ProjectNameList = projects, AmountRecievedDate=DateTime.Today.Date };
            return View("Income", incomeViewModel);
        }

        public IActionResult CreateIncome(IncomeViewModel incomeViewModel)
        {
            Income income = new Income();

            string uniqueFileName = null;
            if (incomeViewModel.File != null)
            {
                uniqueFileName = ProcessUploadFile(incomeViewModel);
            }


            if (ModelState.IsValid)
            {
                string strInvoice = incomeViewModel.InvoiceNumber[3..];

                income.ProjectName = incomeViewModel.ProjectName;
                income.ClientName = incomeViewModel.ClientName;
                income.ProjectChagres = incomeViewModel.AmountRecieved;
                income.Tax = incomeViewModel.Tax;
                income.AmountRecivedBy = incomeViewModel.AmountRecivedBy;
                income.AmountRecievedDate = incomeViewModel.AmountRecievedDate;
                income.InvoiceNumber = Convert.ToInt32(strInvoice);
                income.FilePath = uniqueFileName;
                _income.Add(income);
                return RedirectToAction("AddIncome");
            }
            return View("AddIncome", incomeViewModel);
        }
        public IActionResult UpdateIncome(IncomeViewModel incomeViewModel)
        {
            string strInvoice = incomeViewModel.InvoiceNumber[3..];
            Income income = _income.GetIncomeByInvoiceID(Convert.ToInt32(strInvoice));
            if (ModelState.IsValid)
            {
                income.ProjectName = incomeViewModel.ProjectName;
                income.ClientName = incomeViewModel.ClientName;
                income.ProjectChagres = incomeViewModel.AmountRecieved;
                income.Tax = incomeViewModel.Tax;
                income.AmountRecivedBy = incomeViewModel.AmountRecivedBy;
                income.AmountRecievedDate = incomeViewModel.AmountRecievedDate;
               
                _income.Update(income);
                return RedirectToAction("IncomeDetails");
            }
            return View("AddIncome", incomeViewModel);
        }

        public IActionResult EditIncome(int id)
        {
            IncomeViewModel incomeViewModel = new IncomeViewModel();
            Income income = _income.GetIncome(id);
            if (income != null)
            {
                string invoiceNum = "INV" + string.Format("{0, 0:D5}", income.InvoiceNumber);
                incomeViewModel.AmountRecieved = income.ProjectChagres;
                incomeViewModel.AmountRecievedDate = income.AmountRecievedDate;
                incomeViewModel.AmountRecivedBy = income.AmountRecivedBy;
                incomeViewModel.ClientName = income.ClientName;
                incomeViewModel.ProjectName = income.ProjectName;
                incomeViewModel.InvoiceNumber = invoiceNum;
                incomeViewModel.Tax = income.Tax;
                incomeViewModel.ProjectNameList = _project.GetAllProjects();
                incomeViewModel.ClientNameList = _client.GetAllClients();
            }
         
            return View(incomeViewModel);
        }

        public ViewResult IncomeDetails()
        {

            List<Income> incomes = _income.GetAllIncomes();
            return View("IncomeList", incomes);
        }

        public IActionResult AddClient()
        {
            Client client = new Client();
            return View("Clients", client);
        }

        public IActionResult CreateClient(Client client)
        {
            if(ModelState.IsValid)
            {
                _client.Add(client);
                return RedirectToAction("AddClient");
            }
            
            return View("Clients", client);
        }

        public IActionResult EditClient(int id)
        {
            Client client = new Client();
            if (ModelState.IsValid)
            {
                client = _client.GetClient(id);
            }
            return View("EditClient", client);
        }

        public IActionResult UpdateClient(Client client)
        {
            if (ModelState.IsValid)
            {
                _client.Update(client);
                return RedirectToAction("ClientList");
            }

            return View("EditClient", client);
        }


        public ViewResult ClientList()
        {
            List<Client> clients = _client.GetAllClients();
            return View(clients);
        }

        public IActionResult AddProject()
        {
            List<Client> clients = _client.GetAllClients();
            ProjectViewModel projectViewModel = new ProjectViewModel() { ClientNameList = clients };
            return View("ProjectDetails", projectViewModel);
        }

        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                Projects projects = new Projects()
                {
                    ProjectID = projectViewModel.ProjectID,
                    ProjectDesc = projectViewModel.ProjectDesc,
                    ClientName = projectViewModel.ClientName,
                    ProjectName = projectViewModel.ProjectName                   
                };

                _project.Add(projects);
                return RedirectToAction("AddProject");
            }

            return View("ProjectDetails", projectViewModel);
        }
        public IActionResult EditProject(int id)
        {
            Projects project;
            ProjectViewModel projectViewModel = new ProjectViewModel();
            if (ModelState.IsValid)
            {
                project = _project.GetProject(id);

                projectViewModel.ProjectID = project.ProjectID;
                projectViewModel.ProjectName = project.ProjectName;
                projectViewModel.ClientName = project.ClientName;
                projectViewModel.ClientNameList = _client.GetAllClients();
                projectViewModel.ProjectDesc = project.ProjectDesc;
            }           
            return View("EditProject", projectViewModel);
        }


        public IActionResult UpdateProject(ProjectViewModel projectViewModel)
        {
            Projects project = _project.GetProjectByProjectID(projectViewModel.ProjectID);
            if (ModelState.IsValid)
            {
                project.ProjectName = projectViewModel.ProjectName;
                project.ProjectID = projectViewModel.ProjectID;
                project.ClientName = projectViewModel.ClientName;
                project.ProjectDesc = projectViewModel.ProjectDesc;

                _project.Update(project);
                return RedirectToAction("ProjectList");
            }
            return View("EditProject", projectViewModel);
        }

        public IActionResult DeleteProject(int id)
        {
            Projects project = _project.GetProject(id);
            if(ModelState.IsValid)
            {
                _project.Delete(project);
            }
            return RedirectToAction("ProjectList");
        }

        public IActionResult ProjectList()
        {
            List<Projects> Projects = _project.GetAllProjects();
            return View("ProjectsList", Projects);
        }


        private string ProcessUploadFile(IncomeViewModel incomeViewModel)
        {
            string uniqueFileName = null;
            if (incomeViewModel.File != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath + @"\Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + incomeViewModel.File.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    incomeViewModel.File.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }


    }
}
