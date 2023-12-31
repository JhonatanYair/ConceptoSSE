﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Queues.AbstracionLayer;
using Queues.AbstracionLayer.Enums;

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
                Exchange = ExchangeTypes.ReportService,
                Data = dataJson
            };

            _queueService.PublishMessage(Queues.AbstracionLayer.Enums.ExchangeTypes.ReportService,messageSave);   
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
                Exchange = ExchangeTypes.ReportService,
                To = $"{idUser}",
                Data = dataJson
            };

            _queueService.PublishMessage(Queues.AbstracionLayer.Enums.ExchangeTypes.ReportService, messageSave);
            return Ok("Publicado");
        }

    }
}
