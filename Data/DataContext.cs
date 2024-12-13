using Microsoft.EntityFrameworkCore;
using WizStore.Entities;

namespace WizStore.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<MagicItem> MagicItems { get; set; }

    }
}
