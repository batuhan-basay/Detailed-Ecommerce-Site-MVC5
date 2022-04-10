using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialSoft.Models.Siniflar;
namespace MvcOnlineCommercialSoft.Controllers
{
    public class GaleriController : Controller
    {
        Context c = new Context();
        // GET: Galeri
        public ActionResult Index()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
    }
}