using System.Linq;
using System.Web.Mvc;
using CarInsuranceMVC.Models;

namespace CarInsuranceMVC.Controllers
{
    public class AdminController : Controller
    {
        private InsuranceContext db = new InsuranceContext();

        // GET: Admin
        public ActionResult Index()
        {
            var insurees = db.Insurees.ToList();
            return View(insurees);
        }
    }
}
