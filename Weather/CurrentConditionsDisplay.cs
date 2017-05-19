using System;

namespace Weather
{
	public class CurrentConditionsDisplay : IObserver<Weather>, DisplayElement
	{
		private IDisposable unsubscriber;
		private float temperature;
		private float humidity;
		private IObservable<Weather> weatherData;
		private string name;

		public CurrentConditionsDisplay(string name, IObservable<Weather> observable)
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
			this.temperature = value.Temperature;
			this.humidity = value.Humidity;
			display ();
		}

		public void display() {
			Console.WriteLine ("Current conditions: {0}F degrees and {1}% humidity",
			                   temperature,
			                   humidity);
		}

		public virtual void Unsubscribe()
		{
			unsubscriber.Dispose ();
		}
	}
}

