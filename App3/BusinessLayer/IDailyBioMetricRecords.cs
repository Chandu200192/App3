using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IDailyBioMetricRecords
    {
        public DailyBioMetricRecords Add(DailyBioMetricRecords obj);
        public DailyBioMetricRecords Delete(DailyBioMetricRecords obj);
        public List<DailyBioMetricRecords> GetAllDailyBiometricInfos();
        public List<DailyBioMetricRecords> GetAllDailyBiometricInfosByUserID(int userId);
        public DailyBioMetricRecords GetDailyBiometricRecord(int id);
        public DailyBioMetricRecords Update(DailyBioMetricRecords obj);
        public DailyBioMetricRecords GetUserDailyBiometricRecordByCurrDate(int? empId);
        public DailyBioMetricRecords GetUserRecordToUpdateLogoutInfo(int UserID);
        public void GetUserTotalHoursAndMins(int empId, out double hours, out double mins);

    }
}
