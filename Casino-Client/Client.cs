using System.Text.Json;
using WebSocketSharp;

namespace Casino.Networking
{
    public class Client
    {
        public WebSocket ws;

        public string ClientId { get; set; } = "";

        public bool waitingForMessage = false;

        public Client(string url)
        {
            // this.p = p;

            ws = new WebSocket(url);

            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("Client Connected to " + url);
            };

            // ws.OnMessage += p.OnMessage;

            ws.OnMessage += (sender, e) =>
            {
                // if (waitingForMessage) waitingForMessage = false;

                Message msg = JsonSerializer.Deserialize<Message>(e.Data);

                if (ClientId == "" && msg.type == "ID-ACK")
                {
                    ClientId = msg.data;
                }
                else
                {
                    Console.WriteLine(msg.data);
                }
            };

            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("Client disconnected from " + url);
            };

            ws.Connect();
        }

        public void Send(string msg)
        {
            Message m = new Message()
            {
                sender = ClientId,
                data = msg
            };

            ws.Send(JsonSerializer.Serialize(m));
        }

        public void Disconnect()
        {
            ws.Close();
        }
    }
}