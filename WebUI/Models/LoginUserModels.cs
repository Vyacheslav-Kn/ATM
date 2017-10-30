using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.WebUI.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "Input card number")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Input card password")]
        [DataType(DataType.Password)]
        public string UserPin { get; set; }
    }
}