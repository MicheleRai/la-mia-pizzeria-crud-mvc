using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var ctx = new PizzeriaContext();
            var pizze = ctx.Pizze.ToArray();

            return View(pizze);
        }

        public IActionResult Dettagli(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizze.SingleOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }

            return View(pizza);
        }
		public IActionResult Create()
		{
            var pizza = new Pizza();

			return View(pizza);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Pizza data)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", data);
			}

			using (PizzeriaContext ctx = new PizzeriaContext())
			{
				Pizza pizzaToCreate = new Pizza();
				pizzaToCreate.Name = data.Name;
				pizzaToCreate.Description = data.Description;
				pizzaToCreate.Foto = data.Foto;
				pizzaToCreate.Prezzo = data.Prezzo;

				ctx.Pizze.Add(pizzaToCreate);
				ctx.SaveChanges();

				return RedirectToAction("Index");
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}