using System;
using System.Linq;
using System.Web.Mvc;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;
using ATM.WebUI.Models;
using System.Web.Security;
using ATM.Domain.Password;
using System.Web;
using System.Net;

namespace WebUI.Controllers
{
    
    public class AdminController : Controller
    {
        private ICardRepository repository;
        public AdminController(ICardRepository cardRepository)
        {
            this.repository = cardRepository;
        }

        public ViewResult Login()
        { return View(); }

        private bool Authenticate(string admin_name, string password)
        {
            if (admin_name == "slava" && Password.VerifyHashedPassword(Password.HashPassword("060299"), password)) { return true; }
            else return false;
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Authenticate(model.AdminName, model.Password))
                {
                    var ticket = new FormsAuthenticationTicket(
                      1,
                      model.AdminName,
                      DateTime.Now,
                      DateTime.Now.Add(FormsAuthentication.Timeout),
                      false,
                      string.Empty,
                      FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    var encTicket = FormsAuthentication.Encrypt(ticket);
                    // Create the cookie.
                    var AuthCookie = new HttpCookie(model.AdminName)
                    {
                        Value = encTicket,
                        Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
                    };
                    HttpContext.Response.Cookies.Set(AuthCookie);

                    return RedirectToAction("CardsAdminList");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ViewResult CardsAdminList(string admin_name = "slava")
        {
            HttpCookie authCookie = HttpContext.Request.Cookies.Get(admin_name);
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value)) { 
                return View(repository.Cards);
            }
            else { FormsAuthentication.RedirectToLoginPage(); }
            return View(repository.Cards);
        }

        public ViewResult Edit(int CardId)
        {
            Card card = repository.Cards.FirstOrDefault(p => p.CardId == CardId);
            return View(card);
        }

        [HttpPost]
        public ActionResult Edit(Card Card)
        {
            if (ModelState.IsValid)
            {
                bool truth = repository.Cards.Any(p => p.Cardname == Card.Cardname);
                if (Card.CardId == 0)
                {
                    if (truth == true) { TempData["message"] = string.Format("Check Cardnumber"); return RedirectToAction("Create", new { Card.Cardname }); }
                    else {
                        try { Card.Pin = Password.HashPassword(Card.Pin); }
                        catch (Exception e) { return View(Card); }
                        repository.SaveCard(Card); return RedirectToAction("CardsAdminList"); }
                } 

                else
                {
                    int name = (repository.Cards.FirstOrDefault(p => p.CardId == Card.CardId)).Cardname;
                    if (name == Card.Cardname) {
                        if ( (repository.Cards.FirstOrDefault(p => p.CardId == Card.CardId)).Pin != Card.Pin) { 
                            try { Card.Pin = Password.HashPassword(Card.Pin); }
                            catch (Exception e) { return View(Card); }
                        }
                        repository.SaveCard(Card); return RedirectToAction("CardsAdminList"); }
                    else
                    {
                        if (truth == true) { TempData["message"] = string.Format("Check Cardnumber"); return RedirectToAction("Edit", new { Card.CardId }); }
                        else {
                            try { Card.Pin = Password.HashPassword(Card.Pin); }
                            catch (Exception e) { return View(Card); }
                            repository.SaveCard(Card); return RedirectToAction("CardsAdminList"); }
                    }
                }
            }
            else
            {
                return View(Card);
            }
        }

        public ViewResult Create(int CardName = 0)
        {
            return View("Edit", new Card { Cardname = CardName });
        }

        [HttpPost]
        public ActionResult Delete(int CardId)
        {
            Card deletedCard = repository.DeleteCard(CardId);
            return RedirectToAction("CardsAdminList");
        }

        public ActionResult Logins(int CardId = 0)
        {
            if (CardId != 0)
            {
                Card deletedCard = repository.DeleteCard(CardId);
                return PartialView(repository.Cards);
            }
            else return PartialView(repository.Cards); 
        }

    }
}