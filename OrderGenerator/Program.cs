using System.Timers;
using ClassLibrary;

namespace OrderGenerator {
    public class Program {
        private static System.Timers.Timer? timer;
        private static Boolean status = false;

        static void Main() {
            timer = CreateTimer();

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            timer.Stop();
            timer.Dispose();

            Console.WriteLine("Terminating the application...");
        }

        private static System.Timers.Timer CreateTimer() {
            timer = new System.Timers.Timer(1000);

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Start();

            return timer;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e) {
            CreateAndWriteOrder();
        }

        private static void CreateAndWriteOrder() {
            Order order = new(); //XML serialisation needs parameterless constructor
            ToggleStatus();
            order.SetStatus(status);

            XMLOrderSerializer.Write(order);
        }

        private static void ToggleStatus() {
            status = !status;
        }
    }
}