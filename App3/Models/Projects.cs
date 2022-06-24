using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class Projects
    {
        [Key]
        public int id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
        public string ClientName { get; set; }
        public int ProjectID { get; set; }

    }
}
