namespace CurrencyExchange.APIService.IdentityDataAccess.DBEntity
{
    public class IdentityAuthdbContext : IdentityDbContext
    {
        public IdentityAuthdbContext(DbContextOptions<IdentityAuthdbContext> options) : base(options)
        {
        }

        //For Update-Databas its required
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=currencyexachnage.database.windows.net;Initial Catalog=idenitydb;User ID=currency;Password=exachnage@123;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleID = Guid.NewGuid().ToString();
            var writterRoleID = Guid.NewGuid().ToString();
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleID,
                    ConcurrencyStamp = readerRoleID,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writterRoleID,
                    ConcurrencyStamp = readerRoleID,
                    Name = "Writter",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
