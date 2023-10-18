namespace DuLich.Request.DiemThamQuan
{
    public class TimKiemDiemThamQuanRequest
    {
        private string _key;

        public string Key
        {
            get => _key;
            set => _key = value?.ToLower();
        }
    }
}