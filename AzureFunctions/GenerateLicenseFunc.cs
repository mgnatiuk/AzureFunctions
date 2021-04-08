using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using AzureFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunctions
{
    public static class GenerateLicenseFunc
    {
        [FunctionName("GenerateLicenseFunc")]
        public static async Task Run([QueueTrigger(nameof(Order), Connection = "AzureWebJobsStorage")]Order order,
        IBinder binder,
        ILogger log)
        {
            // Bind TextWriter class with our BlobStorage
            var outputBlob = await binder.BindAsync<TextWriter>(new BlobAttribute($"licenses/{order.Id}.txt") 
                {
                    Connection = "AzureWebJobsStorage"
                });

            // IBinder binder
            // Works with all binding attributes (QueueAttribute, SendGridAttribute)
            // [Blob("licenses/{rand-guid}.txt")] TextWriter outputBlob,

            outputBlob.WriteLine("--- Order details ---");
            outputBlob.WriteLine($"Order number: {order.Id},");
            outputBlob.WriteLine($"Orderer email: {order.OrdererEmail},");
            outputBlob.WriteLine($"Product name: {order.ProductName},");
            outputBlob.WriteLine($"Purchase date: {DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")},");

            string secretKey = Environment.GetEnvironmentVariable("SecretKey");
            var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"{order.OrdererEmail}{secretKey}"));
            outputBlob.WriteLine($"Secret code: {BitConverter.ToString(hash).Replace("-","")}.");

            log.LogInformation($"C# Queue trigger function processed: {order}");
        }
    }
}
