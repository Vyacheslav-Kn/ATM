using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Domain.Entities;
using System.Data.Entity;

namespace ATM.Domain.Concrete
{
        public class CardDbContext : DbContext
        {
public DbSet<Card> Cards { get; set; } // класс будет автоматически определять свойство для каждой таблицы в базе данных, с которой мы будем работать
public DbSet<Erip> Erips { get; set; } 
    }
}
