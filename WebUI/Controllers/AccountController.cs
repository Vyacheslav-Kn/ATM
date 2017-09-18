using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ATM.WebUI.Models;
using ATM.Domain.Abstract;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ICardRepository repository;
        public AccountController(ICardRepository cardRepository)
        {
            this.repository = cardRepository;
        }

        public ViewResult Login()
        { return View(); }

        private bool Authenticate(string username, string password)
        {
           if (username == "slava" && password == "060299") { return true; }        
            else return false; 
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Authenticate(model.AdminName,model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.AdminName, false);
                    return Redirect(Url.Action("CardsAdminList", "Admin"));
                }
                else
                {
                    return Redirect(Url.Action("Login", "Account"));
                }
            }
            else
            {
                return View();
            }
        }
    }
}