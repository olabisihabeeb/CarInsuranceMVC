using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarInsuranceMVC.Models;

namespace CarInsuranceMVC.Controllers
{
    public class InsureesController : Controller
    {
        private InsuranceContext db = new InsuranceContext();

        // ===== Helpers =====
        private decimal CalculateQuote(Insuree i)
        {
            decimal total = 50m; // base

            int age = GetAge(i.DateOfBirth);
            if (age <= 18) total += 100m;
            else if (age >= 19 && age <= 25) total += 50m;
            else total += 25m;

            if (i.CarYear < 2000) total += 25m;
            if (i.CarYear > 2015) total += 25m;

            var make = (i.CarMake ?? "").Trim();
            var model = (i.CarModel ?? "").Trim();
            if (make.Equals("Porsche", StringComparison.OrdinalIgnoreCase))
            {
                total += 25m;
                if (model.Equals("911 Carrera", StringComparison.OrdinalIgnoreCase))
                    total += 25m; // additional
            }

            total += 10m * i.SpeedingTickets;

            if (i.DUI) total *= 1.25m;          // +25%
            if (i.CoverageType) total *= 1.50m; // full coverage +50%

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private int GetAge(DateTime dob)
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }

        // ===== CRUD =====

        // GET: Insurees
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var insuree = db.Insurees.Find(id);
            if (insuree == null) return HttpNotFound();
            return View(insuree);
        }

        // GET: Insurees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
            "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,DUI,CoverageType")]
            Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var insuree = db.Insurees.Find(id);
            if (insuree == null) return HttpNotFound();
            return View(insuree);
        }

        // POST: Insurees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
            "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,DUI,CoverageType")]
            Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var insuree = db.Insurees.Find(id);
            if (insuree == null) return HttpNotFound();
            return View(insuree);
        }

        // POST: Insurees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}


