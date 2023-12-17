using ProductsInventory.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Domain.IRepositories
{
    public interface IProductsRepository
    {
        List<ProductDTO> GetProducts();

        ProductDTO GetProductById(int productId);

        Task<bool> UpdateProduct(ProductDTO product);
        ProductDTO CreateProduct(ProductDTO product);

        void DeleteProduct(int productId);
    }
}
