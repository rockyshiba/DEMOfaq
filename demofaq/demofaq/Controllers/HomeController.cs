using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demofaq.Models;

namespace demofaq.Controllers
{
    public class HomeController : Controller
    {
        faqdemodbContext db = new faqdemodbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.FAQS.ToList());
        }

        //[HttpPost]
        //public ActionResult Index(FAQS faq)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FAQS.Add(faq);
        //        db.SaveChanges();
        //        return View();
        //    }

        //    return View(faq);
        //}
    }
}