using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkCXConsoleTest
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            MakeRequestsAsync();

            Console.ReadKey();
        }
        private static async Task MakeRequestsAsync()
        {
            var i = 0;
            var rand = new Random();
            var semaphore = new SemaphoreSlim(initialCount: 5);
            while (true)
            {
                await semaphore.WaitAsync();
                System.Console.WriteLine(semaphore.CurrentCount + " " + i++);
                var message = CreateNewMessage(rand.Next()); 
                Console.WriteLine("Sending Data for Device :" + message.DeviceId);
                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                client.PostAsync(@"https://thinkcxhttpfunction.azurewebsites.net/api/ThinkCXHttpTriggerFunction?code=h/PXBEMaoPVal3F2jcB7M6l6aaKmxlXF4ZfOTz6wSJCCqnXyGp800Q==", content).ContinueWith(t =>
                {
                    semaphore.Release();
                });
            }
        }

        private Message CreateNewMessage(int i)
        {
            var message = new Message()
            {
                DeviceId = i.ToString(),
                UserAgent = "mobile",
                HeaderString = "",
                TimeStamp = DateTimeOffset.UtcNow,
                UrlParams = null
            };

            return message;
        }
    }

   

}
/*
{
class Program
{

    const string RequestUri = "https://thinkcxhttpfunction.azurewebsites.net/api/ThinkCXHttpTriggerFunction?code=h/PXBEMaoPVal3F2jcB7M6l6aaKmxlXF4ZfOTz6wSJCCqnXyGp800Q==";

    static void Main(string[] args)
    {
        Console.WriteLine("Start sending data");

        AsyncPump.Run(async delegate
        {
            await Run();
        });
    }

    static async Task Run()
    {
        try
        {
            Console.WriteLine("Sending Data...");
            await SendRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ooops, something broke: {0}", ex);
            Console.WriteLine();
        }
    }

    static async Task SendRequest()
    {
        var client = new HttpClient();
        var message = new Message()
        {
            DeviceId = "111222333",
            UserAgent = "mobile",
            HeaderString = "",
            TimeStamp = DateTimeOffset.UtcNow,
            UrlParams = null
        };
        Console.WriteLine("Sending Data for Device :"+message.DeviceId);
        var stringContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
        await client.PostAsync(RequestUri, stringContent);

    }

}
}*/
