using AutoTrader.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoTrader.Conntext
{
    public class AutoTraderContext : DbContext
    {
        public AutoTraderContext(DbContextOptions<AutoTraderContext> option) : base(option) { }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}