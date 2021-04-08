using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctions.Models
{
    public class Order
    {
        public string RowKey { get; set; }

        public string PartitionKey { get; set; }

        public long Id { get; set; }

        public string OrdererEmail { get; set; }

        public string ProductName { get; set; }
    }
}
