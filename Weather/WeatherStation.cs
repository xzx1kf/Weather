using System;

namespace Weather
{
	public class WeatherStation
	{
		public static void Main()
		{
			WeatherData weatherData = new WeatherData ();

			CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay ("Current Conditions Display", weatherData);
			StatisticsDisplay statisticsDisplay = new StatisticsDisplay ("Statistics Display", weatherData);
			ForecastDisplay forecastDisplay = new ForecastDisplay ("Forecast Display", weatherData);
			HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay ("Heat Index Display", weatherData);

			weatherData.setMeasurements (80, 65, 30.4f);
			weatherData.setMeasurements (82, 70, 29.2f);
			weatherData.setMeasurements (78, 90, 29.2f);

			//currentDisplay.Unsubscribe ();
			//statisticsDisplay.Unsubscribe ();

			weatherData.EndTransmission ();
		}
	}
}

