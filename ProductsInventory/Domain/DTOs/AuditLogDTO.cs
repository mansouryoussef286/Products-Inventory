using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Domain.DTOs
{
    public class AuditLogDTO
    {
        public string TableName { get; set; }
        public int Id { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string OperationType { get; set; }
    }
}
