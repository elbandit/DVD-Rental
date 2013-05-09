using System.ComponentModel.DataAnnotations;

namespace Agatha.DVDRental.Operational.UI.Models
{
    public class FilmModel
    {
        [Required]
        [Display(Name = "Film Title")]
        public string Title { get; set; }
    }
}