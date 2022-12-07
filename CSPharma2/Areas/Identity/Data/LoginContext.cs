using CSPharma2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSPharma2.Areas.Identity.Data;

public class LoginContext : IdentityDbContext<UserAuth>
{
    public LoginContext(DbContextOptions<LoginContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.HasDefaultSchema("dlk_torrecontrol");
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<UserAuth>
{
    public void Configure(EntityTypeBuilder<UserAuth> builder)
    {
        builder.Property(usuario => usuario.Nombre).HasMaxLength(255);
        builder.Property(usuario => usuario.Apellido).HasMaxLength(255);
    }
}