using System.Linq;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace VipshopWeb.Infrastructure
{
    // Interface para ser injetada nas telas e serviços
    public interface ITenantProvider
    {
        string GetConnectionString();
        void SetTenant(string cnpjCpfEmpresa);
        void SetCatalog();
    }

    public class TenantProvider : ITenantProvider
    {
        private string _connectionString = string.Empty;
        private readonly IConfiguration _configuration;

        public TenantProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            // Se nenhuma empresa foi selecionada ainda, podemos retornar uma string vazia ou padrão
            return _connectionString;
        }

        public void SetTenant(string cnpjCpfEmpresa)
        {
            // Remove pontos, traços e barras, deixando apenas números
            //var dbName = "db_" + new string(cnpjCpfEmpresa.Where(char.IsDigit).ToArray());
            var dbName = "db_" + new string(cnpjCpfEmpresa.Where(char.IsDigit).ToArray());

            // Busca a string base que configuraremos no appsettings.json
            var baseConnection = _configuration.GetConnectionString("DefaultConnection");

            // Substitui dinamicamente o nome do banco de dados na string de conexão
            var builder = new NpgsqlConnectionStringBuilder(baseConnection)
            {
                Database = dbName
            };

            _connectionString = builder.ConnectionString;
        }

        public void SetCatalog()
        {
            var baseConnection = _configuration.GetConnectionString("DefaultConnection");
            var builder = new Npgsql.NpgsqlConnectionStringBuilder(baseConnection)
            {
                Database = "vipshop_catalog" // Nome fixo do banco central
            };
            _connectionString = builder.ConnectionString;
        }


    }


}
