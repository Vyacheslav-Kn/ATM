using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ATM.Domain.Entities
{
    public class Erip
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Eripnumber { get; set; }
        public string Eripinfo { get; set; }
    }
}
