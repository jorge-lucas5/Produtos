using System;
using System.Threading.Tasks;
using Estudos.App.Business.Models;

namespace Estudos.App.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObeterEnderecoPorFornecedor(Guid fornecedorId);
    }
}