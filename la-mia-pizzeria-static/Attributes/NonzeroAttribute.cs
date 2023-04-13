using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
	public class NonzeroAttribute :ValidationAttribute
	{
		protected override ValidationResult IsValid(object? value, ValidationContext _)
		{
			var input = value as int?;

			if (input is null or 0)
			{
				return new ValidationResult(ErrorMessage ?? $"Il valore non può essere 0.");
			}

			return ValidationResult.Success!;
		}
	}
}
