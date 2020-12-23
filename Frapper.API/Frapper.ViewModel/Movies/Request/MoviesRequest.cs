using System.ComponentModel.DataAnnotations;

namespace Frapper.ViewModel.Movies.Request
{
    public class MoviesCreateRequest
    {
        [Required(ErrorMessage = "Enter Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Director")]
        [Display(Name = "Director")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Enter Genre")]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Enter Title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter ReleaseYear")]
        [Display(Name = "ReleaseYear")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Enter Rating")]
        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [Required(ErrorMessage = "Enter MovieLength")]
        [Display(Name = "MovieLength")]
        public string MovieLength { get; set; }
    }
}