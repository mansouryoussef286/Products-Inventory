using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Domain.Interfaces
{
    public interface IAuditService
    {
        Task<bool> AuditProduct(ProductDTO oldValue, ProductDTO newValue, OperationTypeEnum type);
    }
}
