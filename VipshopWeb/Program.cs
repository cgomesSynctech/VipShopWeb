using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using VipshopWeb.Auth;
using VipshopWeb.Components;
using VipshopWeb.Infrastructure;

namespace VipshopWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // 1. Adiciona os serviços visuais do MudBlazor
            builder.Services.AddMudServices();

            //// 1. O TenantProvider continua Scoped (um por aba/usuário logado)
            //builder.Services.AddScoped<ITenantProvider, TenantProvider>();

            //// 2. Registra a nossa fábrica customizada que resolve o problema de escopo raiz
            //builder.Services.AddSingleton<IDbContextFactory<VipshopDbContext>, VipshopDbContextFactory>();


            // 1. O TenantProvider continua Scoped (um por aba/usuário logado)
            builder.Services.AddScoped<ITenantProvider, TenantProvider>();

            // 2. Altere para AddScoped para que a fábrica viva no mesmo circuito da tela do usuário
            builder.Services.AddScoped<IDbContextFactory<VipshopDbContext>, VipshopDbContextFactory>();


            // 2. Injeta o nosso Provedor de Tenant (Múltiplos bancos de dados)
            // Usamos Scoped para que cada aba/usuário logado tenha a sua própria conexão isolada
            //builder.Services.AddScoped<ITenantProvider, TenantProvider>();
            ////builder.Services.AddTransient<ITenantProvider, TenantProvider>();

            //builder.Services.AddPooledDbContextFactory<VipshopDbContext>((serviceProvider, options) =>
            //{
            //    var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
            //    var connString = tenantProvider.GetConnectionString();

            //    if (string.IsNullOrEmpty(connString))
            //    {
            //        // Conexão temporária padrão apenas para a validação inicial do .NET
            //        options.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=wvaebxti");
            //    }
            //    else
            //    {
            //        // Conexão real dinâmica para o banco da empresa
            //        options.UseNpgsql(connString);
            //    }
            //});
            // 3. Configura a Fábrica de Contexto do Entity Framework (DbContextFactory)
            // No Blazor Server, o DbContextFactory evita erros de concorrência quando a tela atualiza dados rapidamente
            //builder.Services.AddDbContextFactory<VipshopDbContext>();
            //builder.Services.AddDbContextFactory<VipshopDbContext>();

            //builder.Services.AddDbContextFactory<VipshopDbContext>((serviceProvider, options) =>
            //{
            //    var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
            //    options.UseNpgsql(tenantProvider.GetConnectionString());
            //});

            // 4. Configuração de Autenticação Customizada para o Blazor Server
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddCascadingAuthenticationState();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
