using System;

namespace Weather
{
	public class StatisticsDisplay : IObserver<Weather>, DisplayElement
	{
		private IDisposable unsubscriber;
		private float maxTemp = 0.0f;
		private float minTemp = 200;
		private float tempSum = 0.0f;
		private int numReadings;
		private string name;
		private IObservable<Weather> weatherData;

		public StatisticsDisplay(string name, IObservable<Weather> observable)
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
			tempSum += value.Temperature;
			numReadings++;

			if (value.Temperature > maxTemp) {
				maxTemp = value.Temperature;
			}

			if (value.Temperature < minTemp) {
				minTemp = value.Temperature;
			}

			display ();
		}

		public void display() {
			Console.WriteLine ("Avg/Max/Min temperature = {0}/{1}/{2}",
			                   tempSum / numReadings,
			                   maxTemp,
			                   minTemp);
		}

		public virtual void Unsubscribe()
		{
			unsubscriber.Dispose ();
		}
	}
}

