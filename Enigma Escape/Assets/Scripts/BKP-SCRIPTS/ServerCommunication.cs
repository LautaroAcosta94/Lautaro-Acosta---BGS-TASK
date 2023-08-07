using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class ServerCommunication : MonoBehaviour
{
    private TcpClient tcpClient;
    private NetworkStream clientStream;
    private byte[] messageBuffer;
    public string serverIP = "127.0.0.1"; // Reemplaza esto con la dirección IP del servidor local
    public int serverPort = 8888; // Reemplaza esto con el puerto utilizado por el servidor

    private void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        try
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(serverIP, serverPort);
            clientStream = tcpClient.GetStream();
            messageBuffer = new byte[4096];
            Debug.Log("Connected to server.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect to server: " + e.Message);
        }
    }

    public void SendMessageToServer(string message)
    {
        try
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            clientStream.Write(messageBytes, 0, messageBytes.Length);
            clientStream.Flush();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to send message to server: " + e.Message);
        }
    }

    private void Update()
    {
        // Aquí puedes implementar la lógica para recibir mensajes del servidor
        // Utiliza clientStream.Read para leer los mensajes del servidor
    }

    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            tcpClient.Close();
        }
    }
}

