using Kursova.Modul.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kursova.Modul
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("DBConnectionString")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDate> Dates { get; set; }
        public DbSet<UserActivity> Activities { get; set; }
        public DbSet<UserHealth> Healths { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(d => d.Date)
                .WithRequired(d => d.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserDate>()
                .HasMany(ud => ud.Health)
                .WithRequired(h => h.Date)
                .HasForeignKey(h => h.DateId);

            modelBuilder.Entity<UserDate>()
                .HasMany(d => d.Activity)
                .WithRequired(h => h.Date)
                .HasForeignKey(k => k.DateId);

            
        }
        public User GetUserByName(string name)
        {
            Task<User> user = Users.FirstOrDefaultAsync(u => u.Name == name);
            return user.Result;
        }
        public User GetUserById(int id)
        {
            Task<User> user = Users.FirstOrDefaultAsync(u => u.Id == id);
            return user.Result;
        }
    }
}
