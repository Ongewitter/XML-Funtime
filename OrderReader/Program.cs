using System.Collections;
using System.Timers;
using ClassLibrary;  


namespace OrderReader {
    public class Program {
        private static System.Timers.Timer timer;
        private static SQLClient sqlClient = new();

        static void Main() {
            timer = CreateTimer();

            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            timer.Stop();
            timer.Dispose();
            Console.WriteLine("Terminating the application...");
        }

        private static System.Timers.Timer CreateTimer() {
            timer = new System.Timers.Timer(10_000);

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Start();

            return timer;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e) {
            Console.WriteLine("Reading orders and writing to DB");
            Hashtable[] ordersInfo = ReadOrdersInfo();
            WriteToDB(ordersInfo);
        }

        private static Hashtable[] ReadOrdersInfo() {
            Hashtable[] ordersInfo = Array.Empty<Hashtable>();
            try {
                ordersInfo = XMLOrderSerializer.ReadAll();
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            return ordersInfo;
        }

        private static void WriteToDB(Hashtable[] ordersInfo) {
            string query = "USE mydb; INSERT INTO dbo.Orders (status, xml, filename) VALUES ";

            foreach (Hashtable orderInfo in ordersInfo) {
                query += "(\'" + orderInfo["status"] + "\', \'" + orderInfo["xml"] + "\',\'" + orderInfo["filename"] + "\'),";
            }
            query = query.Remove(query.Length - 1, 1) + ";"; //Change last , into ;
            sqlClient.ExecuteNonQuery(query);
        }
    }
}