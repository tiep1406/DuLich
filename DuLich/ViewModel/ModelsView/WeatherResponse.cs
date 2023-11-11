namespace ViewModel.ModelsView
{
    public class WeatherResponse
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Timezone { get; set; }
        public int TimezoneOffset { get; set; }
        public Current Current { get; set; }
    }

    public class Current
    {
        public int Dt { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public double Temp { get; set; }

        public double Feels_Like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double Dew_Point { get; set; }

        public double Uvi { get; set; }
        public int Clouds { get; set; }

        public double Visibility { get; set; }

        public double Wind_Speed { get; set; }
        public int Wind_Deg { get; set; }
        public Weather[] Weather { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }

        public string Icon { get; set; }
    }
}