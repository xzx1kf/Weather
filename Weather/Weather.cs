using System;

namespace Weather
{
	public struct Weather
	{
		float temperature;
		float humidity;
		float pressure;

		public Weather (float temperature, float humidity, float pressure)
		{
			this.temperature = temperature;
			this.humidity = humidity;
			this.pressure = pressure;
		}

		public float Temperature {
			get {
				return this.temperature;
			}
		}

		public float Humidity {
			get {
				return this.humidity;
			}
		}

		public float Pressure {
			get {
				return this.pressure;
			}
		}	     
	}
}

