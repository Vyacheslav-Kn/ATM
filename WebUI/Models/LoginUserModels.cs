using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.WebUI.Models
{
    public class LoginUserModel
    {
        [Required]
        public int UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPin { get; set; }
    }
}