using System;
using System.Threading.Tasks;
using Estudos.App.Business.Models;

namespace Estudos.App.Business.Interfaces
{
    public interface IFornecedorRepository :IRepository<Fornecedor>
    {
        Task<Fornecedor> ObeterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObeterFornecedorProdutosEndereco(Guid id);
    }
}