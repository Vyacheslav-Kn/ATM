using System.Linq;
using ATM.Domain.Entities;

namespace ATM.Domain.Abstract
{
    public interface IEripRepository
    {
        IQueryable<Erip> Erips { get; }
    }
}
 