using HotelManagementMVC.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace HotelManagementMVC.Controllers
{
    public class UserController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public ActionResult Dashboard()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_GetUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return View(dt);
        }
        public ActionResult Edit(int id)
        {
            User user = new User();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user.UserId = Convert.ToInt32(dr["Id"]);
                    user.Username = dr["Username"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Phone = dr["Phone"].ToString();
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
            }
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}