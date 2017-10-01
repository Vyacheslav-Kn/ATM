using System.Linq;
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