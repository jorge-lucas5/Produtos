using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estudos.App.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }
        public async Task<Produto> ObeterProdutoFornecedor(Guid id)
        {
            return await Context.Produtos.AsNoTracking()
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<IEnumerable<Produto>> ObeterProdutosPorFornecedor(Guid fonecedorId)
        {
            return await Buscar(p => p.FornecedorId == fonecedorId);
        }

        public async Task<IEnumerable<Produto>> ObeterProdutosFornecedores()
        {
            return await Context.Produtos.AsNoTracking()
                .Include(p => p.Fornecedor)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }
    }
}