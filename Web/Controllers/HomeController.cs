﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using Web.Models;
using Excel = Microsoft.Office.Interop.Excel;

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
        public ActionResult Index(string startDate, string endDate)
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
            ResultSalesReportUnit result = new ResultSalesReportUnit();
            DateTime start = DateTime.MinValue, end = DateTime.MinValue;
            StartAndEndDateAndEmail startAndEndDateAndEmailView;
            try
            {
                startAndEndDateAndEmailView = JsonConvert.DeserializeObject<StartAndEndDateAndEmail>(startAndEndDateAndEmail);
            }
            catch (Exception ex)
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
                    
                    using (var stream = new System.IO.MemoryStream())
                        using (ExcelPackage package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Subscribers");
                           
                        for (int i = 2, j = 0; j < report.Count ; i++, j++)
                             {
                                 workSheet.Cells[i, 1].Value = report[j].OrderId.ToString();
                                 workSheet.Cells[i, 2].Value = report[j].OrderDate.ToString();
                                 workSheet.Cells[i, 3].Value = report[j].MarkingOfProduct;
                                 workSheet.Cells[i, 4].Value = report[j].NameProduct;
                                 workSheet.Cells[i, 5].Value = report[j].UnitsOnOrder.ToString();
                                 workSheet.Cells[i, 6].Value = report[j].UnitPrice.ToString();
                                 workSheet.Cells[i, 7].Formula = $"= E{i}*F{i}";
                             }
                        package.Save();
                        stream.Position = 0;
                        msg.SendMsgWithFile(startAndEndDateAndEmailView.Email, "Отчёт по продажам", "Отчёт по продажам", new Attachment(stream, "Reports.xlsx", "text/xlsx"));
                        return "The message is sent.";

                    }
                }
                catch (Exception ex)
                {
                    return $"An error occurred on the server. {ex.Message}";
                }
            }
            return JsonConvert.SerializeObject(result);
        }


        public List<SalesReportUnit> Report(DateTime start, DateTime end)
        {
            List<SalesReportUnit> request;
            using (NorthwindContext northwinddb = new NorthwindContext())
            {
                request = (from o in northwinddb.Order
                           join od in northwinddb.OrderDetail on o.Id equals od.OrderId
                           join pr in northwinddb.Product on od.ProductId equals pr.Id
                           where o.OrderDate != null && pr.UnitsOnOrder != 0 && o.OrderDate >= start && o.OrderDate <= end
                           select new SalesReportUnit { OrderId = o.Id, OrderDate = o.OrderDate, MarkingOfProduct = "", NameProduct = pr.Name, UnitsOnOrder = pr.UnitsOnOrder, UnitPrice = pr.UnitPrice }).ToList();

            }
            return request;
        }
    }
}
