using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CardController : Controller
    {
        private ICardRepository repository; private IEripRepository eriprepository;
        public CardController(ICardRepository cardRepository, IEripRepository eripRepositorycreate)
        {
            this.repository = cardRepository;
            this.eriprepository = eripRepositorycreate;
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        public ViewResult List()
        {
            return View();
        }

        public ViewResult Nav1()
        {
            return View();
        }
        public ViewResult Nav1_eripmodel(string org_erip)
        {
            Erip check1 = eriprepository.Erips.FirstOrDefault(p => p.Eripnumber == org_erip);
            return View(new EripModel { Organization_erip_number = check1.Eripnumber, Organization_name = check1.Eripinfo });
        }
        public ViewResult Nav1_erip_find()
        {
            return View(new EripModel {});
        }
        [HttpPost]
        public ViewResult Nav1_erip(EripModel model)
        {
            Erip check1 = eriprepository.Erips.FirstOrDefault(p => p.Eripnumber == model.Organization_erip_number);
            int n = Convert.ToInt32(model.Sender_cart_number);
            Card check2 = repository.Cards.FirstOrDefault(p => p.Cardname == n);
            if (check2.Pin == model.Sender_cart_password && check2.Cash >= model.Cash) {
                check2.Cash = check2.Cash - model.Cash; 

                DateTime thisDate = DateTime.Now; string time = thisDate.ToString(@"MM\/dd\/yyyy HH:mm");
                if (check2.Queue == null) {check2.Queue =  time + " " + check1.Eripinfo + ";";
                    repository.SaveCard(check2); TempData["result"] = "Operation has been performed!";
                    return View("List");
                }
                if (check2.Queue != null ) {
                    int count = (check2.Queue.Length - check2.Queue.Replace(";", "").Length) / ";".Length; // кол-во вхождений
                    int[] numbers = new int[count]; int t = 0;
                    Queue<string> info2 = new Queue<string>();
                    for (int i = 0; i < check2.Queue.Length; i++) {
                            if (check2.Queue[i].ToString() == ";")
                            {
                                numbers[t] = i; t++;
                            }
                    }
                    int a1 = 0; int k = 0;
                        while (k != count)
                        {
                            info2.Enqueue((check2.Queue.Substring(a1, numbers[k] - a1 + 1)));
                            a1 = numbers[k] + 1; k++;
                        }
                    
                    if (info2.Count >= 10) { info2.Dequeue(); info2.Enqueue(time + " " + check1.Eripinfo + ";");
                        foreach (string s in info2) { check2.Queue = check2.Queue + s; }
                    }
                    else { info2.Enqueue(time + " " + check1.Eripinfo + ";");
                        string test = null;
                        foreach (string s in info2) { test = test + s + " "; } 
                        check2.Queue = test;
                    }                    
                }
                repository.SaveCard(check2); TempData["result"] = "Operation has been performed!";
                return View("List"); }
            else { return View("Nav1_erip_find"); }
        }


        public ActionResult Nav2()
        {
            return RedirectToAction("Login","UserEnter",new {n = 1});
        }       

        public ViewResult Nav3()
        {
            return View(new Rollover());
        }
        [HttpPost]
        public ViewResult Nav3(Rollover trans)
        {
            Card card1 = repository.Cards.FirstOrDefault(p => p.Cardname == trans.SenderName);
            Card card2 = repository.Cards.FirstOrDefault(p => p.Cardname == trans.ReceiverName);
            if (card1.Pin == trans.UserPin && card1.Cash >= trans.Cash)
            {
                card2.Cash = card2.Cash + trans.Cash; repository.SaveCard(card2);
                card1.Cash = card1.Cash - trans.Cash; repository.SaveCard(card1);
                TempData["result"] = "Operation has been performed!";
                return View("List");
            }
            else { return View(); }            
        }

    }
}
