using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Product;
using LapTopStore_Computer.Interface;
using LapTopStore_Computer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.MyInterface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductById(int? id);
        Task<List<ProductWithCategory>> AdminIndex(int CatID);
        Task<List<Category>> GetListCategory();
        Task<int> CreateProduct(Product product);
        Task<GetListAttribute> GetListAttribute();
        Task<int> CreateProductAtrtibute(CreateProductAttribute request);
        Task<ProductDetails> GetProductDetail(int? id);
        Task<Product> EditProductNoImage(EditProductRequest requestData);
        Task<Product> EditProduct(EditProductRequest requestData);
        Task<int> DeleteProduct(int? id);
        Task<GetListPrForHomeRes> GetListProductForHome();
        Task<ProductDetailAndRelated> GetProductAndRelated(int? id);
    }
}
