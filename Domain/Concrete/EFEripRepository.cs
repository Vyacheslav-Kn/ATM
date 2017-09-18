using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;

namespace ATM.Domain.Concrete
{
    public class EFEripRepository : IEripRepository
        {
            private CardDbContext context = new CardDbContext();
            public IQueryable<Erip> Erips
            {
                get { return context.Erips; }
            }
        }
    }