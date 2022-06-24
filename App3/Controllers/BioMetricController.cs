using App3.BusinessLayer;
using App3.Models;
using App3.Utilities;
using App3.ViewModel;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace App3.Controllers
{
    public class BioMetricController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private IBioMetricInfo _BioMetricinfo;
        private IProject _project;
        private IEmployee _emplyoee;
        private IConfiguration _configuration;
        private IDailyBioMetricRecords _DailyBioRecords;


        public BioMetricController(UserManager<IdentityUser> userManager, IEmployee employee, IBioMetricInfo info,
            IProject project, IConfiguration configuration, IDailyBioMetricRecords dailyBioMetricRecords)
        {
            _emplyoee = employee;
            _userManager = userManager;
            _BioMetricinfo = info;
            _project = project;
            _configuration = configuration;
            _DailyBioRecords = dailyBioMetricRecords;
        }

        public IActionResult TimeSheet()
        {
            int? empId = _emplyoee.GetEmployeeId(User.Identity.Name);
            BioMetricInfo bioMetricInfo = new BioMetricInfo();
            if (empId != null)
            {
                bioMetricInfo = _BioMetricinfo.GetUserBiometricRecordByCurrDate(empId);
            }
            if (bioMetricInfo == null)
            {
                BioMetricInfoViewModel model = new BioMetricInfoViewModel()
                {
                    UserName = User.Identity.Name,
                    UserID = (int)_emplyoee.GetEmployeeId(User.Identity.Name),
                    Date = DateTime.Now,
                    isLogin = true,
                    isLogout = false,
                    Employee = _emplyoee.GetEmployeeUserId((int)empId)

                };
                model.bioMetricInfos = _BioMetricinfo.GetLastSevenBiometricRecordForUser((int)empId);
                return View("TimeSheet", model);
            }
            else
            {

                DateTime dTimeIn = bioMetricInfo.TimeIn;
                DateTime dTimeOut = DateTime.Now;

                TimeSpan span = dTimeOut.Subtract(dTimeIn);

                int hours = span.Hours;
                int min = span.Minutes;

                DailyBioMetricRecords dialyBioRecord = null;
                if (empId != null)
                {
                    dialyBioRecord = _DailyBioRecords.GetUserRecordToUpdateLogoutInfo((int)empId);
                }

                BioMetricInfoViewModel model = new BioMetricInfoViewModel()
                {
                    UserName = User.Identity.Name,
                    UserID = bioMetricInfo.UserID,
                    Date = DateTime.Now,
                    TimeIn = dialyBioRecord.TimeIn,
                    Projects = _project.GetAllProjectNames(),
                    TimeOut = bioMetricInfo.TimeOut,
                    isLogin = false,
                    isLogout = true,
                    Employee = _emplyoee.GetEmployeeUserId((int)empId)

                };
                if (hours > 8)
                {
                    model.strRegularHours = 8.ToString() + " hr : 00 min";
                    model.strExtraHours = (hours - 8).ToString() + " hr :" + min.ToString() + " min";
                }
                else
                {
                    if (hours < 8)
                    {
                        model.strRegularHours = hours.ToString() + " hr : " + min.ToString() + " min";
                        model.strExtraHours = "0 hr : 0 min";
                    }
                    if (hours == 8)
                    {
                        model.strRegularHours = hours.ToString() + " hr : " + "0 min";
                        model.strExtraHours = "0 hr : " + min.ToString() + " min";
                    }
                }
 
                _DailyBioRecords.GetUserTotalHoursAndMins((int)empId, out double shours, out double smin);
                model.strRegularHours = shours.ToString() + " hr : " + smin.ToString();
              
                model.bioMetricInfos = _BioMetricinfo.GetLastSevenBiometricRecordForUser((int)empId);
                return View("TimeSheet", model);
            }

        }

        public JsonResult CheckStatus()
        {
            int? empId = _emplyoee.GetEmployeeId(User.Identity.Name);
            BioMetricInfo bioMetricInfo = new BioMetricInfo();
            if (empId != null)
            {
                bioMetricInfo = _BioMetricinfo.GetUserBiometricRecordByCurrDate(empId);
            }
            if (bioMetricInfo == null)
            {
                var data = new { isLogin = "True", isLogout = "False" };
                return Json(data);
            }
            else if (bioMetricInfo.isLogin)
            {
                var data = new { isLogin = "False", isLogout = "True" };
                return Json(data);
            }
            else
            {
                var data = new { isLogin = "True", isLogout = "False" };
                return Json(data);
            }
        }

        [HttpPost]
        public JsonResult TimeSheetLogin(BioMetricInfoViewModel model)
        {

            int? empId = _emplyoee.GetEmployeeId(User.Identity.Name);
            BioMetricInfo bioMetricInfo = null;
            if (empId != null)
            {
                bioMetricInfo = _BioMetricinfo.GetUserBiometricRecordByCurrDate(empId);
            }

            if (ModelState.IsValid)
            {
                if (bioMetricInfo == null)
                {
                    BioMetricInfo info = new BioMetricInfo()
                    {
                        UserName = model.UserName,
                        UserID = (int)_emplyoee.GetEmployeeId(User.Identity.Name),
                        Date = DateTime.Today.Date,
                        TimeIn = DateTime.Now,
                        TimeOut = model.TimeOut,
                        RegularHours = model.RegularHours,
                        ExtraHours = model.ExtraHours,
                        Description = model.Description,
                        Issues = model.Issues,
                        isLogin = true
                    };
                    _BioMetricinfo.Add(info);

                    DateTime dTimeIn = DateTime.Now;
                    DateTime dTimeOut = DateTime.Now;

                    TimeSpan span = dTimeOut.Subtract(dTimeIn);

                    int dhours = span.Hours;
                    int dmin = span.Minutes;

                    DailyBioMetricRecords dailyBioMetricRecords = new DailyBioMetricRecords()
                    {
                        UserName = model.UserName,
                        UserID = (int)_emplyoee.GetEmployeeId(User.Identity.Name),
                        Date = DateTime.Today.Date,
                        TimeIn = DateTime.Now,
                        TimeOut = DateTime.Now,
                        Hours = dhours,
                        Minutes = dmin
                    };
                    _DailyBioRecords.Add(dailyBioMetricRecords);

                    var data1 = new { isLogin = "False", isLogout = "True" };
                    return Json(data1);

                }
                else
                {
                     bioMetricInfo.isLogin = true;
                    _BioMetricinfo.Update(bioMetricInfo);

  
                    DateTime dTimeIn = DateTime.Now;
                    DateTime dTimeOut = DateTime.Now;

                    TimeSpan span = dTimeOut.Subtract(dTimeIn);

                    int dhours = span.Hours;
                    int dmin = span.Minutes;

                    DailyBioMetricRecords dailyBioMetricRecords = new DailyBioMetricRecords()
                    {
                        UserName = model.UserName,
                        UserID = (int)_emplyoee.GetEmployeeId(User.Identity.Name),
                        Date = DateTime.Today.Date,
                        TimeIn = DateTime.Now,
                        TimeOut = DateTime.Now,
                        Hours = dhours,
                        Minutes = dmin
                    };
                    _DailyBioRecords.Add(dailyBioMetricRecords);

                    var data1 = new { isLogin = "False", isLogout = "True" };
                    return Json(data1);
                }
            }

            var data = new { isLogin = "True", isLogout = "False" };
            return Json(data);
        }
        [HttpPost]
        public IActionResult TimeSheetLogout(BioMetricInfoViewModel model)
        {
            int? empId = _emplyoee.GetEmployeeId(User.Identity.Name);
            BioMetricInfo bioMetricInfo = new BioMetricInfo();
            if (empId != null)
            {
                bioMetricInfo = _BioMetricinfo.GetUserBiometricRecordByCurrDate(empId);
            }

            DailyBioMetricRecords dialyBioRecord = null;
            if (empId != null)
            {
                dialyBioRecord = _DailyBioRecords.GetUserRecordToUpdateLogoutInfo((int)empId);
            }


            if (ModelState.IsValid)
            {

                DateTime dTimeIn = dialyBioRecord.TimeIn;
                DateTime dTimeOut = DateTime.Now;
                TimeSpan span = dTimeOut.Subtract(dTimeIn);

                int dhours = span.Hours;
                int dmin = span.Minutes;

                dialyBioRecord.TimeOut = DateTime.Now;
                dialyBioRecord.Hours = dhours;
                dialyBioRecord.Minutes = dmin;
                _DailyBioRecords.Update(dialyBioRecord);

                _DailyBioRecords.GetUserTotalHoursAndMins((int)empId, out double hours, out double min);

                if (hours > 8)
                {
                    bioMetricInfo.RegularHours = 8;
                    bioMetricInfo.RegularMin = 0;
                    bioMetricInfo.ExtraHours = hours - 8;
                    bioMetricInfo.ExtraMin = min;
                }
                else 
                {
                    if(hours < 8)
                    {
                        bioMetricInfo.RegularHours = hours;
                        bioMetricInfo.RegularMin = min;
                        bioMetricInfo.ExtraHours = 0;
                        bioMetricInfo.ExtraMin = 0;
                    }
                    if(hours == 8)
                    {
                        bioMetricInfo.RegularHours = hours;
                        bioMetricInfo.RegularMin = 0;
                        bioMetricInfo.ExtraHours = 0;
                        bioMetricInfo.ExtraMin = min;
                    }
                  
                }
                bioMetricInfo.TimeOut = DateTime.Now;
                bioMetricInfo.Description = model.Description;
                bioMetricInfo.Issues = model.Issues;
                bioMetricInfo.isLogin = false;
                _BioMetricinfo.Update(bioMetricInfo);
 
            }  
            var data1 = new { isLogin = "True", isLogout = "False" };
            return Json(data1);
        }

        public IActionResult UserLogout(BioMetricInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? empId = _emplyoee.GetEmployeeId(User.Identity.Name);
                BioMetricInfo bioMetricInfo = new BioMetricInfo();
                if (empId != null)
                {
                    bioMetricInfo = _BioMetricinfo.GetUserBiometricRecordByCurrDate(empId);
                }
                if (bioMetricInfo != null)
                {
                    bioMetricInfo.TimeOut = model.TimeOut;
                    bioMetricInfo.Description = model.Description;
                    bioMetricInfo.Issues = model.Issues;
                    _BioMetricinfo.Update(bioMetricInfo);
                    return RedirectToAction("Summary");
                }
            }
            return View("TimeSheet", model);
        }

        public IActionResult Summary()
        {
            List<BioMetricInfo> bioMetricInfos = new List<BioMetricInfo>();
            if(User.IsInRole("Admin"))
            {
                bioMetricInfos = _BioMetricinfo.GetAllBiometricInfos();
            }
            else
            {
                bioMetricInfos = _BioMetricinfo.GetAllBiometricInfosByUserID((int)_emplyoee.GetEmployeeId(User.Identity.Name));
            }
            return View("History", bioMetricInfos);
        }

        public IActionResult WeeklyData()
        {
            DataTable table = new DataTable();
            table = GetEmpBioWeekly();

            List<WeeklyDataViewModel> weeklyList = new List<WeeklyDataViewModel>();
            weeklyList = BiometricUtility.ConvertToList<WeeklyDataViewModel>(table);
            if(User.IsInRole("Admin"))
            {
                return View(weeklyList);
            }
            var lst = weeklyList.Where(i => i.UserId == (int)_emplyoee.GetEmployeeId(User.Identity.Name)).ToList();
            return View(lst);
        }

        public IActionResult MonthlyData()
        {
            DataTable table = new DataTable();
            table = GetEmpBioMonthly();

            List<MonthlyDataViewModel> monthList = new List<MonthlyDataViewModel>();
            monthList = BiometricUtility.ConvertToList<MonthlyDataViewModel>(table);

            if (User.IsInRole("Admin"))
            {
                return View(monthList);
            }
            var lst = monthList.Where(i => i.UserId == (int)_emplyoee.GetEmployeeId(User.Identity.Name)).ToList();
            return View(lst);
        }

        public JsonResult GetEmployeeID()
        {
           if(User.IsInRole("Admin"))
           {
                var lstEmpIDs = _emplyoee.GetAllEmployees().Select(i => i.EmployeeID).ToList<int>();
                return Json(lstEmpIDs);
           }
            var EmpID = (int)_emplyoee.GetEmployeeId(User.Identity.Name);
            return Json(EmpID);          
        }

        public IActionResult DailyBioRec()
        {
            List<DailyBioMetricRecords> dailyBioMetricRecords = new List<DailyBioMetricRecords>();
            if (User.IsInRole("Admin"))
            {
                dailyBioMetricRecords = _DailyBioRecords.GetAllDailyBiometricInfos();
            }
            else
            {
                dailyBioMetricRecords = _DailyBioRecords.GetAllDailyBiometricInfos().
                                        Where(i => i.UserID == (int)_emplyoee.GetEmployeeId(User.Identity.Name)).ToList();
            }
            return View("DailyBioRecInfo", dailyBioMetricRecords);
        }

        public IActionResult DailyBioRecById()
        {
            List<DailyBioMetricRecords> dailyBioMetricRecords = _DailyBioRecords.GetAllDailyBiometricInfos();
            return View("DailyBioRecInfo", dailyBioMetricRecords);
        }

        public IActionResult BioReports()
        {
            BioRecordReportViewModel bioRecordReportViewModel = new BioRecordReportViewModel()
            {   
                FromDate = DateTime.Now,
                ToDate = DateTime.Now         
            };

            return View("Reports",bioRecordReportViewModel);
        }

        [HttpPost]
        public IActionResult BioReports(BioRecordReportViewModel reportViewModel)
        {
            if (ModelState.IsValid)
            {
                int userid = reportViewModel.UserID;
                DateTime fromDate = reportViewModel.FromDate;
                DateTime toDate = reportViewModel.ToDate;
                string reportName = reportViewModel.reportRadios;

                if(reportName != "" && reportName != null)
                {
                   return ExportToExcel(reportName);
                }
            }
            BioRecordReportViewModel bioRecordReportViewModel = new BioRecordReportViewModel();
            return View("Reports", bioRecordReportViewModel);
        }

        private IActionResult ExportToExcel(string strReportName)
        {
            string sqlQuery = null;
            string reportName = "Report";
            if (strReportName == "SummaryReport")
            {
                sqlQuery = "select UserName, UserID ,Date ,TimeIn ,TimeOut ,RegularHours" +
                    ",ExtraHours ,Description ,Issues ,Project ,ExtraMin ,RegularMin from BioMetricInfoDb";

                reportName = "SummaryReport";
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = GetDataTable(sqlQuery);
                
                wb.Worksheets.Add(dt,"sheet1");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                   return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        reportName+"_"+DateTime.Now.ToString()+".xlsx");
                }
            }
        }

        [HttpPost]
        public JsonResult GetBioWeeklyWithEmployeeID(string empid)
        {
            DataTable table = new DataTable();
            table = GetEmpBioWeekly();

            int id = Convert.ToInt32(empid);

            List<WeeklyDataViewModel> Studentlist = new List<WeeklyDataViewModel>();
            Studentlist = BiometricUtility.ConvertToList<WeeklyDataViewModel>(table);

           if(id == -1)
           {
              return Json(Studentlist);
           }

            var lstEmpIDs = Studentlist.Where(i => i.UserId == id);
            return Json(lstEmpIDs);
        }

        public JsonResult GetBioMonthlyWithEmployeeID(string empid)
        {
            DataTable table = new DataTable();
            table = GetEmpBioWeekly();

            int id = Convert.ToInt32(empid);

            List<MonthlyDataViewModel> Studentlist = new List<MonthlyDataViewModel>();
            Studentlist = BiometricUtility.ConvertToList<MonthlyDataViewModel>(table);

            if (id == -1)
            {
                return Json(Studentlist);
            }

            var lstEmpIDs = Studentlist.Where(i => i.UserId == id);
            return Json(lstEmpIDs);
        }

        private DataTable GetEmpBioWeekly()
        {
            //string strQuery = "SELECT j1.UserID, DATEPART(iso_week, j1.Date) AS Week, SUM(RegularHours) AS RegularHours," +
            //    " SUM(RegularMin) AS RegularMin, SUM(ExtraHours) AS ExtraHours, SUM(ExtraMin) AS ExtraMin FROM BioMetricInfoDb j1 " +
            //    "WHERE Convert(nvarchar(11), DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0), 112) <= j1.Date " +
            //    "AND j1.Date < Convert(nvarchar(11), DATEADD(yy, DATEDIFF(yy, 0, GETDATE()) + 1, 0), 112)" +
            //    " GROUP BY j1.UserID, DATEPART(iso_week, j1.Date)" + " ORDER BY DATEPART(iso_week, j1.Date) ";

            string strQuery = "SELECT UserID, YEAR(Date) as CurYear, MONTH(Date) CurMonth, DATENAME(MONTH,Date) as CurMonthName," +
                "(DATEPART(week, Date) - DATEPART(week, DATEADD(day, 1, EOMONTH(Date, -1)))) + 1 as weekno, " +
                "Sum(RegularHours) as RegularHours, " +
                " Sum(RegularMin) as RegularMin, Sum(ExtraHours) as ExtraHours,  " +
                "Sum(ExtraMin) as ExtraMin  from BioMetricInfoDb " +
                "GROUP BY UserID, Year(Date), MONTH(Date),  DATENAME(MONTH, Date)," + 
                "(DATEPART(week, Date) - DATEPART(week, DATEADD(day, 1, EOMONTH(Date, -1)))) + 1 " +
                "ORDER BY 1,2,3";

            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                SqlDataAdapter myAdapter = new SqlDataAdapter(strQuery, connection);
                // Creating SqlCommand objcet   
                SqlCommand cm = new SqlCommand(strQuery, connection);
                // Opening Connection  
                connection.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();              
                dataTable.Load(sdr);
            }

            return dataTable;

        }

        private DataTable GetEmpBioMonthly()
        {
            string strQuery = "SELECT UserID, YEAR(Date) as CurYear, MONTH(Date) CurMonth, DATENAME(MONTH, Date) as CurMonthName, " +
                "Sum(RegularHours) as RegularHours, Sum(RegularMin) as RegularMin, Sum(ExtraHours) as ExtraHours, " +
                "Sum(ExtraMin) as ExtraMin FROM BioMetricInfoDb GROUP BY UserID, Year(Date), MONTH(Date), " +
                "DATENAME(MONTH, Date) ORDER BY 1,2,3 ";

            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                SqlDataAdapter myAdapter = new SqlDataAdapter(strQuery, connection);
                // Creating SqlCommand objcet   
                SqlCommand cm = new SqlCommand(strQuery, connection);
                // Opening Connection  
                connection.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();
                dataTable.Load(sdr);
            }

            return dataTable;

        }

        private string GetConnectionString()
        {
            string connString = _configuration.GetConnectionString("AppDB3Connection");
            return connString;
        }

        private DataTable GetDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                SqlDataAdapter myAdapter = new SqlDataAdapter(query, connection);
                // Creating SqlCommand objcet   
                SqlCommand cm = new SqlCommand(query, connection);
                // Opening Connection  
                connection.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();
                dataTable.Load(sdr);
            }
            return dataTable;
        }

        public IActionResult EditBiometricRecord(int Id)
        {            
            if(ModelState.IsValid)
            {
               BioMetricInfo bioMetricInfo =  _BioMetricinfo.GetBiometricRecord(Id);
                if(bioMetricInfo != null)
                {
                    return View("EditTimeSheet", bioMetricInfo);
                }
            }
            return View("History");
        }
        [HttpPost]
        public IActionResult EditBiometricRecord(BioMetricInfo model)
        {
            
            BioMetricInfo bioMetricInfo = _BioMetricinfo.GetBiometricRecord(model.Id);

            if (ModelState.IsValid)
            {            
                if (bioMetricInfo != null)
                {
                    bioMetricInfo.UserID = model.UserID;
                    bioMetricInfo.UserName = model.UserName;
                    bioMetricInfo.Date = model.Date;
                    bioMetricInfo.TimeIn = model.TimeIn;
                    bioMetricInfo.TimeOut = model.TimeOut;
                    bioMetricInfo.RegularHours = model.RegularHours;
                    bioMetricInfo.RegularMin = model.RegularMin;
                    bioMetricInfo.ExtraHours = model.ExtraHours;
                    bioMetricInfo.ExtraMin = model.ExtraMin;

                    _BioMetricinfo.Update(bioMetricInfo);

                    return RedirectToAction("Summary");
                }
            }
            return View("EditTimeSheet", bioMetricInfo);
        }

        public IActionResult RequestExtraTime(int id)
        {
            BioMetricInfo bioMetricInfo = _BioMetricinfo.GetBiometricRecord(id);
            return View("ExtraTimeRequest", bioMetricInfo);
        }


        public IActionResult RequestExtraTimeByAdmin(int id)
        {
            BioMetricInfo bioMetricInfo = _BioMetricinfo.GetBiometricRecord(id);
            return View("EditUserOTRequestDataByAdmin", bioMetricInfo);
        }

        public IActionResult GetEmpExtraTimeApproval()
        {
           List<BioMetricInfo> bioMetricInfos = _BioMetricinfo.GetUserBiometricRecordForApproval();
           return View("EmpExtraTimeApproval", bioMetricInfos);
        }

        [HttpPost]
        public IActionResult UpdateEmpExtraTimeApproval(string[] Id)
        {
            if(Id.Length >0)
            {
                for (int i = 0; i < Id.Length; i++)
                {
                    int index = Convert.ToInt32(Id[i]);
                    BioMetricInfo bioMetricInfo = _BioMetricinfo.GetBiometricRecord(index);
                    bioMetricInfo.approvalType = ApprovalType.Approved;

                    _BioMetricinfo.Update(bioMetricInfo);
                }
                List<BioMetricInfo> bioMetricInfos = _BioMetricinfo.GetUserBiometricRecordForApproval();
                return View("EmpExtraTimeApproval", bioMetricInfos);
            }
            List<BioMetricInfo> bioMetricInfos1 = _BioMetricinfo.GetUserBiometricRecordForApproval();
            return View("EmpExtraTimeApproval", bioMetricInfos1);
        }

        [HttpPost]
        public IActionResult UpdateEmpExtraTimeApprovalRejection(string[] Id)
        {
            if (Id.Length > 0)
            {
                for (int i = 0; i < Id.Length; i++)
                {
                    int index = Convert.ToInt32(Id[i]);
                    BioMetricInfo bioMetricInfo = _BioMetricinfo.GetBiometricRecord(index);
                    bioMetricInfo.OTHours = 0;
                    bioMetricInfo.OTMin = 0;
                    bioMetricInfo.approvalType = ApprovalType.Rejected;

                    _BioMetricinfo.Update(bioMetricInfo);
                }
                List<BioMetricInfo> bioMetricInfos = _BioMetricinfo.GetUserBiometricRecordForApproval();
                return View("EmpExtraTimeApproval", bioMetricInfos);
            }
            List<BioMetricInfo> bioMetricInfos1 = _BioMetricinfo.GetUserBiometricRecordForApproval();
            return View("EmpExtraTimeApproval", bioMetricInfos1);
        }


        public IActionResult RequestForOTApproval(BioMetricInfo bioMetricInfo)
        {
             bioMetricInfo.approvalType = ApprovalType.Applied;
            _BioMetricinfo.Update(bioMetricInfo);
            return RedirectToAction("Summary");
        }

        [HttpPost]
        public IActionResult EditAndApproveOT(BioMetricInfo bioMetricInfo)
        {
            bioMetricInfo.approvalType = ApprovalType.Approved;
            _BioMetricinfo.Update(bioMetricInfo);
            List<BioMetricInfo> bioMetricInfos = _BioMetricinfo.GetUserBiometricRecordForApproval();
            return View("EmpExtraTimeApproval", bioMetricInfos);
        }

    }
}
