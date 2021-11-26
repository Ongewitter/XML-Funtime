using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace ClassLibrary {
    public class SQLClient {
        private const string CONNECTIONSTRING_DEFAULT = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated security=SSPI;database=master";
        private string connectionString = CONNECTIONSTRING_DEFAULT;

        public SQLClient() {
            try {
                connectionString = GetConnectionStringFromXML();
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("Using default connectionString");
                connectionString = CONNECTIONSTRING_DEFAULT; // Reset value in case it got nulled
            }
            Console.WriteLine($"Using connectionString: {connectionString}");
        }
        public void ExecuteNonQuery(string query) {
            using (var connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                } catch (Exception ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                } finally {
                    if (connection.State == ConnectionState.Open) {
                        connection.Close();
                    }
                }
            }
        }

        public List<Hashtable> GetOrdersFromDB(string query) {
            List<Hashtable> orders = new();
            using (var connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) {
                        while (reader.Read()) {
                            Hashtable order = new() {
                                { "id", reader["id"] },
                                { "status", reader["status"] },
                                { "created_at", reader["created_at"] },
                                { "filename", reader["filename"] }
                            };
                            orders.Add(order);
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                } finally {
                    if (connection.State == ConnectionState.Open) {
                        connection.Close();
                    }
                }
            }
            return orders;
        }
        private string GetConnectionStringFromXML() {
            string connString = "";
            string configPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/config.xml";
            var reader = XmlReader.Create(configPath);
            using (reader) {
                reader.ReadToFollowing("settings");
                reader.ReadToFollowing("connectionString");
                connString = reader.ReadElementContentAsString();
            }
          
            return connString;
        }
    }
}
