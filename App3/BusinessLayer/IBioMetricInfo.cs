using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IBioMetricInfo
    {
        public BioMetricInfo Add(BioMetricInfo obj);
        public BioMetricInfo Delete(BioMetricInfo obj);
        public List<BioMetricInfo> GetAllBiometricInfos();
        public List<BioMetricInfo> GetAllBiometricInfosByUserID(int userId);
        public BioMetricInfo GetBiometricRecord(int id);
        public BioMetricInfo Update(BioMetricInfo obj);
        public BioMetricInfo GetUserBiometricRecordByCurrDate(int? empId);
        public List<BioMetricInfo> GetUserBiometricRecordForApproval();
        public List<BioMetricInfo> GetLastSevenBiometricRecordForUser(int userID);
    }
}
