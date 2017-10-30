using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ATM.Domain.Abstract;
using ATM.Domain.Entities;
using WebUI.Models;
using ATM.Domain.Password;

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
            return View(new EripModel { });
        }
        [HttpPost]
        public ViewResult Nav1_erip(EripModel model)
        {
            if (!ModelState.IsValid) { return View("Nav1_erip_find"); } 

            Erip check1 = eriprepository.Erips.FirstOrDefault(p => p.Eripnumber == model.Organization_erip_number);
            if (check1 == null)
            {
                string erip = (TempData["Erip"]).ToString();
                check1 = eriprepository.Erips.FirstOrDefault(p => p.Eripnumber == erip);
            }
            int n = Convert.ToInt32(model.Sender_cart_number);
            Card check2 = repository.Cards.FirstOrDefault(p => p.Cardname == n);
            if (Password.VerifyHashedPassword(check2.Pin, model.Sender_cart_password) && check2.Cash >= model.Cash) 
            {
                check2.Cash = check2.Cash - model.Cash;

                DateTime thisDate = DateTime.Now; string time = thisDate.ToString(@"MM\/dd\/yyyy HH:mm");
                if (check2.Queue == null)
                {
                    check2.Queue = time + " " + check1.Eripinfo + ";";
                    repository.SaveCard(check2); TempData["result"] = "Operation has been performed!";
                    return View("List");
                }
                else
                {
                    int count = (check2.Queue.Length - check2.Queue.Replace(";", "").Length) / ";".Length; // кол-во вхождений
                    int[] numbers = new int[count]; int t = 0;
                    Queue<string> info2 = new Queue<string>();
                    for (int i = 0; i < check2.Queue.Length; i++)
                    {
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

                    if (info2.Count >= 10)
                    {
                        info2.Dequeue(); info2.Enqueue(time + " " + check1.Eripinfo + ";");
                        string test = null;
                        foreach (string s in info2) { test = test + s + " "; }
                        check2.Queue = test;
                    }
                    else
                    {
                        info2.Enqueue(time + " " + check1.Eripinfo + ";");
                        string test = null;
                        foreach (string s in info2) { test = test + s + " "; }
                        check2.Queue = test;
                    }
                }
                repository.SaveCard(check2); TempData["result"] = "Operation has been performed!";
                return View("List");
               }
            else { return View("List"); }
        }
    

        public ActionResult Nav2()
        {
            return RedirectToAction("Login", "UserEnter");
        }

        public ViewResult Nav3()
        {
            return View(new Rollover());
        }

        private void queue_check(Card card1, Rollover trans)
        {
            DateTime thisDate = DateTime.Now; string time = thisDate.ToString(@"MM\/dd\/yyyy HH:mm");
            if (card1.Queue == null)
            {
                if (card1.Cardname == trans.SenderName) { card1.Queue = time + " Send " + trans.Cash + " rubles to card " + trans.ReceiverName + ";"; repository.SaveCard(card1); }
                else { card1.Queue = time + " Get " + trans.Cash + " rubles from card " + trans.SenderName + ";"; repository.SaveCard(card1); }
            }
            else 
            {
                int count = (card1.Queue.Length - card1.Queue.Replace(";", "").Length) / ";".Length; // кол-во вхождений
                int[] numbers = new int[count]; int t = 0;
                Queue<string> info2 = new Queue<string>();
                for (int i = 0; i < card1.Queue.Length; i++)
                {
                    if (card1.Queue[i].ToString() == ";")
                    {
                        numbers[t] = i; t++;
                    }
                }
                int a1 = 0; int k = 0;
                while (k != count)
                {
                    info2.Enqueue((card1.Queue.Substring(a1, numbers[k] - a1 + 1)));
                    a1 = numbers[k] + 1; k++;
                }

                if (info2.Count >= 10)
                {
                    info2.Dequeue(); 
                    if (card1.Cardname == trans.SenderName) { info2.Enqueue(time + " Send " + trans.Cash + " rubles to card " + trans.ReceiverName + ";"); }
                    else { info2.Enqueue(time + " Get " + trans.Cash + " rubles from card " + trans.SenderName + ";"); }
                    string test = null;
                    foreach (string s in info2) { test = test + s + " "; }
                    card1.Queue = test;
                }
                else
                {
                    if (card1.Cardname == trans.SenderName) { info2.Enqueue(time + " Send " + trans.Cash + " rubles to card " + trans.ReceiverName + ";"); }
                    else { info2.Enqueue(time + " Get " + trans.Cash + " rubles from card " + trans.SenderName + ";"); }
                    string test = null;
                    foreach (string s in info2) { test = test + s + " "; }
                    card1.Queue = test;
                }
                repository.SaveCard(card1);
            }
        }
        
        [HttpPost]
        public ViewResult Nav3(Rollover trans)
        {
            Card card1, card2; Rollover t;
            try
            {
                card1 = repository.Cards.FirstOrDefault(p => p.Cardname == trans.SenderName);  t = trans;
                card2 = repository.Cards.FirstOrDefault(p => p.Cardname == trans.ReceiverName);
                if(t.Cash < 0 || card1 == null || card2 == null ) { return View(); }
            }
            catch { return View(); }
            try { Password.VerifyHashedPassword(card1.Pin, trans.UserPin); }
            catch { return View(); }
            if (Password.VerifyHashedPassword(card1.Pin, trans.UserPin) && card1.Cash >= trans.Cash)
            {
                card2.Cash = card2.Cash + trans.Cash; queue_check(card2, t);
                card1.Cash = card1.Cash - trans.Cash; queue_check(card1, t);
                TempData["result"] = "Operation has been performed!";
                return View("List");
            }
            else { return View(); }
        }
    }
}

