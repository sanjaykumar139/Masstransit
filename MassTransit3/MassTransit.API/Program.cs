using MassTransit.Producer.Settings;

namespace MassTransit.API
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
            var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
            builder.Services.AddMassTransit(x => {
                x.UsingRabbitMq((cntxt, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Uri, "/", c =>
                    {
                        c.Username(rabbitMqSettings.UserName);
                        c.Password(rabbitMqSettings.Password);
                    });
                });
            });
            //builder.Services.AddMassTransitHostedService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}