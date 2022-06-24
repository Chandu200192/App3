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
    public class EmployeeController : Controller
    {

        private IEmployee _employee;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployee employee, IWebHostEnvironment hostingEnvironment)
        {
            _employee = employee;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult AddEmployee()
        {
            Employee employee = new Employee();
            //_employee.AddEmployee(employee);
            return View(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employee.AddEmployee(employee);
                return RedirectToAction("AddEmployee");
            }
            Employee employee1 = new Employee();
            return View("AddEmployee",employee1);
        }
        public ViewResult EmployeesList()
        {
            List<Employee> employees = _employee.GetAllEmployees();
            return View(employees);
        }

        [HttpPost]
        public IActionResult UpdateResult(EditEmployeeViewModel employeeViewModel)
        {

            string uniqueFileName = null;
            if (employeeViewModel.Photo != null)
            {
                uniqueFileName = ProcessUploadFile(employeeViewModel);
            }


            if (ModelState.IsValid)
            {
                Employee employee1 = new Employee()
                {
                    EmployeeID = employeeViewModel.EmployeeID,
                    EmployeeName = employeeViewModel.EmployeeName,
                    Email = employeeViewModel.Email,
                    DateOfJoining = employeeViewModel.DateOfJoining,
                    DOB = employeeViewModel.DOB,
                    id = employeeViewModel.id,
                    ReportingManager = employeeViewModel.ReportingManager,
                    Project = employeeViewModel.Project,
                    Designation = employeeViewModel.Designation,
                    PhotoPath = uniqueFileName
                    
                };
                _employee.UpdateEmployee(employee1);
                return RedirectToAction("EmployeesList");
            }
            return View();
        }


        private string ProcessUploadFile(EditEmployeeViewModel editEmployeeViewModel)
        {
            string uniqueFileName = null;
            if (editEmployeeViewModel.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath + @"\Files\EmployeePhotos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + editEmployeeViewModel.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    editEmployeeViewModel.Photo.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult EditEmployee(int id)
        {
            Employee employee1 = new Employee();


            if (ModelState.IsValid)
            {
                employee1 =  _employee.GetEmployee(id);
                EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel()
                {
                    id = employee1.id,
                    EmployeeName = employee1.EmployeeName,
                    Email = employee1.Email,
                    EmployeeID = employee1.EmployeeID,
                    DateOfJoining = employee1.DateOfJoining,
                    Designation = employee1.Designation,
                    DOB = employee1.DOB,
                    Project = employee1.Project,
                    ReportingManager = employee1.ReportingManager
                };
                return View(editEmployeeViewModel);
            }

            return RedirectToAction("EmployeesList");
        }

        //public IActionResult UploadUserPhoto()
        //{
        //    Income income = new Income();

        //    string uniqueFileName = null;
        //    if (incomeViewModel.File != null)
        //    {
        //        uniqueFileName = ProcessUploadFile(incomeViewModel);
        //    }
        //}


        //private string ProcessUploadFile(IncomeViewModel incomeViewModel)
        //{
        //    string uniqueFileName = null;
        //    if (incomeViewModel.File != null)
        //    {
        //        string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath + @"\Files");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + incomeViewModel.File.FileName;
        //        string filePath = Path.Combine(uploadFolder, uniqueFileName);
        //        using (var filestream = new FileStream(filePath, FileMode.Create))
        //        {
        //            incomeViewModel.File.CopyTo(filestream);
        //        }
        //    }
        //    return uniqueFileName;
        //}


        public IActionResult DeleteEmployee(int id)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employee.GetEmployee(id);
                _employee.DeleteEmployee(employee);
                return RedirectToAction("EmployeesList");
            }
            return View("EmployeesList");
        }

    }
}
