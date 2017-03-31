using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkCXConsoleTest
{
    public class Message
    {
        public Guid DeviceId { get; set; }
        public string UrlParams { get; set; }
        public string UserAgent { get; set; }
        public string HeaderString { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string IPAddress { get; set; }
        public int IPCarrierId { get; set; }
        public int DeviceCarrierId { get; set; }
    }
}
