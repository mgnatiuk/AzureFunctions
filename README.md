# AzureFunctions
Azure functions - generate licenses flow.


# HTTP
# Queue
# Blob

# AppSettings example
  {
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "SecretKey": "this is my secret key for license.",
    "SendGridApiKey": "here api key for send grid",
    "SenderEmail": "test@gmail.com",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
