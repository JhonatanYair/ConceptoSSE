using Queues.AbstracionLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues.AbstracionLayer
{
    public interface IQueueService
    {
        //void InitializeConnection(string queueName);
        //void DeclareQueue(RabbitExchange exchange, RabbitQueue queue);
        //void PublishMessage(RabbitExchange exchange, RabbitQueue queue, string message);
        //string ConsumeMessage(RabbitExchange exchange, RabbitQueue queue);
        //int GetMessageCount(string queueName);

        void DeclareQueue(RabbitExchange exchange);
        void PublishMessage(RabbitExchange exchange, PayloadDTO message);
        string ConsumeMessage(RabbitExchange exchange);
        int GetMessageCount(string queueName);

    }
}
