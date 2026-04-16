using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementMVC.Controllers
{
    public class CalculatorController : Controller
    {
        public string GetResult(string str)
        {
            List<char> symbleList = new List<char>();
            char[] charSymble = { '+', '-', '*', '/' };
            string[] strList = str.Split(charSymble);
            for (int i = 0; i < strList.Length; i++)
            {
                if (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/')
                {
                    symbleList.Add(str[i]);
                }
            }
            double result = Convert.ToDouble(strList[0]);
            for (int i = 1; i < strList.Length; i++)
            {
                double num = Convert.ToDouble(strList[i]);
                int j = i - 1;
                switch (symbleList[j])
                {
                    case '+':
                        result += num;
                        break;
                    case '-':
                        result -= num;
                        break;
                    case '*':
                        result *= num;
                        break;
                    case '/':
                        if (num != 0)
                            result /= num;
                        else
                            return "Error: Division by zero";
                        break;
                }
            }
            return result.ToString();
        }
        // GET: Calculator
        public ActionResult Index()
        {
            if (Request["txt"] != null)
            {
                if (Request["txt"][Request["txt"].Length - 1] == '+' || Request["txt"][Request["txt"].Length - 1] == '-' || Request["txt"][Request["txt"].Length - 1] == '*' || Request["txt"][Request["txt"].Length - 1] == '/')
                {
                    ViewBag.flag = "Please enter a valid expression without ending with an operator.";
                    ViewBag.Result = "Invalid expression" + Request["txt"];
                }
                else
                {
                    ViewBag.Result = GetResult(Request["txt"]);
                    ViewBag.flag = "Result for the expression: " + Request["txt"];
                }
            }
            return View();
        }
    }
}