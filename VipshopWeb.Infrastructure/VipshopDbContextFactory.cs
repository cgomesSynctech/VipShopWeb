using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace VipshopWeb.Infrastructure
{
    public class VipshopDbContextFactory : IDbContextFactory<VipshopDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public VipshopDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public VipshopDbContext CreateDbContext()
        {
            // Busca o ITenantProvider do circuito ativo (onde o CNPJ já foi preenchido)
            var tenantProvider = _serviceProvider.GetRequiredService<ITenantProvider>();

            var optionsBuilder = new DbContextOptionsBuilder<VipshopDbContext>();
            var connString = tenantProvider.GetConnectionString();

            if (string.IsNullOrEmpty(connString))
            {
                // Conexão padrão inicial apenas para validações de inicialização do .NET
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=wvaebxti");
            }
            else
            {
                // Conexão real e dinâmica para o banco de dados da empresa (ex: db_12345678000199)
                optionsBuilder.UseNpgsql(connString);
            }

            return new VipshopDbContext(optionsBuilder.Options, tenantProvider);
        }
    }
}
