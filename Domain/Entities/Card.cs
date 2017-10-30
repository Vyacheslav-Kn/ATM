using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ATM.Domain.Entities
{
    public class Card
    { 
        [HiddenInput(DisplayValue = false)]
        public int CardId { get; set; } // уник-е значение в рамках SQl-таблицы
        [Required(ErrorMessage = "Input card number")]
        public int Cardname { get; set; } // уник-ый номер карты
        [Required(ErrorMessage = "Input card activation date")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Input card end date")]
        public DateTime Finish { get; set; }
        [Required(ErrorMessage = "Input card cash")]
        public decimal Cash { get; set; }
        public string Queue { get; set; }
        [Required(ErrorMessage = "Input card password")]
        public string Pin { get; set; }
    }
}
