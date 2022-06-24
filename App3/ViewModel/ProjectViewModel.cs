using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class ProjectViewModel
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public List<Client> ClientNameList { get; set; }
        public string ProjectDesc { get; set; }
        public int ProjectID { get; set; }

    }
}
