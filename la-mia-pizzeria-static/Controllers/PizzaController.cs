using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
	public class PizzaController : Controller
	{
		private readonly ILogger<PizzaController> _logger;
		private readonly PizzeriaContext _context;

		public PizzaController(ILogger<PizzaController> logger, PizzeriaContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			
			var pizze = _context.Pizze.Include(p => p.Category).ToArray();

			return View(pizze);
		}

		public IActionResult Dettagli(int id)
		{
			var pizza = _context.Pizze.Include(p => p.Category).Include(p => p.Ingredienti).SingleOrDefault(p => p.Id == id);

			if (pizza is null)
			{
				return NotFound($"Pizza with id {id} not found.");
			}

			return View(pizza);
		}
		public IActionResult Privacy()
		{

			return View();
		}
		public IActionResult Create()
		{
			var formModel = new PizzaFormModel()
			{
				Categories = _context.Categories.ToArray(),
				Ingredienti = _context.Ingredienti.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray(),
			};

			return View(formModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PizzaFormModel form)
		{
			if (!ModelState.IsValid)
			{
				form.Categories = _context.Categories.ToArray();
				form.Ingredienti = _context.Ingredienti.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();

				return View(form);
			}

			_context.Pizze.Add(form.Pizza);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		public IActionResult Update(int id)
		{
			var pizza = _context.Pizze.Include(p => p.Ingredienti).FirstOrDefault(p => p.Id == id);

			if (pizza is null)
			{
				return View("NotFound");
			}
			var formModel = new PizzaFormModel
			{
				Pizza = pizza,
				Categories = _context.Categories.ToArray(),
				Ingredienti = _context.Ingredienti.ToArray().Select(i => new SelectListItem( 
					i.Name, 
					i.Id.ToString(), 
					pizza.Ingredienti!.Any(_i => _i.Id == i.Id))
				).ToArray()
			};

			formModel.IngredientiSelezionati = formModel.Ingredienti.Where(i => i.Selected).Select(i => i.Value).ToList();

			return View(formModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, PizzaFormModel form)
		{
			if (!ModelState.IsValid)
			{
				form.Categories = _context.Categories.ToArray();
				form.Ingredienti = _context.Ingredienti.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();


				return View(form);
			}
			var savedPizza = _context.Pizze.Include(p => p.Ingredienti).FirstOrDefault(p => p.Id == id);

			if (savedPizza is null)
			{
				return View("NotFound");
			}

			savedPizza.Name = form.Pizza.Name;
			savedPizza.Description = form.Pizza.Description;
			savedPizza.Category = form.Pizza.Category;
			savedPizza.Foto = form.Pizza.Foto;
			savedPizza.Prezzo = form.Pizza.Prezzo;
			savedPizza.Ingredienti = form.Pizza.Ingredienti;

			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var pizzeDaCancellare = _context.Pizze.FirstOrDefault(p => p.Id == id);

			if (pizzeDaCancellare is null)
			{
				return View("NotFound");
			}

			_context.Pizze.Remove(pizzeDaCancellare);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}