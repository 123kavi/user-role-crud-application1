namespace server.Data
{
    public class AuthContext : DbContext
    {

        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=infos_7d9j;Username=postgres;Password=root;");
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(entity => entity.HasIndex(e => e.Email).IsUnique());
            
        }

    }
}
