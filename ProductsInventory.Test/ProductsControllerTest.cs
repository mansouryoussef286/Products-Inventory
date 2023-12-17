using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductsInventory.Controllers;
using ProductsInventory.Data;
using ProductsInventory.Data.Models;
using ProductsInventory.Domain;
using ProductsInventory.Domain.DTOs;
using ProductsInventory.Domain.Interfaces;
using ProductsInventory.Domain.IRepositories;
using ProductsInventory.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProductsInventory.Test
{
    public class ProductsControllerTest
    {
        ProductsController _controller;
        ProductsService _service;
        IProductsRepository _productsRepository;
        IAuditService _auditService;
        AuditLogRepository _auditLogRepository;
        ProductsInventoryContext _context;


        public ProductsControllerTest()
        {
            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            var options = new DbContextOptionsBuilder<ProductsInventoryContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"))
                .Options;

            _context = new ProductsInventoryContext(options);
            _productsRepository = new ProductsRepository(_context);
            _auditLogRepository = new AuditLogRepository(_context);
            _auditService = new AuditDbService(_auditLogRepository);
            _service = new ProductsService(_productsRepository, _auditService);
            _controller = new ProductsController(_service);

        }

        [Fact]
        public void GetProducts_ReturnsOkResult()
        {
            var result = _controller.GetProducts();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.NotEmpty(products);
        }

        [Fact]
        public void GetProductById_ReturnsNotFoundResult_ForInvalidId()
        {
            // Arrange
            int invalidId = 999;

            // Act
            var result = _controller.GetProductById(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetProductById_ReturnsOkResult()
        {
            // Arrange
            int Id = 12345678;

            // Act
            var result = _controller.GetProductById(Id);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result); 
            var actualProduct = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal("16-Dec-23 4:29:41 PM", actualProduct.CreatedAt.ToString());
        }

        [Fact]
        public void CreateProduct_ReturnsOkResult_ForValidProduct()
        {
            // Arrange
            var validProduct = new ProductDTO {
                Name = "test Product",
                Price = 1200,
                Quantity = 300,
                ProductId = 11111111
            };

            // Act
            var result = _controller.CreateProduct(validProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdProduct = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(validProduct.Name, createdProduct.Name);
        }

        //[Fact]
        //public void CreateProduct_ReturnsBadRequest_ForinValidProduct()
        //{
        //    // Arrange
        //    var validProduct = new ProductDTO
        //    {
        //        Name = "test Product",
        //        Price = -1200,
        //        Quantity = 300,
        //        ProductId = 11111112
        //    };

        //    // Act
        //    var result = _controller.CreateProduct(validProduct);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        [Fact]
        public async void UpdateProduct_ReturnsOkResult_ForValidProduct()
        {
            // Arrange
            int validId = 11111111;
            var validProduct = new ProductDTO
            {
                Name = "updated from test",
                Price = 1200,
                Quantity = 300,
                ProductId = 11111111
            };

            // Act
            var result = await _controller.UpdateProduct(validId, validProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedProduct = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(validProduct.Name, updatedProduct.Name);
        }

        [Fact]
        public void DeleteProduct_ReturnsNoContentResult_ForValidId()
        {
            // Arrange
            int validId = 11111111;

            // Act
            var result = _controller.DeleteProduct(validId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
