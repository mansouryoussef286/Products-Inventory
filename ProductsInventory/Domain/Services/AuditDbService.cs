using Newtonsoft.Json;
using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.Enums;
using ProductsInventory.Domain.Interfaces;
using ProductsInventory.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Domain.Services
{
    public class AuditDbService : IAuditService
    {
        private readonly IAuditLogRepository _productsLogRepository;

        public AuditDbService(IAuditLogRepository productLogRepository)
        {
            _productsLogRepository = productLogRepository;
        }

        public async Task<bool> AuditProduct(ProductDTO oldValue, ProductDTO newValue, OperationTypeEnum type)
        {
            var auditObject = new AuditLogDTO
            {
                TableName = "products",
                OldValue = JsonConvert.SerializeObject(oldValue),
                NewValue = JsonConvert.SerializeObject(newValue),
                ChangeDate = DateTime.Now,
                Id = oldValue.ProductId,
                OperationType = type.ToString(),
            };
             return await _productsLogRepository.AddLog(auditObject);
        }
    }
}
