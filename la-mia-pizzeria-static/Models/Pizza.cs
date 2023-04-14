using la_mia_pizzeria_static.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Per piacere fornire un nome per la pizza.")]
		[StringLength(100, ErrorMessage = "Il nome deve contenere meno di 100 characters.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Per piacere fornire una descrizione per la Pizza.")]
		[Column(TypeName = "text")]
		[ParoleMinime(4)]
		public string Description { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;

		[Required(ErrorMessage = "Per piacere fornire un prezzo per la pizza.")]
		public double Prezzo { get; set; }

		[Nonzero(ErrorMessage = "Per piacere scegliere una categoria.")]
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }
		public List<Ingrediente>? Ingredienti { get; set;}
	}
}
