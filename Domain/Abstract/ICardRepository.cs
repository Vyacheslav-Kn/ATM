using System.Linq;
using ATM.Domain.Entities;

namespace ATM.Domain.Abstract
{
    public interface ICardRepository
    {
         IQueryable<Card> Cards { get; }
        // интерфейс IQueryable<T> получить последовательность объектов Product и не требует указаний на то, как и где хранятся данные или 
        // как следует их извлекать. Объект IQueryable предоставляет удаленный доступ к базе данных.
        void SaveCard(Card card);
        Card DeleteCard(int cardId);
    }
}
