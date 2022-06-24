using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IProject
    {
        public Projects Add(Projects project);
        public Projects Delete(Projects project);
        public List<Projects> GetAllProjects();
        public Projects GetProject(int Id);
        public Projects Update(Projects project);
        public Projects GetProjectByProjectID(int id);
        public List<string> GetAllProjectNames();
    }
}
