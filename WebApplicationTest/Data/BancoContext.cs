using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Models;

namespace WebApplicationTest.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        { 
        }

        public DbSet<ContatoModel> Contatos { get; set; }
    }
}
