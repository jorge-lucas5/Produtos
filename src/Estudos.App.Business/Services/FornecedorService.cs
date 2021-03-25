using System;
using System.Threading.Tasks;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.Business.Models.Validations;

namespace Estudos.App.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Adicionar(Fornecedor fornecedor)
        {
            //validar o estar da entidade
            if(!ExecutarValidacao(new FornecedorValidation(), fornecedor)
            && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;
            
            //validar se não existe fornecedor com o msm documento
            return;
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}