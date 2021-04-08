 using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureFunctions
{
    public static class SendEmailFunc
    {
        [FunctionName("SendEmailFunc")]
        public static void Run([BlobTrigger("licenses/{name}", Connection = "AzureWebJobsStorage")]string licenseFileContent, 
        //[SendGrid(ApiKey ="SendGridApiKey")] out SendGridMessage message,
        string name, 
        ILogger log)
        {
          string OrdererEmail = Regex.Match(licenseFileContent, @"^Orderer email\:\ (.+)$", RegexOptions.Multiline)
            .Groups[1].Value.Replace(".\\r","").Trim();

            //message = new SendGridMessage();
            //message.From = new EmailAddress(Environment.GetEnvironmentVariable("SenderEmail"));
            //message.AddTo(OrdererEmail);
            //message.Subject = "Confirmation of order & license file";
            //message.HtmlContent = "In attachments you can download your license file. Our team thanks You for buying our products.";

            //var textFromBytes = System.Text.Encoding.UTF8.GetBytes(licenseFileContent);
            //var bas64 = Convert.ToBase64String(textFromBytes);
            //message.AddAttachment(name,bas64,"text/plain");
 
            //log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {textFromBytes.Length} Bytes");
        }
    }
}
