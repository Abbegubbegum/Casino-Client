using Casino.Networking;

string url = "ws://localhost:8080/Blackjack";
Console.WriteLine("What url? blank for regular");
string input = Console.ReadLine();
Client client;

if (input == "")
{
    client = new Client(url);
}
else
{
    client = new Client(input);
}

while (true)
{
    // while (client.waitingForMessage) ;

    input = Console.ReadLine();

    // client.waitingForMessage = true;

    client.Send(input);
}



