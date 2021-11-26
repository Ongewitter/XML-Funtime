using ClassLibrary;

namespace CreateDB {
    public class Program {
        static void Main() {
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.WriteLine("WARNING: This will DROP any previous Database called 'mydb'");
            Console.ReadLine();
            SQLClient sqlClient = new();

            Console.WriteLine("Dropping previous Database");
            sqlClient.ExecuteNonQuery("DROP DATABASE mydb");
            Console.WriteLine("Creating Database");
            sqlClient.ExecuteNonQuery("CREATE DATABASE mydb");

            // Drop pre-existing tables, then create new one
            Console.WriteLine("Creating Orders table in Database mydb");
            sqlClient.ExecuteNonQuery("USE mydb; IF EXISTS ( SELECT * FROM sys.tables WHERE name LIKE 'Orders%')"
                        + "DROP TABLE dbo.Orders;"
                        + "CREATE TABLE dbo.Orders ("
                            + "id int IDENTITY(1,1),"
                            + "status bit,"
                            + "xml varchar(max),"
                            + "created_at datetime DEFAULT CURRENT_TIMESTAMP NOT NULL,"
                            + "filename varchar(max))");

            Console.WriteLine("Terminating the application...");
        }
    }
}