using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Controllers;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.InputModels
{
    public class CourseEditInputModel : IValidatableObject
    {
        [Required]
        public int Id {get;set;}

        [Required(ErrorMessage = "Il titolo è obbligatorio"),
        MinLength(10, ErrorMessage = "Il titolo dev'essere di almeno {1} caratteri"),
        MaxLength(199, ErrorMessage = "Il titolo dev'essere di al massimo {1} caratteri"),
        RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Titolo non valido"),
        Remote(action: "IsTitleAvailable", controller: "Courses", ErrorMessage = "Il titolo esiste già", AdditionalFields = "Id"),
        Display(Name = "Titolo")]
        public string Title {get;set;} = string.Empty;

        [MinLength(10, ErrorMessage = "La descrizione dev'essere di almeno {1} caratteri"),
        MaxLength(4000, ErrorMessage = "La descrizione dev'essere di al massimo {1} caratteri"),
        Display(Name = "Descrizione")]
        public string Description {get;set;} = string.Empty;

        [Display(Name = "Immagine rappresentativa")]
        public string? ImagePath {get;set;} 

        [Required(ErrorMessage = "L'email di contatto è obbligatoria"),
        EmailAddress(ErrorMessage = "Devi inserire un indirizzo email"),
        Display(Name = "Email di contatto")]
        public string Email {get;set;} = string.Empty;

        [Required(ErrorMessage = "Il prezzo intero è obbligatorio"),
        Display(Name = "Prezzo intero")]
        public Money FullPrice {get;set;} = new Money();

        [Required(ErrorMessage = "Il prezzo scontato è obbligatorio"),
        Display(Name = "Prezzo scontato")]
        public Money CurrentPrice {get;set;} = new Money();

        [Display(Name = "Nuova Immagine")]
        public IFormFile? Image {get;set;} 

        public byte[]? RowVersion {get;set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(FullPrice.Currency != CurrentPrice.Currency)
            {
                yield return new ValidationResult("Il prezzo intero deve avere la stessa valuta del prezzo scontato");
            }
            else if(FullPrice.Amount < CurrentPrice.Amount)
            {
                yield return new ValidationResult("Il prezzo intero non può essere minore di quello scontato");
            }
        }
    }
}