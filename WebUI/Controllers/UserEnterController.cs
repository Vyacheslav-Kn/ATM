using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;
using ATM.WebUI.Models;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;

namespace WebUI.Controllers
{
    public class UserEnterController : Controller
    {
        private ICardRepository repository;
        public UserEnterController(ICardRepository cardRepository)
        {
            this.repository = cardRepository;
        }

        public ViewResult Login(int n = 0)
        {
            TempData["choice"] = n;
            return View(); }

        private bool Authenticate(int username,string userpin)
        {            
            Card card = repository.Cards.FirstOrDefault(p => p.Cardname == username);
            if (card.Cardname == username && card.Pin == userpin) { return true; } 
            else return false;
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (Authenticate(model.UserName,model.UserPin))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName.ToString(), false);
                    Card card1 = repository.Cards.FirstOrDefault(p => p.Cardname == model.UserName);
                    switch (Convert.ToInt32(TempData["choice"]))
                    {
                        case 0: { return Redirect(Url.Action("List", "Card")); }
                        case 1: { return View("Nav2_success", card1); }
                    }
                }
                else
                {
                    return Redirect(Url.Action("Login", "UserEnter"));
                }
            }
            else
            {
                return View();
            }
            return View();
        }

    }
}
