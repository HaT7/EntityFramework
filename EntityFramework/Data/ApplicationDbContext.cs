using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Data
{
    // DbContext thuộc quyền sở hữu của thư viện Microsoft.EntityFrameworkCore
    // kế thừa lớp này để tiến hành mở rộng nó, cấp cho nó các connection string,...
    // Trong file Starup.cs có những nhiệm vụ để setting cho dự án, khi nó setting nó sẽ đưa các file setting vào
    // constructor này (Dependency Injection) và sau đó nó sẽ truyền vào thằng cha của nó là DbContext thì sau
    // đó thằng DbContext này sẽ có những giá trị mà mình đã config cho nó.
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Oder> Oders { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}