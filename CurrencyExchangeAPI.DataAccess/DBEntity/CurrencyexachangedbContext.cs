
namespace CurrencyExchange.APIService.DataAccess.Entity;

public partial class CurrencyExachangedbContext : DbContext
{
    public CurrencyExachangedbContext()
    {
    }

    public CurrencyExachangedbContext(DbContextOptions<CurrencyExachangedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CurrencyMaster> CurrencyMasters { get; set; }

    public virtual DbSet<CurrencyRate> CurrencyRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyMaster>(entity =>
        {
            entity.HasKey(e => e.CurrencyId);

            entity.ToTable("tbl_CurrencyMaster");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("('Admin')");
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CurrencyRate>(entity =>
        {
            entity.HasKey(e => e.CurrencyRateId);

            entity.ToTable("tbl_CurrencyRate");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("('Admin')");
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExchageRate).HasColumnType("decimal(14, 7)");
            entity.Property(e => e.IsLatest)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.BaseCurrency).WithMany(p => p.TblCurrencyRates)
                .HasForeignKey(d => d.BaseCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseCurrenyId");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
