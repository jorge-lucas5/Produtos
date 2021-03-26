using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.Web.Util;
using Microsoft.AspNetCore.Mvc;
using Estudos.App.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Estudos.App.Web.Controllers
{
    [Route("produto")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProdutoController(IProdutoRepository produtoRepository,
                                IMapper mapper, IFornecedorRepository fornecedorRepository,
                                IConfiguration configuration,
                                IProdutoService produtoService,
                                INotificador notificador
                                ) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
            _configuration = configuration;
            _produtoService = produtoService;
        }

        #region Actions

        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            var consulta = await _produtoRepository.ObeterProdutosFornecedores();
            var lista = _mapper.Map<IEnumerable<ProdutoViewModel>>(consulta);
            return View(lista);
        }

        [Route("detalhes-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return NotFound();

            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
            ViewBag.FornecedorId = new SelectList(produtoViewModel.Fornecedores, "Id", "Nome");
            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            var caminho = await FileHelper.UploadArquivo(produtoViewModel.ImagemUpload, _configuration);
            if (!ModelState.IsValid || string.IsNullOrEmpty(caminho))
            {
                produtoViewModel = await PopularFornecedores(produtoViewModel);
                ViewBag.FornecedorId = new SelectList(produtoViewModel.Fornecedores, "Id", "Nome");
                return View(produtoViewModel);
            }

            var produto = _mapper.Map<Produto>(produtoViewModel);
            produto.Imagem = caminho;
            await _produtoService.Adicionar(produto);

            if (!OperacaoValida()) return View(produtoViewModel);

            NotificacaoSucesso("produto cadastrado com sucesso");

            return RedirectToAction(nameof(Index));

        }

        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return NotFound();

            return View(produtoViewModel);
        }

        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await _produtoRepository.ObterPorId(id);
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;
            ModelState.Remove(nameof(produtoViewModel.ImagemUpload));
            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var caminho = await FileHelper.UploadArquivo(produtoViewModel.ImagemUpload, _configuration);
                if (string.IsNullOrEmpty(caminho))
                {
                    ModelState.AddModelError(string.Empty, "Erro ao realizar upload da imagem");
                    return View(produtoViewModel);
                }
                produtoAtualizacao.Imagem = caminho;
            }

            produtoAtualizacao.Ativo = produtoViewModel.Ativo;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Nome = produtoViewModel.Nome;

            var produto = _mapper.Map<Produto>(produtoAtualizacao);
            await _produtoService.Atualizar(produto);

            if (!OperacaoValida()) return View(produtoViewModel);

            NotificacaoSucesso("produto ditado com sucesso");

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return NotFound();

            return View(produtoViewModel);
        }

        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var existe = await _produtoRepository.Existe(id);
            if (!existe) return NotFound();

            await _produtoService.Remover(id);

            if (!OperacaoValida()) return RedirectToAction("Delete", id);

            NotificacaoSucesso("produto excluído com sucesso");

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Privates

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObeterProdutoFornecedor(id));
            produto.Fornecedores =
                _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores =
                _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }



        #endregion
    }
}
