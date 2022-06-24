using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class EditEmployeeViewModel
    {
        public int id { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [MaxLength(10)]
        public string EmployeeName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfJoining { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }

        public string Designation { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ReportingManager { get; set; }
        public string Project { get; set; }
        public IFormFile Photo { get; set; }

    }
}
