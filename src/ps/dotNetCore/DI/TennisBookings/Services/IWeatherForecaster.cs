namespace TennisBookings.Services
{
    public interface IWeatherForecaster
    {
        WeatherResult GetCurrentWeather();
        int GetCurrentDegree();
    }
}