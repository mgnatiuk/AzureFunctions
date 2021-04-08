using System;
using System.Configuration;
using System.IO;
using AzureFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunctions
{
    public static class GenerateLicenseFunc
    {
        [FunctionName("GenerateLicenseFunc")]
        public static void Run([QueueTrigger("orders", Connection = "AzureWebJobsStorage")]Order order, 
        [Blob("licenses/{rand-guid}.txt")] TextWriter outputBlob,
        ILogger log)
        {
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
