using Dapper;
using LapTopStore_Computer.Models;
using LapTopStore_Computer.MyInterface;
using LapTopStore_Computer.MyDapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LapTopStore_Computer.Data;
using LapTopStore_Computer.Data.Product;

namespace LapTopStore_Computer.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(LapTopStoreContext context, IApplicationDbConnection applicationDbConnection) : base(context, applicationDbConnection)
        {
        }

        public async Task<Product> GetProductById(int? id)
        {
            var result = _context.Products.Where(x => x.ProductId== id).FirstOrDefault();

            if(result == null)
            {
                return new Product();
            }

            return result;
        }



        public async Task<List<ProductWithCategory>> AdminIndex(int CatID)
        {
            List<ProductWithCategory> products = new List<ProductWithCategory>();
            List<Product> products2 = new List<Product>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CatID", CatID);
                products = await _applicationDbConnection.QueryAsync<ProductWithCategory>("LayDsTuCatID1", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }

            return products;
        }

        public async Task<List<Category>> GetListCategory()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = await _applicationDbConnection.QueryAsync<Category>("GetListCategory", commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw;
            }

            return categories;
        }

        public async Task<int> CreateProduct(Product product)
        {
            
            var result = _context.Products.Add(product);
            await _context.SaveChangesAsync();
            if(result == null)
            {
                return -1;
            }

            return product.ProductId;
        }

        public async Task<int> CreateProductAtrtibute(CreateProductAttribute request)
        {
            foreach (var item in request.AttrIDList)
            {
                var productAttr = new ProductAttribute()
                { 
                    ProductId = request.ProductID,
                    AttrId = item
                };

                _context.ProductAttributes.Add(productAttr);
            }

            return 1;

        }

        public async Task<GetListAttribute> GetListAttribute()
        {
            var result = new GetListAttribute();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AttrName", "Color");
                result.AttributeColor = await _applicationDbConnection.QueryAsync<Models.Attribute>("GetListAttr", parameters, commandType: CommandType.StoredProcedure);

                parameters.Add("@AttrName", "CPU");
                result.AttributeCPU = await _applicationDbConnection.QueryAsync<Models.Attribute>("GetListAttr", parameters, commandType: CommandType.StoredProcedure);

                parameters.Add("@AttrName", "RAM");
                result.AttributeRAM = await _applicationDbConnection.QueryAsync<Models.Attribute>("GetListAttr", parameters, commandType: CommandType.StoredProcedure);

                parameters.Add("@AttrName", "System");
                result.AttributeSystem = await _applicationDbConnection.QueryAsync<Models.Attribute>("GetListAttr", parameters, commandType: CommandType.StoredProcedure);

                parameters.Add("@AttrName", "Screen");
                result.AttributeScreen = await _applicationDbConnection.QueryAsync<Models.Attribute>("GetListAttr", parameters, commandType: CommandType.StoredProcedure);

                parameters.Add("@AttrName", "Driver");
                result.AttributeDriver = await _applicationDbConnection.QueryAsync<Models.Attribute>("GetListAttr", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public async Task<ProductDetails> GetProductDetail(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("id");
            }

            List<ProductDetails> list = new List<ProductDetails>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductID", id);
                list = await _applicationDbConnection.QueryAsync<ProductDetails>("GetProductDetails", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }

            return list[0];
        }

        public async Task<ProductDetailAndRelated> GetProductAndRelated(int? id)
        {
            var result = new ProductDetailAndRelated();

            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            List<ProductDetails> list1 = new List<ProductDetails>();
            List<ProductDetails> list2 = new List<ProductDetails>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductID", id);
                list1 = await _applicationDbConnection.QueryAsync<ProductDetails>("GetProductDetails", parameters, commandType: CommandType.StoredProcedure);

                list2 = await _applicationDbConnection.QueryAsync<ProductDetails>("GetRelated", parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw;
            }

            result.product = list1[0];
            result.listRelated = list2;
            return result;
        }

        public async Task<Product> EditProductNoImage(EditProductRequest requestData)
        {
            var product = _context.Products.Where(x => x.ProductId == requestData.ProductId).FirstOrDefault();
            product.ProductName = requestData.ProductName;
            product.ProductDescription = requestData.ProductDescription;
            product.ProductPrice = requestData.ProductPrice;
            product.CategoryId = requestData.CategoryId;
            product.ProductDiscount = requestData.ProductDiscount;
            product.ProductInStock = requestData.ProductInStock;
            product.HomeFlag = requestData.HomeFlag;
            product.BestSeller = requestData.BestSeller;

            var productAttr = _context.ProductAttributes.Where(x => x.ProductId == requestData.ProductId).ToList();
            productAttr[0].AttrId = requestData.ColorCategory;
            productAttr[1].AttrId = requestData.RamCategory;
            productAttr[2].AttrId = requestData.CpuCategory;
            productAttr[3].AttrId = requestData.ScreenCategory;
            productAttr[4].AttrId = requestData.OsCategory;
            productAttr[5].AttrId = requestData.HddCategory;

            return product;
        }

        public async Task<Product> EditProduct(EditProductRequest requestData)
        {
            var product = _context.Products.Where(x => x.ProductId == requestData.ProductId).FirstOrDefault();
            product.ProductName = requestData.ProductName;
            product.ProductDescription = requestData.ProductDescription;
            product.ProductPrice = requestData.ProductPrice;
            product.CategoryId = requestData.CategoryId;
            product.ProductDiscount = requestData.ProductDiscount;
            product.ProductInStock = requestData.ProductInStock;
            product.HomeFlag = requestData.HomeFlag;
            product.BestSeller = requestData.BestSeller;
            if(requestData.PrImage1 != null)
            {
                product.PrImage1 = requestData.PrImage1;
            }
            if (requestData.PrImage2 != null)
            {
                product.PrImage2 = requestData.PrImage2;
            }
            if (requestData.PrImage3 != null)
            {
                product.PrImage3 = requestData.PrImage3;
            }
            if (requestData.PrImage4 != null)
            {
                product.PrImage4 = requestData.PrImage4;
            }

            var productAttr = _context.ProductAttributes.Where(x => x.ProductId == requestData.ProductId).ToList();
            productAttr[0].AttrId = requestData.ColorCategory;
            productAttr[1].AttrId = requestData.RamCategory;
            productAttr[2].AttrId = requestData.CpuCategory;
            productAttr[3].AttrId = requestData.ScreenCategory;
            productAttr[4].AttrId = requestData.OsCategory;
            productAttr[5].AttrId = requestData.HddCategory;

            return product;
        }

        public async Task<int> DeleteProduct(int? id)
        {
            var ListProductAttr = _context.ProductAttributes.Where(x=> x.ProductId== id).ToList();

            if(ListProductAttr == null && ListProductAttr.Count < 0)
            {
                return -1;
            }    

            _context.ProductAttributes.RemoveRange(ListProductAttr);

            var product = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if(product == null)
            {
                return -1;
            }
            _context.Products.Remove(product);

            return 1;
        }

        public async Task<GetListPrForHomeRes> GetListProductForHome()
        {
            var result = new GetListPrForHomeRes();
            var list = new List<ProductWithCategory>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CatID", 0);
                list = await _applicationDbConnection.QueryAsync<ProductWithCategory>("LayDsTuCatID1", parameters, commandType: CommandType.StoredProcedure);

                result.listLenovo = list.Where(x => x.HomeFlag == true && x.CategoryId == 1).ToList();
                result.listAcer = list.Where(x => x.HomeFlag == true && x.CategoryId == 3).ToList();
                result.listMac = list.Where(x => x.HomeFlag == true && x.CategoryId == 4).ToList();
                result.listHP = list.Where(x => x.HomeFlag == true && x.CategoryId == 5).ToList();
                result.listDELL = list.Where(x => x.HomeFlag == true && x.CategoryId == 6).ToList();
                result.listMSI = list.Where(x => x.HomeFlag == true && x.CategoryId == 8).ToList();
                result.listASUS = list.Where(x => x.HomeFlag == true && x.CategoryId == 9).ToList();
                result.topBestSeller = list.Where(x => x.BestSeller == true).Take(8).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        //public async Task<int> CheckProduct(Product product)
        //{
        //    List<Product> list = new List<Product>();

        //    list = _context.Products.ToList();

        //    foreach (var item in list)
        //    {
        //        if (product.ProductName == item.ProductName && product.ProductDescription == item.ProductDescription 
        //            && product.ProductPrice == item.ProductPrice && product.)
        //        {

        //        }
        //    }


        //}
    }
}
