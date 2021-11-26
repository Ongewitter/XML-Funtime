using System.Collections;
using System.Xml;

namespace ClassLibrary {
    public class XMLOrderSerializer {
        private static readonly System.Xml.Serialization.XmlSerializer serializer =
                new(typeof(Order));
        private const string XML_ORDER_PATH = @"C:\Orders";

        public static void Write(Order order) {

            if (!Directory.Exists(XML_ORDER_PATH)) { Directory.CreateDirectory(XML_ORDER_PATH); }

            string path = XML_ORDER_PATH + "\\" +  Guid.NewGuid().ToString() + ".xml";
            Console.WriteLine("Writing to " + path);
            System.IO.FileStream file = System.IO.File.Create(path);

            serializer.Serialize(file, order);
            file.Close();
        }

        public static Hashtable[] ReadAll() {
            string[] filenames = Directory.GetFiles(XML_ORDER_PATH, "*.xml");
            Hashtable[] ordersInfo = new Hashtable[filenames.Length];
            int i = 0;

            foreach (string filename in filenames) {
                FileStream filestream = new(filename, FileMode.Open);
                XmlReader reader = XmlReader.Create(filestream);

                Order order = (Order) serializer.Deserialize(reader);
                filestream.Close();

                string xmlText = File.ReadAllText(filename);

                Hashtable orderInfo = new() {
                    { "status", order.status },
                    { "xml", xmlText },
                    { "filename", filename }
                };


                // Write out the properties of the object.
                Console.WriteLine("Status: " + orderInfo["status"]
                    + "\nXML: " + orderInfo["xml"]
                    + "\nFilename: " + orderInfo["filename"]);

                ordersInfo[i] = orderInfo;
                i++;
            }

            return ordersInfo;
        }
    }
}
