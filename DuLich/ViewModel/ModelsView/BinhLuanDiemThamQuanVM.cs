using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel.ModelsView
{
    public class BinhLuanDiemThamQuanVM
    {
        public int? IdDiemThamQuan { get; set; }

        public int? IdNguoiDung { get; set; }
        public string NoiDung { get; set; }
        public double Rating { get; set; }
    }
}