using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demofaq.Models;

namespace demofaq.Controllers
{
    public class FAQSController : Controller
    {
        private faqdemodbContext db = new faqdemodbContext();

        // GET: FAQS
        public ActionResult Index()
        {
            return View(db.FAQS.ToList());
        }

        public PartialViewResult Answers()
        {
            return PartialView("_List_questions", db.FAQS.ToList().OrderByDescending(model => model.Created_at));
        }

        public PartialViewResult AnswersByFirst()
        {
            return PartialView("_List_questions", db.FAQS.ToList().OrderBy(model => model.Created_at));
        }

        // GET: FAQS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQS fAQS = db.FAQS.Find(id);
            if (fAQS == null)
            {
                return HttpNotFound();
            }
            return View(fAQS);
        }

        // GET: FAQS/Create
        public ActionResult Create()
        {
            ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: FAQS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Question,Answer,Name,Email")] FAQS fAQS)
        {
            if (ModelState.IsValid)
            {
                db.FAQS.Add(fAQS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fAQS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion([Bind(Include = "Id,Question,Answer,Name,Email")] FAQS fAQS)
        {
            if (ModelState.IsValid)
            {
                db.FAQS.Add(fAQS);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public void AddQuestionAjax([Bind(Include = "Id,Question,Answer,Name,Email")] FAQS fAQS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.FAQS.Add(fAQS);
                    db.SaveChanges();
                }
            }
            catch (DataException dex)
            {
                ViewBag.Message = dex.Message;
            }
        }

        public ActionResult CreatePartial()
        {
            //For reasons unclear, for a data-driven dropdownlist the ViewBag property has to be the same as the HTML helper string like so:
            //@Html.DropDownList("Category_id", null, htmlAttributes: new { @class = "form-dropdown"})
            //This is not easily explained by the MVC developers
            ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name");
            return PartialView("_Create_faq");
        }

        // GET: FAQS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQS fAQS = db.FAQS.Find(id);
            if (fAQS == null)
            {
                return HttpNotFound();
            }
            return View(fAQS);
        }

        // POST: FAQS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Question,Answer,Name,Email,Created_at")] FAQS fAQS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fAQS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fAQS);
        }

        // GET: FAQS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQS fAQS = db.FAQS.Find(id);
            if (fAQS == null)
            {
                return HttpNotFound();
            }
            return View(fAQS);
        }

        // POST: FAQS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FAQS fAQS = db.FAQS.Find(id);
            db.FAQS.Remove(fAQS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
