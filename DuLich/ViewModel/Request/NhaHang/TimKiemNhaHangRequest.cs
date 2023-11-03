using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Request.NhaHang
{
    public class TimKiemNhaHangRequest
    {
        private string _key;

        public string Key
        {
            get => _key;
            set => _key = value?.ToLower();
        }
    }
}