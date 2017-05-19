using System;

namespace Weather
{
	public class ForecastDisplay : IObserver<Weather>, DisplayElement
	{
		private IDisposable unsubscriber;
		private float currentPressure = 29.9f;
		private float lastPressure;
		private IObservable<Weather> weatherData;
		private string name;

		public ForecastDisplay (string name, IObservable<Weather> observable)
		{
			this.name = name;
			this.weatherData = observable;
			this.Subscribe (this.weatherData);
		}

		public virtual void Subscribe(IObservable<Weather> provider)
		{
			if (provider != null)
				unsubscriber = provider.Subscribe (this);
		}

		public virtual void OnCompleted()
		{
			Console.WriteLine ("Weather Data has completed transmitting data to {0}.", this.name);
			this.Unsubscribe ();
		}

		public virtual void OnError(Exception e)
		{
		}

		public virtual void OnNext(Weather value) {
			lastPressure = currentPressure;
			currentPressure = value.Pressure;

			display ();
		}

		public void display() {
			Console.Write ("Forecast: ");
			if (currentPressure > lastPressure) {
				Console.WriteLine ("Improving weather on the way!");
			} else if (currentPressure == lastPressure) {
				Console.WriteLine ("More of the same");
			} else if (currentPressure < lastPressure) {
				Console.WriteLine ("Watch out for cooler, rainy weather");
			}
		}

		public virtual void Unsubscribe()
		{
			unsubscriber.Dispose ();
		}
	}
}

