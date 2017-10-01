using System.Web.Mvc;

namespace ATM.Domain.Entities
{
    public class Erip
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; } // уник-е значение в рамках SQl-таблицы
        public string Eripnumber { get; set; } // уник-ый номер erip операции
        public string Eripinfo { get; set; }
    }
}
