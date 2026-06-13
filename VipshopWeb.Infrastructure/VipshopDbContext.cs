using Microsoft.EntityFrameworkCore;
using VipshopWeb.Core.Entities;
using VipshopWeb.Core.EntitiesCatalog; 


namespace VipshopWeb.Infrastructure
{
    public class VipshopDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        // O construtor recebe as opções do contexto e o nosso provedor de Tenant
        public VipshopDbContext(DbContextOptions<VipshopDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        // Mapeamento das tabelas expostas para o sistema
        public DbSet<TipoFavorecido> TiposFavorecidos { get; set; } = null!;
        public DbSet<Favorecido> Favorecidos { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Fornecedor> Fornecedores { get; set; } = null!;
        public DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Item> Itens { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Empresa> Empresas { get; set; } = null!;
        public DbSet<Grupo> Grupos { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Deixe em branco ou apenas com o base, pois o Program.cs já resolveu a conexão nativamente
            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connString = _tenantProvider.GetConnectionString();

        //    // Se o usuário ainda não digitou o CNPJ, evita passar uma string vazia que quebra o EF
        //    if (string.IsNullOrEmpty(connString))
        //    {
        //        // Uma string temporária apenas para passar pela validação inicial do .NET
        //        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=wvaebxti");
        //    }
        //    else
        //    {
        //        // Conexão real para o banco da empresa
        //        optionsBuilder.UseNpgsql(connString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            // Carga inicial (Seed Data) para os índices obrigatórios da tabela tipos_favorecidos
            modelBuilder.Entity<TipoFavorecido>().HasData(
                new TipoFavorecido { Id = 1, Descricao = "Cliente" },
                new TipoFavorecido { Id = 2, Descricao = "Fornecedor" },
                new TipoFavorecido { Id = 3, Descricao = "Funcionário" }
            );

            // Configuração da Herança TPT (Table-Per-Type) de forma explícita
            // Define que o ID das tabelas filhas aponta diretamente para a tabela pai de favorecidos
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.FavorecidoId);

            modelBuilder.Entity<Fornecedor>()
                .HasKey(f => f.FavorecidoId);

            modelBuilder.Entity<Funcionario>()
                .HasKey(f => f.FavorecidoId);


            // Configuração da Classe Base (Item)
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Servico>().ToTable("Servicos");

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Itens");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Codigo).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descricao).HasMaxLength(150).IsRequired();
                entity.Property(e => e.PrecoVenda).HasPrecision(18, 2);
                entity.Property(e => e.Custo).HasPrecision(18, 2);
                entity.Property(e => e.Tipo).HasConversion<int>().IsRequired();

                // Criar um índice no código para buscas rápidas
                entity.HasIndex(e => e.Codigo).IsUnique();
            });

            // Configuração de Produto (Herdando de Item)
            modelBuilder.Entity<Produto>(entity =>
            {
                // 1. Mantém a estratégia TPT apontando para a sua tabela física existente
                entity.ToTable("Produtos");

                // 2. Mantém as suas propriedades comerciais atuais
                entity.Property(e => e.UnidadeVenda).HasMaxLength(10);
                entity.Property(e => e.Referencia).HasMaxLength(100);

                // 3. Configura a NOVA Chave Estrangeira de relacionamento com a tabela Grupos
                entity.HasOne(p => p.Grupo)
                      .WithMany()
                      .HasForeignKey(p => p.GrupoId)
                      .OnDelete(DeleteBehavior.Restrict); // Impede apagar um grupo que tenha produtos vinculados
            });

            // Configuração de Serviço (Herdando de Item)
            modelBuilder.Entity<Servico>(entity =>
            {
                entity.ToTable("Servicos");
                entity.Property(e => e.QuantidadeMaoObra).HasPrecision(18, 2);
            });
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), // ID fixo para o Seed
                Login = "admin",
                SenhaHash = "123", // No futuro aplicaremos hash aqui
                Nome = "Administrador Vipshop",
                Ativo = true
            }
            );
        }
    }
}
