using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Input admin name")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "Input admin password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}