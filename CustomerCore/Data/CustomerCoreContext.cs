using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerCore.Models;

namespace CustomerCore.Data
{
    public class CustomerCoreContext : DbContext
    {
        public CustomerCoreContext (DbContextOptions<CustomerCoreContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerCore.Models.Customers> Customers { get; set; }
    }
}
