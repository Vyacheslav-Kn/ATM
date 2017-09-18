using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ATM.Domain.Entities
{
    public class Card
    { 
        [HiddenInput(DisplayValue = false)] // для визуализации элемента в форме как скрытого
        public int CardId { get; set; } // уник-е значение в рамках SQl-таблицы
        [Required(ErrorMessage = "Введите номер карты")]
        public int Cardname { get; set; } // уник-ый номер карты
        [Required(ErrorMessage = "Введите дату активации карты")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Введите дату конца срока карты")]
        public DateTime Finish { get; set; }
        [Required(ErrorMessage = "Введите сумму средств на карте")]
        public decimal Cash { get; set; }
       // [HiddenInput(DisplayValue = false)]
        public string Queue { get; set; }
        [Required(ErrorMessage = "Введите пароль карты")]
        public string Pin { get; set; }
    }
}
