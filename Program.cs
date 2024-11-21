
using HeaterSim.Models;
using Version2.Models;
using Version2.Models.Version2.Models;

namespace Version2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<EnvironmentState>(provider =>
            {
                var state = new EnvironmentState();

                // Initialize sensors
                for (int i = 1; i <= 3; i++)
                {
                    state.Sensors.Add(new TemperatureSensor { Id = i, CurrentTemperature = 18 });
                }

                // Initialize heaters
                for (int i = 1; i <= 3; i++)
                {
                    state.Heaters.Add(new HeatingControl { Id = i, Level = 0 });
                }

                // Initialize fans
                for (int i = 1; i <= 3; i++)
                {
                    state.Fans.Add(new FanControl { Id = i, IsOn = false });
                }

                return state;
            });
            builder.Services.AddSingleton<ApiKeyManager>();
            builder.Services.AddSingleton<ClientStateManager>();
            builder.Services.AddSingleton<TemperatureCalculator>();
            builder.Services.AddHostedService<EnvironmentUpdater>();
            builder.Services.AddHostedService<StateCleanupService>();
            builder.Services.AddHostedService<SimulationMonitorService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
