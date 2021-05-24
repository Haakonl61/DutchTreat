using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext context;
        private readonly ILogger<DutchRepository> logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context
                .Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {

                logger.LogInformation("GetAllProducts called");
                return context.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex.Message}");
            }
            return null;
        }

        public Order GetOrderById(int id)
        {
            return context
                .Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return context.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
