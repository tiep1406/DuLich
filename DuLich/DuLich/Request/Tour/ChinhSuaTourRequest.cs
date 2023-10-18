using System.ComponentModel.DataAnnotations;

namespace DuLich.Request.Tour
{
    public class ChinhSuaTourRequest : ThemTourRequest
    {
        [Required]
        public int Id { get; set; }
    }
}