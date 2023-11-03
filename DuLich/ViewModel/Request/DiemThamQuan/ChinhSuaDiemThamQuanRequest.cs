using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.DiemThamQuan
{
    public class ChinhSuaDiemThamQuanRequest : ThemDiemThamQuanRequest
    {
        [Required]
        public int Id { get; set; }
    }
}