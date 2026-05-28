using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="Prenumele este obligatoriu")]
        [Display(Name="Prenume")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage="Numele este obligatoriu")]
        [Display(Name="Nume")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage="Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage="Email invalid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage="Parola este obligatorie")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength=6, ErrorMessage="Parola trebuie sa aiba minim 6 caractere")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name="Confirma parola")]
        [Compare("Password", ErrorMessage="Parolele nu coincid")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
