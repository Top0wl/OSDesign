using System.Net;
using System.Net.Sockets;
using System.Text;

Socket clientSocket = null;

try
{
    // Устанавливаем IP-адрес и порт сервера, к которому будем подключаться
    int port = 12345;
    string serverIp = "127.0.0.1";

    // Создаем сокет и подключаемся к серверу
    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIp), port));

    while (true)
    {
        // Читаем ввод пользователя
        string message = Console.ReadLine();

        // Отправляем сообщение на сервер
        byte[] data = Encoding.Unicode.GetBytes(message);
        clientSocket.Send(data);

        // Получаем ответ от сервера
        data = new byte[256];
        int bytesRead = clientSocket.Receive(data);
        string response = Encoding.Unicode.GetString(data, 0, bytesRead);
        Console.WriteLine($"Ответ от сервера: {response}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
finally
{
    // Закрываем сокет
    clientSocket?.Close();
}