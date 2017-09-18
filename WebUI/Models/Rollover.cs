using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class Rollover
    {
        [Required]
        public int SenderName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPin { get; set; }
        [Required]
        public decimal Cash { get; set; }
        [Required]
        public int ReceiverName { get; set; }
    }
}