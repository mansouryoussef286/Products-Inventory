using ProductsInventory.Data.Models;
using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Data
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ProductsInventoryContext _context;

        public AuditLogRepository(ProductsInventoryContext context)
        {
            _context = context;
        }

        private AuditLogs MapDTOToProductAudit(AuditLogDTO productAuditLogDTO)
        {
            return new AuditLogs
            {
                LogId = Guid.NewGuid().ToString(),
                Id = productAuditLogDTO.Id,
                OperationType = productAuditLogDTO.OperationType,
                TableName = productAuditLogDTO.TableName,
                OldValue = productAuditLogDTO.OldValue,
                NewValue = productAuditLogDTO.NewValue,
                ChangeDate = productAuditLogDTO.ChangeDate,
            };
        }

        public async Task<bool> AddLog(AuditLogDTO newProductAuditLog)
        {
            var productAuditLog = MapDTOToProductAudit(newProductAuditLog);
            await _context.AuditLogs.AddAsync(productAuditLog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
