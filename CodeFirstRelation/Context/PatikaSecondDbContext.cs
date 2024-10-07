using CodeFirstRelation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CodeFirstRelation.Context
{
    public class PatikaSecondDbContext : DbContext
    {
        //ctor a generic bir parametre vereceğiz DbContextOptions diye bu dbcontext sınıfındaki verilerle veritabanı yapılandırılmasını ayarlayacak.
        public PatikaSecondDbContext(DbContextOptions<PatikaSecondDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired();
                //1 to many için ilişkilendirme yaptık
                entity.HasMany(e => e.Posts)
                     .WithOne(e => e.User)
                     .HasForeignKey(e => e.UserId)
                     .OnDelete(DeleteBehavior.Cascade);// bir kullanıcı silindiğinde ona ait postlarda silinsin diye yazdık
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);

            });

            
        }
    }
}
