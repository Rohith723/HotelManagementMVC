using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagementMVC.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        public string Username { get; set; }

        [Required]
        public string RoomType { get; set; }

        public string Amenities { get; set; }
        public int Price { get; set; }
    }
}