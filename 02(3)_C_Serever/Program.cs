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

            
            List<Car> Cars = new List<Car>() { 
                new Car("BK1111YA", "100 020 km", "BMW", true, false, false, true),
                new Car("BK2222AA", "250 030 km", "Audi", false, false, false, false),
                new Car("BK3333HT", "115 010 km", "Mercedess", true, true, true, true),
                new Car("BK4444PC", "213 100 km", "Renaut", false, false, false, false),
                new Car("BK5555MK", "97 523 km", "Opel", false, true, true, false)
            };
            
            listener.Start(10);

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
                            Car CarByVIN = Cars[numInList];
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
                            
                            JsonSerializer.Serialize(ns, CarByVIN);

                            ns.Flush();
                            ns.Close();
                        }
                        else
                            response = "VIN of car not found!";
 
                        Console.WriteLine(response);

                        
                    }

                  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
                listener.Stop(); 
        }
    }
}