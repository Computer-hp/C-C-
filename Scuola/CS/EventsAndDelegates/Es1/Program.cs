using System;

namespace MyNameSpace
{
    public class TemperatureChangedEventArgs : EventArgs
    {
        public double NewTemperature {get; set;}
        public TemperatureChangedEventArgs(double temperature)
        {
            NewTemperature = temperature;
        }
    }
    
    public class TemperatureSensor
    {
        private double currentTemperature;
        private Random random = new Random();
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        public TemperatureSensor(double temperature)
        {
            currentTemperature = temperature;

            this.TemperatureChanged += TemperatureHandle.HandleTemperatureChanged;
        }

        public void Start()
        {
            currentTemperature = currentTemperature * random.Next(0, 100);

            OnTemperatureChanged(new TemperatureChangedEventArgs(currentTemperature));
        }

        public void OnTemperatureChanged(TemperatureChangedEventArgs e)
        {
            TemperatureChanged?.Invoke(this, e);
        }
    }

    public class TemperatureHandle
    {
        /*public TemperatureHandle(TemperatureSensor sensor)
        {
            sensor.TemperatureChanged += HandleTemperatureChanged;
        }*/

        public static void HandleTemperatureChanged(object sender, TemperatureChangedEventArgs e)
        {
            if (sender != null)
            {
                Console.WriteLine("New temperature = " + e.NewTemperature);
            }
        }
    }

    public class Program
    {
        static void Main()
        {
            var sensor = new TemperatureSensor(1);

            

            sensor.Start();
        }

    }
}
