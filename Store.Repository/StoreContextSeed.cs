using Microsoft.Extensions.Logging;
using Store.Data.contexts;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed                          //function بتقرا الفايلات +  db تضيفها في ال
    {
        public static async Task SeedDataAsync(StoreDbContext context, ILoggerFactory loggerFactory)

        {
            try

            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())

                {
                    var BrandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);            //(Deserialize)string(list) => object(list)

                    if (Brands != null)

                    {
                       
                        await context.ProductBrands.AddRangeAsync(Brands);

                    }

                    if (context.ProductTypes != null && !context.ProductTypes.Any())

                    {
                        var TypeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                        var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                        if (Types != null)

                        {
                            
                            await context.ProductTypes.AddRangeAsync(Types);

                        }
                    }

                    if (context.Products != null && !context.Products.Any())

                    {
                        var ProductData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                        var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                        if (Products != null)

                        {
                 
                            await context.Products.AddRangeAsync(Products);

                        }

                           await context.SaveChangesAsync();
                    }
                }
            }



            catch (Exception ex)

            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}








