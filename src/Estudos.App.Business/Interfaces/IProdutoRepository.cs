using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.App.Business.Models;

namespace Estudos.App.Business.Interfaces
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObeterProdutosPorFornecedor(Guid fonecedorId);
        Task<IEnumerable<Produto>> ObeterProdutosFornecedores();
        Task<Produto> ObeterProdutoFornecedor(Guid id);
    }
}