using Congratulator.Domain.Birthday;
using Microsoft.EntityFrameworkCore;

namespace Congratulator.DataAccess
{
    public class CongratulatorDbContext : DbContext
    {
        public CongratulatorDbContext(DbContextOptions<CongratulatorDbContext> options) : base (options) 
        {
        }

        public DbSet<Birthday> Birthdays { get; set; }
    }
}
