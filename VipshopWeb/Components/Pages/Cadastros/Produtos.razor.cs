
using global::VipshopWeb.Core.Entities;
using global::VipshopWeb.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VipshopWeb.Core.Entities;
using VipshopWeb.Infrastructure;

namespace VipshopWeb.Components.Pages.Cadastros
{
    // A palavra-chave 'partial' indica que esta classe se funde com o arquivo .razor
    public partial class Produtos : ComponentBase
    {
        // No arquivo C#, as diretivas @inject viram propriedades com a anotação [Inject]
        [Inject] protected IDbContextFactory<VipshopDbContext> DbFactory { get; set; } = null!;
        [Inject] protected AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
        [Inject] protected ITenantProvider TenantProvider { get; set; } = null!;
        [Inject] protected ISnackbar Snackbar { get; set; } = null!;

        protected MudForm form = null!;
        protected bool formValido;
        protected System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.GetCultureInfo("pt-BR");

        protected List<Produto> listaProdutos = new();
        protected List<Grupo> listaGrupos = new();
        protected Produto produtoModel = new() { Ativo = true, GrupoId = Guid.Empty };

        protected bool isEdicao = false;
        protected string descricaoOriginal = string.Empty;
        protected string? codigoBarrasOriginal = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var tenantClaim = authState.User.FindFirst("Tenant")?.Value;

            if (!string.IsNullOrEmpty(tenantClaim))
            {
                TenantProvider.SetTenant(tenantClaim);
                await CarregarGrupos();
                await CarregarGrid();
            }
        }

        private async Task CarregarGrupos()
        {
            try
            {
                using var context = await DbFactory.CreateDbContextAsync();
                listaGrupos = await context.Grupos.OrderBy(g => g.Codigo).ToListAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao carregar os grupos de produtos: {ex.Message}", Severity.Error);
            }
        }

        private async Task CarregarGrid()
        {
            try
            {
                using var context = await DbFactory.CreateDbContextAsync();
                listaProdutos = await context.Produtos
                    .Include(p => p.Grupo)
                    .OrderByDescending(p => p.DataCadastro)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao carregar produtos: {ex.Message}", Severity.Error);
            }
        }

        protected async Task SalvarProduto()
        {
            await form.Validate();
            if (!formValido || produtoModel.GrupoId == Guid.Empty)
            {
                Snackbar.Add("Selecione um grupo de produtos válido.", Severity.Warning);
                return;
            }

            string? codigoBarrasDigitado = produtoModel.CodigoBarras?.Trim();

            try
            {
                using var context = await DbFactory.CreateDbContextAsync();

                if (!string.IsNullOrWhiteSpace(codigoBarrasDigitado))
                {
                    if (!isEdicao || codigoBarrasDigitado != codigoBarrasOriginal)
                    {
                        bool codigoJaExiste = await context.Produtos
                            .AnyAsync(p => p.CodigoBarras == codigoBarrasDigitado);

                        if (codigoJaExiste)
                        {
                            Snackbar.Add($"Erro: Código de barras '{codigoBarrasDigitado}' já cadastrado!", Severity.Error);
                            return;
                        }
                    }
                }

                produtoModel.CodigoBarras = codigoBarrasDigitado;

                if (isEdicao)
                {
                    context.Produtos.Update(produtoModel);
                    await context.SaveChangesAsync();
                    Snackbar.Add($"Produto '{produtoModel.Descricao}' atualizado com sucesso!", Severity.Success);
                }
                else
                {
                    produtoModel.DataCadastro = DateTime.UtcNow;
                    context.Produtos.Add(produtoModel);
                    await context.SaveChangesAsync();
                    Snackbar.Add($"Produto '{produtoModel.Descricao}' gravado com sucesso!", Severity.Success);
                }

                ResetarFormulario();
                await CarregarGrid();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Falha ao salvar produto: {ex.InnerException?.Message ?? ex.Message}", Severity.Error);
            }
        }

        protected void PrepararEdicao(Produto produto)
        {
            produtoModel = new Produto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                PrecoVenda = produto.PrecoVenda,
                Ativo = produto.Ativo,
                DataCadastro = produto.DataCadastro,
                CodigoBarras = produto.CodigoBarras,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                GrupoId = produto.GrupoId,
                Referencia = produto.Referencia,
                UnidadeVenda = produto.UnidadeVenda
            };

            descricaoOriginal = produto.Descricao;
            codigoBarrasOriginal = produto.CodigoBarras;
            isEdicao = true;
        }

        protected async Task AlternarStatus(Produto produto)
        {
            try
            {
                using var context = await DbFactory.CreateDbContextAsync();
                produto.Ativo = !produto.Ativo;
                context.Produtos.Update(produto);
                await context.SaveChangesAsync();

                Snackbar.Add($"Produto '{produto.Descricao}' alterado com sucesso!", Severity.Info);
                await CarregarGrid();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao mudar status: {ex.Message}", Severity.Error);
            }
        }

        protected void CancelarEdicao() => ResetarFormulario();

        private void ResetarFormulario()
        {
            produtoModel = new() { Ativo = true, GrupoId = Guid.Empty };
            isEdicao = false;
            descricaoOriginal = string.Empty;
            codigoBarrasOriginal = string.Empty;
        }

        protected async Task<string?> ValidarCodigoBarrasEmTempoReal(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return null;
            if (isEdicao && codigo.Trim() == codigoBarrasOriginal) return null;

            using var context = await DbFactory.CreateDbContextAsync();
            bool existe = await context.Produtos.AnyAsync(p => p.CodigoBarras == codigo.Trim());
            return existe ? "Este código de barras já está em uso." : null;
        }
    }
}

