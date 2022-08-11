
using FoodOrdering.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<CustomerTable> CustomersTable { get; set; }
        public DbSet<FoodTable> FoodTables { get; set; }
        public DbSet<OrderTable> OrdersTable { get; set; }
        public DbSet<AdminTable> AdminsTable { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }


    }
}
