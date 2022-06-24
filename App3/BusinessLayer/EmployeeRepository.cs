using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App3.BusinessLayer;

namespace App3.BusinessLayer.Repository
{
    public class EmployeeRepository: IEmployee
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public Employee AddEmployee(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
            return employee;
            
        }

        public Employee DeleteEmployee(Employee employee)
        {
            Employee employee1 = _context.Employees.Find(employee.id);
            if (employee != null)
            {
                _context.Employees.Remove(employee1);
                _context.SaveChanges();
            }
            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = _context.Employees?.ToList();
            return employees;
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees.Find(employeeId);
        }

        public int? GetEmployeeId(string email)
        {
            return _context.Employees.Where(i => i.Email == email)?.FirstOrDefault().EmployeeID;

        }

        public Employee GetEmployeeUserId(int UserId)
        {
            return _context.Employees.Where(i => i.EmployeeID == UserId)?.FirstOrDefault();

        }

        public Employee UpdateEmployee(Employee employee)
        {
            var employee1 = _context.Employees.Attach(employee);
            employee1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return employee;
        }
    }
}
