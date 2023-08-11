using MassTransit;

namespace MassTransit.Consumer
{
    public class Program
    {
       public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("order-created-event", e =>
                {
                   // e.Consumer<OrderCreatedConsumer>();
                    e.AddConsumer<OrderCreatedConsumer>();

                });

                cfg.ReceiveEndpoint("order-service", e =>
                {
                    e.ConfigureConsumer<OrderCreatedConsumer>();
                });

            });
            startMyTask();
        }

        public static async void startMyTask()
        {
            await MassTransit.buscont .StartAsync(new CancellationToken());

            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }

}