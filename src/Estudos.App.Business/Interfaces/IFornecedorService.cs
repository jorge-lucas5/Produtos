using System;
using System.Threading.Tasks;
using Estudos.App.Business.Models;

namespace Estudos.App.Business.Interfaces
{
    public interface IFornecedorService
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);

    }
}