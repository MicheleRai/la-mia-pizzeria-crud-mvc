using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
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
			
			var pizze = _context.Pizze.ToArray();

			return View(pizze);
		}

		public IActionResult Dettagli(int id)
		{
			var pizza = _context.Pizze.SingleOrDefault(p => p.Id == id);

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
		public IActionResult Create(Pizza pizza)
		{
			if (!ModelState.IsValid)
			{
				return View(pizza);
			}

			_context.Pizze.Add(pizza);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		public IActionResult Update(int id)
		{
			var pizza = _context.Pizze.FirstOrDefault(p => p.Id == id);

			if (pizza is null)
			{
				return View("NotFound");
			}
			return View(pizza);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, Pizza pizza)
		{
			if (!ModelState.IsValid)
			{
				return View(pizza);
			}
			var savedPizza = _context.Pizze.AsNoTracking().FirstOrDefault(p => p.Id == id);

			if (savedPizza is null)
			{
				return View("NotFound");
			}

			_context.Pizze.Update(pizza);
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