using System;

namespace Weather
{
	public class HeatIndexDisplay : IObserver<Weather>, DisplayElement
	{
		private IDisposable unsubscriber;
		private float heatIndex = 0.0f;
		private IObservable<Weather> weatherData;
		private string name;

		public HeatIndexDisplay (string name, IObservable<Weather> observable)
		{
			this.name = name;
			this.weatherData = observable;
			this.Subscribe (this.weatherData);
		}

		public virtual void Subscribe (IObservable<Weather> provider)
		{
			if (provider != null)
				unsubscriber = provider.Subscribe (this);
		}

		public virtual void OnCompleted ()
		{
			Console.WriteLine ("Weather Data has completed transmitting data to {0}.", this.name);
			this.Unsubscribe ();
		}

		public virtual void OnError (Exception e)
		{
		}

		public virtual void OnNext (Weather value)
		{
			heatIndex = computeHeatIndex (value.Temperature, value.Humidity);
			display ();
		}

		public void display ()
		{
			Console.WriteLine ("Heat index is {0}", heatIndex);
		}

		private float computeHeatIndex (float t, float rh)
		{
			float index = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh) +
				(0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) +
				(0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
				(0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 * 
				(rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) +
				(0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) + 
				0.000000000843296 * (t * t * rh * rh * rh)) -
				(0.0000000000481975 * (t * t * t * rh * rh * rh)));
			return index;
		}

		public virtual void Unsubscribe ()
		{
			unsubscriber.Dispose ();
		}
	}
}

