using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Models;

namespace AzureFunctions
{
    public static class RecivePaymentFunc
    {
        [FunctionName("RecivePaymentFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [Queue("orders")] IAsyncCollector<Order> ordersQueue)
        {
            log.LogInformation("Payment recived.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            Order order = JsonConvert.DeserializeObject<Order>(requestBody);
 
            string responseMessage = $"Order with id:{order.Id}, recived for user with email {order.OrdererEmail}. Product name: {order.ProductName}.";

            // [Queue("orders")] IAsyncCollector<Order>
            await ordersQueue.AddAsync(order);

            return new OkObjectResult(responseMessage);
        }
    }
}
