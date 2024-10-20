using Microsoft.EntityFrameworkCore;
using DTSMS.Models;

namespace DTSMS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }

        // 定義 DbSet 來映射 FileRecord 表
        public DbSet<FileModel> FileModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
