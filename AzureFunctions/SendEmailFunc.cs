 using System;
using System.IO;
using System.Text.RegularExpressions;
using AzureFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureFunctions
{
    public static class SendEmailFunc
    {
        [FunctionName("SendEmailFunc")]
        public static void Run([BlobTrigger("licenses/{Id}", Connection = "AzureWebJobsStorage")]string licenseFileContent,
        [SendGrid(ApiKey ="SendGridApiKey")] ICollector<SendGridMessage> sender,
        [Table(nameof(Order), nameof(Order),"{Id}")] Order order,
        string Id, 
        ILogger log)
       {
            var message = new SendGridMessage();
            message.From = new EmailAddress(Environment.GetEnvironmentVariable("SenderEmail"));
            message.AddTo(order.OrdererEmail);
            message.Subject = "Confirmation of order & license file";
            message.HtmlContent = "In attachments you can download your license file. Our team thanks You for buying our products.";

            var textFromBytes = System.Text.Encoding.UTF8.GetBytes(licenseFileContent);
            var bas64 = Convert.ToBase64String(textFromBytes);
            message.AddAttachment(Id, bas64, "text/plain");

            if (!order.OrdererEmail.Equals("alex.one@gmail.com"))
                sender.Add(message);

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{Id} \n Size: {textFromBytes.Length} Bytes");
        }
    }
}
