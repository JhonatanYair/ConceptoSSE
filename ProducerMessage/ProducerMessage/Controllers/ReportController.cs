using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Queues.AbstracionLayer;

namespace ProducerMessage.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        IQueueService _queueService;

        public ReportController(IQueueService queueService) 
        {
            _queueService = queueService;
        }  
        
        [HttpPost("General")]
        public IActionResult Post(/*[FromBody] */)
        {

            var dataJson = new JObject
            {
                { "report_id", $"{Guid.NewGuid()}" }
            };

            PayloadDTO messageSave = new PayloadDTO()
            {
                From = "ProduccerMessage",
                tracking = Guid.NewGuid(),
                EventType = EventTypes.report_new,
                Data = dataJson
            };

            _queueService.PublishMessage(Queues.AbstracionLayer.Enums.RabbitExchange.ReportService,messageSave);   
            return Ok("Publicado");
        }

        [HttpPost("User")]
        public IActionResult PostUser([FromBody] string idUser)
        {

            var dataJson = new JObject
            {
                { "report_id", $"{Guid.NewGuid()}" }
            };

            PayloadDTO messageSave = new PayloadDTO()
            {
                From = "ProduccerMessage",
                tracking = Guid.NewGuid(),
                EventType = EventTypes.report_new,
                To = $"{idUser}",
                Data = dataJson
            };

            _queueService.PublishMessage(Queues.AbstracionLayer.Enums.RabbitExchange.ReportService, messageSave);
            return Ok("Publicado");
        }

    }
}
