using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Request.VanChuyen
{
    public class TimKiemVanChuyenRequest
    {
        private string _key;

        public string Key
        {
            get => _key;
            set => _key = value?.ToLower();
        }
    }
}