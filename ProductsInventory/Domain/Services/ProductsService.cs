using ProductsInventory.Data.Models;
using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.Enums;
using ProductsInventory.Domain.Interfaces;
using ProductsInventory.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Domain
{
    public class ProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IAuditService _auditService;

        public ProductsService(IProductsRepository productRepository,IAuditService auditService)
        {
            _productsRepository = productRepository;
            _auditService = auditService;
        }

        public List<ProductDTO> GetProducts()
        {
            return _productsRepository.GetProducts();
        }

        public ProductDTO GetProductById(int productId)
        {
            return _productsRepository.GetProductById(productId);
        }

        public void CreateProduct(ProductDTO product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _productsRepository.CreateProduct(product);
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            product.UpdatedAt = DateTime.Now;
            var isUpdated = await _productsRepository.UpdateProduct(product);
            if (isUpdated)
            {
                var oldProduct = GetProductById(product.ProductId);
                await _auditService.AuditProduct(oldProduct, product, OperationTypeEnum.Update);
                return true;
            }
            return false;
        }

        public void DeleteProduct(int productId)
        {
            _productsRepository.DeleteProduct(productId);
        }
    }
}
