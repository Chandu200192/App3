using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public class DailyBioMetricRecordsRepository : IDailyBioMetricRecords
    {
        private readonly AppDbContext _context;

        public DailyBioMetricRecordsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public DailyBioMetricRecords Add(DailyBioMetricRecords obj)
        {
            int curUserInd = 1;
            string currDate = DateTime.Now.ToString("dd/MM/yyyy");
            var empBioRecord = _context.DailyBioRecordDb.Where(i => i.UserID == (int)obj.UserID)?.ToList();

            if (empBioRecord != null && empBioRecord.Count > 0)
            {
                var currUserLoginIndex = from i in empBioRecord
                                   let k = i.Date.ToString("dd/MM/yyyy")
                                   where k == currDate
                                   orderby i.UserLoginIndex descending
                                   select i.UserLoginIndex
                                   ;
                if (currUserLoginIndex != null && currUserLoginIndex.Count() > 0)
                {
                    curUserInd = currUserLoginIndex.ToList()[0]+1;
                }

            }

            obj.UserLoginIndex = curUserInd;
            _context.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public DailyBioMetricRecords Delete(DailyBioMetricRecords obj)
        {
            DailyBioMetricRecords project1 = _context.DailyBioRecordDb.Find(obj.Id);
            if (project1 != null)
            {
                _context.Remove(project1);
                _context.SaveChanges();
            }
            return obj;
        }

        public List<DailyBioMetricRecords> GetAllDailyBiometricInfos()
        {
            return _context.DailyBioRecordDb?.ToList();
        }

        public List<DailyBioMetricRecords> GetAllDailyBiometricInfosByUserID(int userId)
        {
            return _context.DailyBioRecordDb?.Where(i => i.UserID == userId)?.ToList();
        }

        public DailyBioMetricRecords GetDailyBiometricRecord(int id)
        {
            return _context.DailyBioRecordDb.FirstOrDefault(i => i.Id == id);
        }

        public DailyBioMetricRecords GetUserDailyBiometricRecordByCurrDate(int? empId)
        {
            string currDate = DateTime.Now.ToString("dd/MM/yyyy");
            var empBioRecord = _context.DailyBioRecordDb.Where(i => i.UserID == (int)empId)?.ToList();


            if (empBioRecord != null && empBioRecord.Count > 0)
            {
                var currUserDate = from i in empBioRecord
                                   let k = i.Date.ToString("dd/MM/yyyy")
                                   where k == currDate
                                   select i;
                if (currUserDate != null && currUserDate.Count() > 0)
                {
                    return currUserDate.ToList()[0];
                }

            }
            return null;
        }

        public void GetUserTotalHoursAndMins(int empId, out double hours, out double mins)
        {
            hours = 0;
            mins = 0;
            string currDate = DateTime.Now.ToString("dd/MM/yyyy");
            var empBioRecord = _context.DailyBioRecordDb.Where(i => i.UserID == (int)empId)?.ToList();

            if (empBioRecord != null && empBioRecord.Count > 0)
            {
                var currUserDateRec = from i in empBioRecord
                                   let k = i.Date.ToString("dd/MM/yyyy")
                                   where k == currDate
                                   select i;
                if (currUserDateRec != null && currUserDateRec.Count() > 0)
                {
                    hours = currUserDateRec.Select(i => i.Hours).Sum();
                    mins = currUserDateRec.Select(i => i.Minutes).Sum();
                }
            }
        }

        public DailyBioMetricRecords GetUserRecordToUpdateLogoutInfo(int UserID)
        {
            DailyBioMetricRecords bioMetricRecordsToUpdateLogout = null;
            string currDate = DateTime.Now.ToString("dd/MM/yyyy");
            var empBioRecord = _context.DailyBioRecordDb.Where(i => i.UserID == UserID)?.ToList();

            if (empBioRecord != null && empBioRecord.Count > 0)
            {
                var currUserRecordForLogout = from i in empBioRecord
                                              let k = i.Date.ToString("dd/MM/yyyy")
                                              where k == currDate
                                              orderby i.UserLoginIndex descending
                                              select i;

                if (currUserRecordForLogout != null && currUserRecordForLogout.Count() > 0)
                {
                    bioMetricRecordsToUpdateLogout = currUserRecordForLogout.ToList()[0];
                }
            }
            return bioMetricRecordsToUpdateLogout;
        }

        public DailyBioMetricRecords Update(DailyBioMetricRecords obj)
        {
            var project1 = _context.DailyBioRecordDb.Attach(obj);
            project1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }
}
