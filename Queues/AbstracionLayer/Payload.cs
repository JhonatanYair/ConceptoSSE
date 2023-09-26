using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace Queues.AbstracionLayer
{
    public class Payload
    {
        public int Event_id { get; set; }
        public Guid tracking { get; set; }
        public string From { get; set; }
        public string? To { get; set; }
        public string EventType { get; set; }
        public DateTime? DateCreate { get; set; }
        public JObject Data { get; set; }
    }

    public class PayloadDTO
    {
        public Guid tracking { get; set; }
        public string From { get; set; }
        public string? To { get; set; }
        public EventTypes EventType { get; set; }
        public JObject Data { get; set; }
    }


    public enum EventTypes
    {
        report_new,
        company_new
    }

}
