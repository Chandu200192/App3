using App3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public class BioMetricInfoRepository : IBioMetricInfo
    {
        private readonly AppDbContext _context;
       
        public BioMetricInfoRepository(AppDbContext appDbContext )
        {
            _context = appDbContext;
        }
        public BioMetricInfo Add(BioMetricInfo obj)
        {
             _context.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public BioMetricInfo Delete(BioMetricInfo obj)
        {
            BioMetricInfo project1 = _context.BioMetricInfoDb.Find(obj.Id);
            if (project1 != null)
            {
                _context.Remove(project1);
                _context.SaveChanges();
            }
            return obj;
        }

        public List<BioMetricInfo> GetAllBiometricInfos()
        {
            return _context.BioMetricInfoDb?.ToList().OrderByDescending(i => i.Date).ToList();
        }

        public List<BioMetricInfo> GetAllBiometricInfosByUserID(int userId)
        {
            return _context.BioMetricInfoDb?.Where(i => i.UserID == userId)?.ToList();
        }

        public BioMetricInfo GetBiometricRecord(int id)
        {
            return _context.BioMetricInfoDb.FirstOrDefault(i => i.Id == id);
        }

        public BioMetricInfo GetUserBiometricRecordByCurrDate(int? empId)
        {
            string currDate = DateTime.Now.ToString("dd/MM/yyyy");
            var empBioRecord = _context.BioMetricInfoDb.Where(i => i.UserID == (int)empId)?.ToList();

           
            if (empBioRecord!= null && empBioRecord.Count>0)
            {
                var currUserDate = from i in empBioRecord
                                   let k = i.Date.ToString("dd/MM/yyyy")
                                   where k == currDate
                                   select i;
                if(currUserDate!=null && currUserDate.Count()>0)
                {
                    return currUserDate.ToList()[0];
                }
                
            }
            return null;
        }


        public List<BioMetricInfo> GetLastSevenBiometricRecordForUser(int userID)
        {
            List<BioMetricInfo> infos = new List<BioMetricInfo>();
            var records = _context.BioMetricInfoDb.Where(i => i.UserID == userID).
                            OrderByDescending(x => x.Date).ToList();
            string currDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (records[0].Date.ToString("dd/MM/yyyy") == currDate)
            {
                infos= records.Skip(1).Take(7).ToList();
                return infos;

            }
            infos = records.Take(7).ToList();
            return infos;
        }

        public List<BioMetricInfo> GetUserBiometricRecordForApproval()
        {
            List<BioMetricInfo> bioMetricInfos = _context.BioMetricInfoDb.
                Where(i => i.approvalType == Utilities.ApprovalType.Applied)?.ToList();
            return bioMetricInfos;
        }

        public BioMetricInfo Update(BioMetricInfo obj)
        {
            var project1 = _context.BioMetricInfoDb.Attach(obj);
            project1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }

        

    }
}
