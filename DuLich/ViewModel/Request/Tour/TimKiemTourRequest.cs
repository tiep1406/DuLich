namespace ViewModel.Request.Tour
{
    public class TimKiemTourRequest
    {
        private string _key;

        public string Key
        {
            get => _key;
            set => _key = value?.ToLower();
        }
    }
}