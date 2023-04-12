using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
	public class ParoleMinime : ValidationAttribute
	{
		int numeroParole;

		public ParoleMinime(int numeroParole)
		{
			this.numeroParole = numeroParole;
		}

		protected override ValidationResult IsValid(object? value, ValidationContext _)
		{
			var input = value as string;

			if (input is null || input.Trim().Split(' ').Length < numeroParole)
			{
				return new ValidationResult(ErrorMessage ?? $"Please provide at least {numeroParole} word{(numeroParole is 1 ? "" : "s")}.");
			}

			return ValidationResult.Success!;
		}
	}
}
