
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.VisualBasic;

class Program {

    public class CatFact
    {
        public string fact { get; set; }
        public int length { get; set; }
    }

    private const string LINK = "https://catfact.ninja/fact";

    static void Main(string[] args) {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(LINK);

        request.KeepAlive = false;

        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        Stream responseStream = response.GetResponseStream();
        StreamReader responseReader = new StreamReader(responseStream);

        char[] readed_buffers = new char[4096];
        int count = responseReader.Read(readed_buffers, 0, 4096);
        string outputData = new string(readed_buffers, 0, count);
        
        responseStream.Close();
        responseReader.Close();

        Console.WriteLine($"Json Value: {outputData}");
        
        CatFact? item = JsonSerializer.Deserialize<CatFact>(outputData);
        Console.WriteLine($"fact: {item?.fact}");
        Console.WriteLine($"length: {item?.length}");
    }
}
