using System;
using System.Collections.Generic;

namespace ProductsInventory.Data.Models
{
    public partial class AuditLogs
    {
        public string TableName { get; set; }
        public int Id { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string OperationType { get; set; }
        public string LogId { get; set; }
    }
}
