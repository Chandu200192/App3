using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IEmployee
    {
        public Employee AddEmployee(Employee employee);
        public List<Employee> GetAllEmployees();
        public Employee DeleteEmployee(Employee employee);
        public Employee GetEmployee(int employeeId);
        public Employee UpdateEmployee(Employee employee);
        public int? GetEmployeeId(string email);
        public Employee GetEmployeeUserId(int UserId);

    }
}
