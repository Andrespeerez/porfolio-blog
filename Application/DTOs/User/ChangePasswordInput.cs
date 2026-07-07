using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.DTOs.User;

public record ChangePasswordInput(
    [property: Required, Password] string OldPassword,
    [property: Required, Password] string NewPassword,
    [property: Compare(nameof(ChangePasswordInput.NewPassword), ErrorMessage = "Las contraseñas nuevas no coinciden.")] string RepeatNewPassword
);