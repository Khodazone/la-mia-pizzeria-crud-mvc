using csharp_ef_pizze;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> ourPizze = db.Pizza.ToList();
                return View(ourPizze);
            }


        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? dettagliPizze = db.Pizza.Where(pizze => pizze.Id == id).FirstOrDefault();

                if (dettagliPizze != null)
                {
                    return View("Details", dettagliPizze);
                }
                else
                {
                    return NotFound($"La pizza con id {id} non è stato trovato!");
                }
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

    }
}
