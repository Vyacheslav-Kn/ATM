using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Rollover
    {
        [Required(ErrorMessage = "Input sender card number")]
        public int SenderName { get; set; }
        [Required(ErrorMessage = "Input sender card password")]
        [DataType(DataType.Password)]
        public string UserPin { get; set; }
        [Required(ErrorMessage = "Input sender card cash")]
        public decimal Cash { get; set; }
        [Required(ErrorMessage = "Input receiver card number")]
        public int ReceiverName { get; set; }
    }
}