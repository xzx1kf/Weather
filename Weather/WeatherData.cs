using System;
using System.Collections.Generic;

namespace Weather
{
	public class WeatherData : IObservable<Weather>
	{
		private Weather weather;
		private List<IObserver<Weather>> observers;

		public WeatherData ()
		{
			observers = new List<IObserver<Weather>> ();
		}

		public IDisposable Subscribe(IObserver<Weather> o)
		{
			if (! observers.Contains (o))
				observers.Add (o);
			return new Unsubscriber (observers, o);
		}

		private class Unsubscriber : IDisposable
		{
			private List<IObserver<Weather>> _observers;
			private IObserver<Weather> _observer;

			public Unsubscriber(List<IObserver<Weather>> observers, IObserver<Weather> observer)
			{
				this._observers = observers;
				this._observer = observer;
			}

			public void Dispose()
			{
				if (_observer != null && _observers.Contains (_observer))
					_observers.Remove (_observer);
			}
		}

		public void measurementsChanged(Nullable<Weather> weather) {
			foreach (var observer in observers) {
				observer.OnNext(weather.Value);
			}
		}

		public void setMeasurements(float temperature, float humidity, float pressure)
		{
			this.weather = new Weather (temperature, humidity, pressure);
			measurementsChanged (weather);
		}

		public void EndTransmission()
		{
			foreach (var observer in observers.ToArray())
				if (observers.Contains (observer))
					observer.OnCompleted ();

			observers.Clear ();
		}
	}
}

