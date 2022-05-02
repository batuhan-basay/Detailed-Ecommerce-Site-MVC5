using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineCommercialSoft.Models.Siniflar;
namespace MvcOnlineCommercialSoft.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PartialRegister()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult PartialRegister(Cariler p)
        {
            c.Carilers.Add(p);
            c.SaveChanges();
            return PartialView();
        }
        /*CURRENT LOGIN*/
        [HttpGet]
        public ActionResult CurrentLogin1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CurrentLogin1(Cariler crr)
        {
            var bilgi = c.Carilers.FirstOrDefault(x => x.CariMail == crr.CariMail && x.CariSifre == crr.CariSifre);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.CariMail, false);
                Session["CariMail"] = bilgi.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var bilgi = c.Admins.FirstOrDefault(x => x.KullaniciAd == admin.KullaniciAd && admin.Sifre == admin.Sifre);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.KullaniciAd, false);
                Session["KullaniciAd"] = bilgi.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }
    }
}