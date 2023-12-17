using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.Enums;
using ProductsInventory.Domain.Interfaces;

namespace ProductsInventory.Domain.Services
{
    public class RealTimeService: IRealTimeService
    {
        private readonly FirebaseClient _firebaseClient;

        public RealTimeService(string firebaseUrl, string firebaseSecret)
        {
            _firebaseClient = new FirebaseClient(firebaseUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(firebaseSecret),
            });
        }

        public async void PushAuditLog(ProductDTO oldValue, ProductDTO newValue, OperationTypeEnum type)
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
            await _firebaseClient.Child("AuditLog").PostAsync(auditObject);
        }
    }
}
