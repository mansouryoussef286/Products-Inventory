using ProductsInventory.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Domain.IRepositories
{
    public interface IAuditLogRepository
    {
        Task<bool> AddLog(AuditLogDTO product);
    }
}
