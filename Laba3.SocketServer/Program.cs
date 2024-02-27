using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Server
{
    private Socket serverSocket;

    public void Start(string ipAddress, int port)
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse(ipAddress);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(localAddr, port));
            serverSocket.Listen(10);
            Console.WriteLine($"Сервер запущен... ");

            AcceptClients();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private async void AcceptClients()
    {
        try
        {
            while (true)
            {
                Socket clientSocket = await serverSocket.AcceptAsync();
                Console.WriteLine("=========================================================");
                Console.WriteLine("Клиент подключен!");

                Task.Run(() => HandleClient(clientSocket));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при принятии клиента: {ex.Message}");
        }
    }

    private void HandleClient(Socket clientSocket)
    {
        try
        {
            byte[] data = new byte[256];
            while (true)
            {
                int bytesRead = clientSocket.Receive(data);
                string message = Encoding.UTF8.GetString(data, 0, bytesRead);
                Console.WriteLine($"Получено от клиента: {message}");

                string response = "Сообщение получено на сервере.";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                clientSocket.Send(responseData);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке клиента: {ex.Message}");
        }
        finally
        {
            clientSocket.Close();
        }
    }

    public void Stop()
    {
        try
        {
            serverSocket?.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при остановке сервера: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Server server = new Server();

        Console.WriteLine("Введите порт: ");
        int port = int.Parse(Console.ReadLine());
        server.Start("10.80.15.198", port);

        while (true) { }

        server.Stop();
    }
}