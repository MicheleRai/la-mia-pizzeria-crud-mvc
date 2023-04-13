using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Per piacere fornire un nome per la categoria.")]
		[StringLength(100, ErrorMessage = "Il titolo deve avere meno di 100 caratteri.")]
		public string Name { get; set; } = string.Empty;

		public IEnumerable<Pizza>? Pizze { get; set; }
	}
}
