using System.Net;
using System.Net.Sockets;
using System.Text;

Socket serverSocket = null;
try
{
    // Устанавливаем IP-адрес и порт для прослушивания
    int port = 12345;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    // Создаем сокет и привязываем его к локальному адресу
    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    serverSocket.Bind(new IPEndPoint(localAddr, port));

    // Начинаем прослушивание клиентов
    serverSocket.Listen(10);
    Console.WriteLine("Сервер запущен...");

    // Блокируем программу, ожидая подключения клиента
    Socket clientSocket = serverSocket.Accept();
    Console.WriteLine("Клиент подключен!");

    // Буфер для хранения полученных данных
    byte[] data = new byte[256];

    // Цикл обмена данными с клиентом
    while (true)
    {
        // Читаем данные из сокета
        int bytesRead = clientSocket.Receive(data);
        string message = Encoding.Unicode.GetString(data, 0, bytesRead);
        Console.WriteLine($"Получено от клиента: {message}");

        // Отправляем ответ клиенту
        string response = "Сообщение получено на сервере.";
        byte[] responseData = Encoding.Unicode.GetBytes(response);
        clientSocket.Send(responseData);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
finally
{
    // Закрываем сокеты
    serverSocket?.Close();
}











Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    
    socket.Shutdown(SocketShutdown.Both);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    socket.Close();
}