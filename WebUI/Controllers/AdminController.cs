﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private ICardRepository repository;
        public AdminController(ICardRepository cardRepository)
        {
            this.repository = cardRepository;
        }

        public RedirectResult Index()
        {
            return Redirect("/Account/Login");
        }

        public ViewResult CardsAdminList()
        {
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
    if (truth == true) { TempData["message"] = string.Format("Check Cardnumber");  return RedirectToAction("Create", new { Card.Cardname }); }
                    else { repository.SaveCard(Card); return RedirectToAction("CardsAdminList"); }
                }

                else
                {
                    int name = (repository.Cards.FirstOrDefault(p => p.CardId == Card.CardId)).Cardname;
                    if (name == Card.Cardname) { repository.SaveCard(Card); return RedirectToAction("CardsAdminList"); }
                    else
                    {
     if (truth == true) { TempData["message"] = string.Format("Check Cardnumber"); return RedirectToAction("Edit", new { Card.CardId }); }
                        else { repository.SaveCard(Card); return RedirectToAction("CardsAdminList"); }
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

    }
}
