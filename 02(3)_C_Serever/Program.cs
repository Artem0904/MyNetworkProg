using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using _02_1__SharedData;

namespace _02_3__C_Serever
{
    internal class Program
    {
        static int port = 8080;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            TcpListener listener = new TcpListener(ipPoint);

            listener.Start(10);
            List<Car> Cars = new List<Car>() { 
                new Car("BK1111YA", "100 020 km", "BMW", true, false, false, true),
                new Car("BK2222AA", "250 030 km", "Audi", false, false, false, false),
                new Car("BK3333HT", "115 010 km", "Mercedess", true, true, true, true),
                new Car("BK4444PC", "213 100 km", "Renaut", false, false, false, false),
                new Car("BK5555MK", "97 523 km", "Opel", false, true, true, false)
            }; 
            while (true)
            {
                Console.WriteLine("Server started! Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient(); // wait until connection

                try
                {
                    while (client.Connected)
                    {
                        NetworkStream ns = client.GetStream();

                        // отримуємо переданий об'єкт та десеріалізуємо його

                        //string VINNum = JsonSerializer.Deserialize(ns, typeof(string));
                        StreamReader sr = new StreamReader(ns);
                        string VINNum = sr.ReadLine();


                        Console.WriteLine($"Request Vin: {VINNum} from {client.Client.RemoteEndPoint}");

                        int numInList = -1;
                        for (int i = 0; i < Cars.Count; i++)
                        {
                            if (Cars[i].number == VINNum)
                            {
                                numInList = i;
                                break;
                            }
                        }

                        string response;
                        if (numInList != -1)
                        {
                            response = @$"
Result :

VIN : {Cars[numInList].number}
Model : {Cars[numInList].model}
Run : {Cars[numInList].run}
Is painted : {Cars[numInList].painted}
Is beaten : {Cars[numInList].beaten}
Is flooded : {Cars[numInList].sank}
Is electro : {Cars[numInList].electro} 
";

                        }
                        else
                            response = "VIN of car not found!";
 
                        Console.WriteLine(response);

                        StreamWriter sw = new StreamWriter(ns); // розмір буфера за замовчуванням: 1KB
                        sw.WriteLine(response);
                        sw.Flush(); // clear buffer
                    }

                    // закриваємо сокет
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                listener.Stop(); 
            }
        }
    }
}