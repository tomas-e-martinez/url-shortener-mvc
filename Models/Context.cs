using Microsoft.EntityFrameworkCore;

namespace url_shortener_mvc.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Url> Urls { get; set; }
    }
}
