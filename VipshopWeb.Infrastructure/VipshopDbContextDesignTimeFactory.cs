using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VipshopWeb.Infrastructure
{
    public class VipshopDbContextDesignTimeFactory : IDesignTimeDbContextFactory<VipshopDbContext>
    {
        public VipshopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VipshopDbContext>();
            // Aponta para o banco modelo padrão apenas para o CLI do VS conseguir criar o arquivo
            optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");

            // Passa um TenantProvider nulo ou vazio apenas para o construtor aceitar
            return new VipshopDbContext(optionsBuilder.Options, new TenantProviderFake());
        }
    }

    public class TenantProviderFake : ITenantProvider
    {
        public string GetConnectionString() => "Host=localhost;Database=postgres;Username=postgres;Password=postgres";
        public void SetTenant(string cnpjCpfEmpresa) { }
        public void SetCatalog() { }
    }
}
