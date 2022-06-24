using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer.Repository
{
    public class ProjectRepository : IProject
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public Projects Add(Projects project)
        {
            _context.Add(project);
            _context.SaveChanges();
            return project;
        }

        public Projects Delete(Projects project)
        {
            Projects project1 = _context.Projects.Find(project.id);
            if(project1 != null)
            {
                _context.Remove(project1);
                _context.SaveChanges();
            }
            return project;
        }

        public List<Projects> GetAllProjects()
        {
            return _context.Projects?.ToList();
        }

        public Projects GetProject(int Id)
        {
            return _context.Projects.Find(Id);
        }

        public Projects GetProjectByProjectID(int Id)
        {
            return _context.Projects.FirstOrDefault(i=> i.ProjectID == Id);
        }

        public List<string> GetAllProjectNames()
        {
            var lst = _context.Projects;
            var lstProjects = from i in lst
                              let k = i.ProjectName
                              select k;
            return lstProjects.ToList();
        }

        public Projects Update(Projects project)
        {
            var project1 = _context.Projects.Attach(project);
            project1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return project;
        }
    }
}
