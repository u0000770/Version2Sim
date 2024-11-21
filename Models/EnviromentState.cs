namespace HeaterSim.Models
{
    public class EnvironmentState
    {
        public List<TemperatureSensor> Sensors { get; set; }
        public List<HeatingControl> Heaters { get; set; }
        public List<FanControl> Fans { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? SimulationStartTime { get; set; }

        public EnvironmentState()
        {
            Sensors = new List<TemperatureSensor>();
            Heaters = new List<HeatingControl>();
            Fans = new List<FanControl>();
            LastUpdated = DateTime.Now;

            // Automatically start a session
            SimulationStartTime = DateTime.Now;

            // Initialize default sensors
            for (int i = 1; i <= 3; i++)
            {
                Sensors.Add(new TemperatureSensor
                {
                    Id = i,
                    CurrentTemperature = 18.0 // Default temperature
                });
            }

            // Initialize default heaters
            for (int i = 1; i <= 3; i++)
            {
                Heaters.Add(new HeatingControl
                {
                    Id = i,
                    Level = 0 // Default level
                });
            }

            // Initialize default fans
            for (int i = 1; i <= 3; i++)
            {
                Fans.Add(new FanControl
                {
                    Id = i,
                    IsOn = false // Default state
                });
            }

            Console.WriteLine($"Simulation started automatically at {SimulationStartTime}");
        }

        public bool IsSimulationRunning => SimulationStartTime.HasValue;
    }




}
