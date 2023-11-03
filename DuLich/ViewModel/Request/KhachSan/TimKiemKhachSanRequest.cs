using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Request.KhachSan
{
    public class TimKiemKhachSanRequest
    {
        private string _key;

        public string Key
        {
            get => _key;
            set => _key = value?.ToLower();
        }
    }
}