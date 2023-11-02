using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.Tour
{
    public class ChinhSuaTourRequest : ThemTourRequest
    {
        [Required]
        public int Id { get; set; }
    }
}