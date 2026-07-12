using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validators;

[AttributeUsage(
    AttributeTargets.Property,
    AllowMultiple = false
)]
public sealed class PasswordAttribute : ValidationAttribute
{
    private static readonly Regex PasswordRegex = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?\"":{}|<>ñÑ])\S+$", RegexOptions.Compiled);

    private readonly int _minLength;
    private readonly int _maxLength;

    public PasswordAttribute(int minLength = 8, int maxLength = 100)
    {
        _minLength = minLength;
        _maxLength = maxLength;
        ErrorMessage = "La contraseña no cumple los requisitos";
    }

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext
    )
    {
        string? password = value as string;

        if(password!.Length < _minLength)
        {
            return new ValidationResult($"La contraseña debe de tener mínimo {_minLength} caracteres.");
        }

        if(password!.Length > _maxLength)
        {
            return new ValidationResult($"La contraseña no puede tener más de {_maxLength} caracteres.");
        }

        if(!PasswordRegex.IsMatch(password!))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}