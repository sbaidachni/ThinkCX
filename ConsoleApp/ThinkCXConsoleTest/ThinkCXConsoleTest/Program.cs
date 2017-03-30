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

        private static List<Guid> DeviceIds = new List<Guid>()
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
        };

        private static List<string> IPAddresses = new List<string>()
        {
            "1.54.23.197",
            "1.54.23.195",
            "1.54.23.191",
            "1.54.23.196"
        };
        static void Main(string[] args)
        {
            MakeRequestsAsync();

            Console.ReadKey();
        }
        private static async Task MakeRequestsAsync()
        {
            var i = 0;
            var rand = new Random();
            var semaphore = new SemaphoreSlim(initialCount: 100);
            while (true)
            {
                await semaphore.WaitAsync();
                System.Console.WriteLine(semaphore.CurrentCount + " " + i++);
                var message = CreateNewMessage(rand);
                Console.WriteLine("Sending Data for Device :" + message.DeviceId +" IPAddress: "+message.IPAddress);
                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                client.PostAsync(@"https://thinkcxhttpfunction.azurewebsites.net/api/ThinkCXHttpTriggerFunction?code=h/PXBEMaoPVal3F2jcB7M6l6aaKmxlXF4ZfOTz6wSJCCqnXyGp800Q==", content).ContinueWith(t =>
                {
                    semaphore.Release();
                });
            }
        }

        private static Message CreateNewMessage(Random i)
        {
            var message = new Message()
                {
                    DeviceId = DeviceIds.ElementAt(i.Next(DeviceIds.Count-1)),
                    UserAgent =  "mobile",
                    HeaderString = "Test Run 5",
                    TimeStamp = DateTimeOffset.UtcNow,
                    UrlParams = "Test Run 5",
                    IPAddress = IPAddresses.ElementAt(i.Next(IPAddresses.Count-1))
            };

            return message;
        }
    }
}