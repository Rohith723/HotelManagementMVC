using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HotelManagementMVC.Controllers
{
    public class StockMarketController : Controller
    {


        private string[] x_Axis =
        {
            "PAYTM.NSE",
            "HDFCBANK.NSE",
            "KOTAKBANK.NSE",
            "SBIN.NSE",
            "CANBK.NSE"
        };

        private string[] y_Axis =
        {
            "1000",
            "1500",
            "2000",
            "2500",
            "3000"
        };
        // GET: StockMarket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ColumnChart()
        {
            return CreateChart("Column");
        }

        public ActionResult LineChart()
        {
            return CreateChart("Line");
        }

        public ActionResult BarChart()
        {
            return CreateChart("Bar");
        }

        public ActionResult PieChart()
        {
            return CreateChart("Pie");
        }

        public ActionResult AreaChart()
        {
            return CreateChart("Area");
        }

        public ActionResult DoughnutChart()
        {
            return CreateChart("Doughnut");
        }

        private FileResult CreateChart(string chartType)
        {
            var chart = new Chart(width: 600, height: 400)
                .AddTitle("Stock Market - " + chartType)
                .AddSeries(
                    chartType: chartType,
                    xValue: x_Axis,
                    yValues: y_Axis);

            return File(chart.GetBytes("png"), "image/png");

        }

        public ActionResult Graph()
        {
            return View();
        }

        // This provides realtime data to AJAX
        public JsonResult GetStockData()
        {
            string[] symbols =
            {
                "PAYTM.NSE",
                "HDFCBANK.NSE",
                "KOTAKBANK.NSE",
                "SBIN.NSE",
                "CANBK.NSE"
            };

            List<string> x_Axis = new List<string>();
            List<double> y_Axis = new List<double>();

            string apiKey = "f05dd9cea93e4e5c96446e8754403d67";

            foreach (var symbol in symbols)
            {
                string url = "https://api.twelvedata.com/price?symbol=" + symbol + "&apikey=" + apiKey;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                string json = reader.ReadToEnd();

                JObject obj = JObject.Parse(json);

                double price = Convert.ToDouble(obj["price"]);

                x_Axis.Add(symbol);
                y_Axis.Add(price);
            }

            return Json(new
            {
                xAxis = x_Axis,
                yAxis = y_Axis
            }, JsonRequestBehavior.AllowGet);
        }
    }
}