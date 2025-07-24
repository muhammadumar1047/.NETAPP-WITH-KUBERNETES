using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using MyApi.Data;
using MyApi.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Tests;

public class ProductsControllerTests
{

    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new AppDbContext(options);
    }


    [Fact]
        public async Task GetProducts_ReturnsProducts()
        {
            var context = GetDbContext();
            context.Products.Add(new Product { Id = 1, Name = "Test", Price = 100 });
            await context.SaveChangesAsync();

            var controller = new ProductsController(context);

            var result = await controller.GetProducts();

            var okResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var products = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Single(products);
        }
}