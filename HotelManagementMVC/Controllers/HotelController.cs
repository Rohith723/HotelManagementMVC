using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using HotelManagementMVC.Models;

namespace HotelManagementMVC.Controllers
{
    public class HotelController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public ActionResult Register()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Account");
            Hotel model = new Hotel();
            model.Username = Session["Username"].ToString();
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(Hotel hotel, string[] Amenities, DateTime checkIn, DateTime checkOut, int guests)
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Account");

            hotel.Username = Session["Username"].ToString();
            hotel.Amenities = string.Join(",", Amenities ?? new string[] { });

            int basePrice = 2000;
            int extra = 0;

            if (Amenities != null)
            {
                foreach (var item in Amenities)
                {
                    if (item == "AC") extra += 1000;
                    if (item == "WiFi") extra += 500;
                    if (item == "TV") extra += 300;
                    if (item == "Breakfast") extra += 700;
                }
            }

            int days = (checkOut - checkIn).Days;
            if (days <= 0) days = 1;

            hotel.Price = (basePrice + extra) * days;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_RegisterHotel", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", hotel.Username);
                cmd.Parameters.AddWithValue("@RoomType", hotel.RoomType);
                cmd.Parameters.AddWithValue("@Amenities", hotel.Amenities);
                cmd.Parameters.AddWithValue("@Price", hotel.Price);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            ViewBag.CheckIn = checkIn;
            ViewBag.CheckOut = checkOut;
            ViewBag.Guests = guests;

            return View("BookingSuccess", hotel);
        }


        public ActionResult Details()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Account");

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_GetHotels", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", Session["Username"].ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            ViewBag.Username = Session["Username"].ToString();

            return View(dt);
        }
        public ActionResult Search(string roomType, int guests, string[] Amenities, DateTime checkIn, DateTime checkOut)
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchResult(string roomType)
        {
            Hotel hotel = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT TOP 1 * FROM Hotels WHERE RoomType = @RoomType";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RoomType", roomType);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    hotel = new Hotel()
                    {
                        HotelId = Convert.ToInt32(dr["HotelId"]),
                        Username = dr["Username"].ToString(),
                        RoomType = dr["RoomType"].ToString(),
                        Amenities = dr["Amenities"].ToString()
                    };
                }
            }

            return View(hotel); 
        }
    }
}