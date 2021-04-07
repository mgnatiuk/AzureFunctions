 using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctions
{
    public static class SendEmailFunc
    {
        [FunctionName("SendEmailFunc")]
        public static void Run([BlobTrigger("licenses/{name}", Connection = "AzureWebJobsStorage")]string licenseFileContent, 
        string name, 
        ILogger log)
        {
          string OrdererEmail = Regex.Match(licenseFileContent, @"^Orderer email\:\ (.+)$", RegexOptions.Multiline)
            .Groups[1].Value;

            //log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
