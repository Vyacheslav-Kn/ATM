using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;

namespace ATM.Domain.Concrete
{
   public class EFCardRepository: ICardRepository
    {
        private CardDbContext context = new CardDbContext();
        public IQueryable<Card> Cards
        {
            get { return context.Cards; }
        }

        public void SaveCard(Card Card)
        {
            int n = 0;
            if (Card.CardId == 0)
            {
                context.Cards.Add(Card);
            }
            else
            {
                Card dbEntry = context.Cards.Find(Card.CardId);
                if (dbEntry != null)
                {
                    dbEntry.Cardname = Card.Cardname;
                    dbEntry.Start = Card.Start;
                    dbEntry.Finish = Card.Finish;
                    dbEntry.Cash = Card.Cash;
                   dbEntry.Queue = Card.Queue; 
                    dbEntry.Pin = Card.Pin;
                }
            }
              context.SaveChanges();
        }

        public Card DeleteCard(int CardID)
        {
            Card dbEntry = context.Cards.Find(CardID);
            if (dbEntry != null)
            {
                context.Cards.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
