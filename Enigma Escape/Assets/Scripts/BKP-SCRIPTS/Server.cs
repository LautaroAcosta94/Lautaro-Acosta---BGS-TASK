using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Server 
{
    private List<ClientHandler> clients;
    private TcpListener tcpListener;
    private Thread listenThread;

    public Server()
    {
        clients = new List<ClientHandler>();
        tcpListener = new TcpListener(IPAddress.Any, 8888);
        listenThread = new Thread(new ThreadStart(ListenForClients));
        listenThread.Start();
        Console.WriteLine("Server started.");
    }

    private void ListenForClients()
    {
        tcpListener.Start();

        while (true)
        {
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            ClientHandler clientHandler = new ClientHandler(tcpClient, this);
            clients.Add(clientHandler);
            clientHandler.StartClient();
        }
    }

    public void Broadcast(string message, ClientHandler excludeClient = null)
    {
        foreach (ClientHandler client in clients)
        {
            if (client != excludeClient)
            {
                client.SendMessage(message);
            }
        }
    }

    public void RemoveClient(ClientHandler client)
    {
        clients.Remove(client);
    }
}

public class ClientHandler
{
    private TcpClient tcpClient;
    private Server server;
    private Thread clientThread;
    private NetworkStream clientStream;
    private byte[] messageBuffer;

    public ClientHandler(TcpClient client, Server server)
    {
        tcpClient = client;
        this.server = server;
        clientThread = new Thread(new ThreadStart(HandleClient));
        messageBuffer = new byte[4096];
    }

    public void StartClient()
    {
        clientStream = tcpClient.GetStream();
        clientThread.Start();
    }

    public void SendMessage(string message)
    {
        byte[] messageBytes = Encoding.ASCII.GetBytes(message);
        clientStream.Write(messageBytes, 0, messageBytes.Length);
        clientStream.Flush();
    }

    private void HandleClient()
    {
        try
        {
            while (true)
            {
                int bytesRead = clientStream.Read(messageBuffer, 0, messageBuffer.Length);
                string message = Encoding.ASCII.GetString(messageBuffer, 0, bytesRead);
                Console.WriteLine("Received message: " + message);
                server.Broadcast(message, this);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error handling client: " + e.Message);
            server.RemoveClient(this);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Server server = new Server();

        while (true)
        {
            // Server loop
        }
    }
}

