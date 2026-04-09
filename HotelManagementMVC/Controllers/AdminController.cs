using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace HotelManagementMVC.Controllers
{
    public class AdminController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public ActionResult Dashboard()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.UserCount = GetCount("Users");
            ViewBag.HotelCount = GetCount("Hotels");

            return View();
        }

        private int GetCount(string table)
        {
            int count = 0;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", con);
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }

            return count;
        }
    }
}