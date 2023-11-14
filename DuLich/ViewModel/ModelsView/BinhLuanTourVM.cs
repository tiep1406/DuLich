using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelsView
{
    public class BinhLuanTourVM
    {
        public int? IdTour { get; set; }

        public int? IdNguoiDung { get; set; }
        public string NoiDung { get; set; }
        public double Rating { get; set; }
    }
}