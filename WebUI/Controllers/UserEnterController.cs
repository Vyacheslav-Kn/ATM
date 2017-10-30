using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ATM.WebUI.Models;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;
using ATM.Domain.Password;
using System;

namespace WebUI.Controllers
{
    public class UserEnterController : Controller
    {
        private ICardRepository repository;
        public UserEnterController(ICardRepository cardRepository)
        {
            this.repository = cardRepository;
        }

        public ViewResult Login()
        {
            return View();
        }

        private bool Authenticate(int username, string userpin)
        {
            Card card = repository.Cards.FirstOrDefault(p => p.Cardname == username);
            try { Password.VerifyHashedPassword(card.Pin, userpin); }
            catch { return false; }
            if (card.Cardname == username && Password.VerifyHashedPassword(card.Pin, userpin)) { return true; }
            else return false;
        }

    [HttpPost]
        public ActionResult Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
               int card_number = int.Parse(model.UserName); 
               if (Authenticate(card_number, model.UserPin))
                {
                    Card card1 = repository.Cards.FirstOrDefault(p => p.Cardname == card_number);
                    return View("Nav2_success", card1);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
