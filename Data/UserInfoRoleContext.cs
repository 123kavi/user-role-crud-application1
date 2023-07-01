namespace server.Data
{
    public class UserInfoRoleContext : DbContext
    {

        public UserInfoRoleContext(DbContextOptions<UserInfoRoleContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=infos_7d9j;Username=postgres;Password=root;");
        }
        public DbSet<UserInfoRole> UserInfoRole { get; set; }

    }
}
