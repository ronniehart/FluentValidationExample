using Microsoft.EntityFrameworkCore;

namespace FluentValidationExample.Data
{
    public class FluentValidationExampleContext : DbContext
    {
        public FluentValidationExampleContext(DbContextOptions<FluentValidationExampleContext> options)
            : base(options)
        {
        }

        public DbSet<FluentValidationExample.Models.Person> Person { get; set; } = default!;
    }
}