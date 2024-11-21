namespace Version2.Models
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using HeaterSim.Models;
    using Microsoft.Extensions.Hosting;

    public class EnvironmentUpdater : BackgroundService
    {
        private readonly EnvironmentState _state;
        private readonly TemperatureCalculator _calculator;

        public EnvironmentUpdater(EnvironmentState state, TemperatureCalculator calculator)
        {
            _state = state;
            _calculator = calculator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                UpdateEnvironmentState();
                await Task.Delay(1000, stoppingToken); // Wait 1 second before next update
            }
        }

        private void UpdateEnvironmentState()
        {
            var elapsedSeconds = (DateTime.Now - _state.LastUpdated).TotalSeconds;

            foreach (var sensor in _state.Sensors)
            {
                double netTemperatureChange = _calculator.CalculateNetTemperatureChange(
                    _state.Heaters,
                    _state.Fans,
                    elapsedSeconds / 60); // Convert seconds to minutes

                sensor.CurrentTemperature += netTemperatureChange;
            }

            _state.LastUpdated = DateTime.Now;
        }
    }

}
