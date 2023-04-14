using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models
{
	public class PizzaFormModel
    {
		public Pizza Pizza { get; set; } = new Pizza { Foto = "https://picsum.photos/200/300" };
		public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
		public IEnumerable<SelectListItem> Ingredienti { get; set; } = Enumerable.Empty<SelectListItem>();
		public List<string> IngredientiSelezionati { get; set; } = new();
	}
}
