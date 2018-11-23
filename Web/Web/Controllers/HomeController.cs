using BLL;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using Web.Models;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {          
        return View();
        }

        [HttpPost]
        public ActionResult Index(string startDate,string endDate)
        {
            List<SalesReportUnit> result = null;
            ViewBag.Error = "";
            try
            {

                if (!DateTime.TryParseExact(startDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime start))
                {
                    ViewBag.Error = ViewBag.Error + "\n The start date is not correct.";
                }
                if (!DateTime.TryParseExact(endDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime end))
                {
                    ViewBag.Error = ViewBag.Error + "\n The end date is not correct.";
                }

                if (ViewBag.Error == "")
                {
                    result = Report(start, end).ToList();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ViewBag.Error + $"\n An error occurred on the server. {ex.Message}.";
            }
            return View(result); 
        }

        [HttpPost]
        public string SendMsgWithReport(string startAndEndDateAndEmail)
        {
            ResultSalesReportUnit result=new ResultSalesReportUnit();
            DateTime start=DateTime.MinValue, end = DateTime.MinValue;
            StartAndEndDateAndEmail startAndEndDateAndEmailView;
            try
            {
                startAndEndDateAndEmailView = JsonConvert.DeserializeObject<StartAndEndDateAndEmail>(startAndEndDateAndEmail);
            }
            catch(Exception ex)
            {
                return $"\n An error occurred on the server. {ex.Message}.";
            }
            
            if (string.IsNullOrEmpty(startAndEndDateAndEmailView.Email))
            {
                return "Email is not correct.";
            }


            if (result.Error.Count == 0)
            {
                try
                {
                    List<SalesReportUnit> report = Report((DateTime)startAndEndDateAndEmailView.StartDate, (DateTime)startAndEndDateAndEmailView.EndDate);
                    MessageManager msg = new MessageManager();
                    using (var stream = new MemoryStream())
                    using (var writer = new StreamWriter(stream))
                    {
                        StringBuilder reportToFile = new StringBuilder();
                        
                        for (int i = 0; i < report.Count; i++)
                        {
                            reportToFile.Append(report[i].OrderId.ToString() + " ; "
                                + report[i].OrderDate.ToString() + " ; " + report[i].MarkingOfProduct + " ; "
                                + report[i].NameProduct + " ; " + report[i].UnitsOnOrder.ToString() + " ; "
                                + report[i].UnitPrice.ToString() + $" ;=E{i + 1}*F{i + 1}\n");
                        }
                        writer.WriteLine(reportToFile);
                        writer.Flush();
                        stream.Position = 0;
                        msg.SendMsgWithFile(startAndEndDateAndEmailView.Email, "Отчёт по продажам", "Отчёт по продажам", new Attachment(stream, "filename.csv", "text/csv"));
                        return "The message is sent.";
                    }
                }
                catch(Exception ex)
                {
                    return $"An error occurred on the server. {ex.Message}";
                }
            }
            return JsonConvert.SerializeObject(result);
        }

        public List<SalesReportUnit> Report(DateTime start, DateTime end)
        {
            List<SalesReportUnit> request;
            using (NorthwindEntities northwinddb = new NorthwindEntities())
            {
                request = (from o in northwinddb.Orders
                          join od in northwinddb.OrderDetails on o.ID equals od.OrderID
                          join pr in northwinddb.Products on od.ProductID equals pr.ID
                           where o.OrderDate!=null && pr.UnitsOnOrder!=0 && EntityFunctions.TruncateTime(o.OrderDate)>= EntityFunctions.TruncateTime(start) && EntityFunctions.TruncateTime(o.OrderDate) <= EntityFunctions.TruncateTime(end)
                           select new SalesReportUnit { OrderId = o.ID, OrderDate = o.OrderDate, MarkingOfProduct = "", NameProduct = pr.Name, UnitsOnOrder = pr.UnitsOnOrder, UnitPrice = pr.UnitPrice }).ToList();
                
            }
            return request;
        }
    }
}