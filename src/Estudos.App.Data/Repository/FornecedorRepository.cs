using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estudos.App.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext context) : base(context) { }

        public async Task<Fornecedor> ObeterFornecedorEndereco(Guid id)
        {
            return await Context.Fornecedores.AsNoTracking()
                .Include(f=> f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObeterFornecedorProdutosEndereco(Guid id)
        {
            return await Context.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .Include(f => f.Produtos)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
