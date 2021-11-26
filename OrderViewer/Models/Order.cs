using ClassLibrary;
using System.Collections;

namespace OrderViewer.Models {
    public class Order {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Filename { get; set; }
        public static List<Order> GetOrders() {
            SQLClient sqlClient = new SQLClient();
            List<Order> orders = new();
            List<Hashtable> db_orders = new();
            string query = "USE mydb; SELECT * FROM dbo.Orders";
            db_orders = sqlClient.GetOrdersFromDB(query);
            foreach (Hashtable db_order in db_orders) {
                Order order = new();
                order.Id = (int) db_order["id"];
                order.CreatedAt = (DateTime) db_order["created_at"];
                order.Filename = db_order["filename"].ToString();
                orders.Add(order);
            }
            return orders;
        }
    }
}
