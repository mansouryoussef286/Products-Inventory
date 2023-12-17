using Microsoft.EntityFrameworkCore;
using ProductsInventory.Data.Models;
using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsInventoryContext _context;

        public ProductsRepository(ProductsInventoryContext context)
        {
            _context = context;
        }

        private ProductDTO MapProductToDTO(Products product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private Products MapDTOToProduct(ProductDTO product)
        {
            return new Products
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public List<ProductDTO> GetProducts()
        {
            var products = _context.Products.ToList();
            return products.Select(MapProductToDTO).ToList();
        }

        public ProductDTO GetProductById(int productId)
        {
            var product = _context.Products.Find(productId);
            return product != null ? MapProductToDTO(product) : null;
        }

        public ProductDTO CreateProduct(ProductDTO newproduct)
        {
            var product = MapDTOToProduct(newproduct);
            _context.Products.Add(product);
            _context.SaveChanges();
            return MapProductToDTO(product);
        }


        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p=>p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.UpdatedAt = product.UpdatedAt;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public void DeleteProduct(int productId)
        {
            var productToDelete = _context.Products.Find(productId);

            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
        }
    }
}
