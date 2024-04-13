using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext _dbContext)
        {
            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                //Deserialize -> from json to another loke list of data , object

                // if (brands is not null && brands.Count()>0)
                if (brands?.Count() > 0)
                {
                    //brands=brands.Select(b=> new ProductBrand()
                    //{
                    //  Name=b.Name,
                    //}).ToList();
                    // Or Remove Id from json file

                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                } 
            }
            if (!_dbContext.ProductCategories.Any())
            {
                var ProductCategoriesData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(ProductCategoriesData);
                //Deserialize -> from json to another loke list of data , object

                // if (brands is not null && brands.Count()>0)
                if (categories?.Count() > 0)
                {
                    //brands=brands.Select(b=> new ProductBrand()
                    //{
                    //  Name=b.Name,
                    //}).ToList();
                    // Or Remove Id from json file

                    foreach (var category in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (!_dbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                //Deserialize -> from json to another loke list of data , object

                // if (brands is not null && brands.Count()>0)
                if (products?.Count() > 0)
                {
                    //brands=brands.Select(b=> new ProductBrand()
                    //{
                    //  Name=b.Name,
                    //}).ToList();
                    // Or Remove Id from json file

                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
