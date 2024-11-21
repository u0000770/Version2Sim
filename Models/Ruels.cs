using HeaterSim.Controllers;
using HeaterSim.Models;
using Version2.Controllers;

namespace Version2.Models
{
    public class TemperatureCalculator
    {
        private const double HeatFactor = 0.1;  // Heater effect per level per minute
        private const double CoolingFactor = -0.5; // Cooling effect per fan per minute

        public double CalculateHeaterEffect(IEnumerable<HeatingControl> heaters, double elapsedMinutes)
        {
            double effect = heaters.Sum(h => h.Level * elapsedMinutes * HeatFactor);
            Console.WriteLine($"Heater Effect: {effect} (Elapsed Minutes: {elapsedMinutes})");
            return effect;
        }

        public double CalculateFanEffect(IEnumerable<FanControl> fans, double elapsedMinutes)
        {
            return fans.Count(f => f.IsOn) * elapsedMinutes * CoolingFactor;
        }

        public double CalculateNetTemperatureChange(IEnumerable<HeatingControl> heaters,IEnumerable<FanControl> fans,double elapsedMinutes)
        {
            double heaterEffect = CalculateHeaterEffect(heaters, elapsedMinutes);
            double fanEffect = CalculateFanEffect(fans, elapsedMinutes);

            return heaterEffect + fanEffect;
        }
    }

}
