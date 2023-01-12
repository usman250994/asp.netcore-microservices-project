using Microsoft.EntityFrameworkCore;
using platformService.Models;

namespace platformService.Data 
{

    public class AppDbContext : DbContext
    {
   public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
   {
    
   }
   public DbSet<Platform> Platforms { get; set; }
    }
}