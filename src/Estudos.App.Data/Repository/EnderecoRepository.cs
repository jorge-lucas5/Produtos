using System;
using System.Threading.Tasks;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estudos.App.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext context) : base(context) { }

        public async Task<Endereco> ObeterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Context.Enderecos
                .FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
        }
    }
}