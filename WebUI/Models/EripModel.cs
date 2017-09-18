using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class EripModel
    {
        [Required(ErrorMessage = "Enter Erip operation number")]
        public string Organization_erip_number { get; set; }
        //[HiddenInput(DisplayValue = false)]
        public string Organization_name { get; set; }
        [Required(ErrorMessage = "Enter your card number")]
        public string Sender_cart_number { get; set; }
        [Required(ErrorMessage = "Enter your password")] 
        //[DataType(DataType.Password)]               
        public string Sender_cart_password { get; set; }
        [Required(ErrorMessage = "Enter Erip cash")]
        public decimal Cash { get; set; }
    }
}