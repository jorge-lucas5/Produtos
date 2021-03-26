using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Estudos.App.Web.ViewModels;

namespace Estudos.App.Web.Controllers
{
    [Route("fornecedor")]
    public class FornecedorController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository,
            IMapper mapper, IFornecedorService fornecedorService,INotificador notificador
        ) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        #region Actions
        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            var consulta = await _fornecedorRepository.ObterTodos();
            var lista = _mapper.Map<IEnumerable<FornecedorViewModel>>(consulta);
            return View(lista);
        }

        [Route("detalhes-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }
        
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo-fornecedor")]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            NotificacaoSucesso("Fonecedor cadastrado com sucesso");
            return RedirectToAction(nameof(Index));
        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObeterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            NotificacaoSucesso("Fonecedor editado com sucesso");
            return RedirectToAction(nameof(Index));
        }

        [Route("exluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        [Route("exluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var existe = await _fornecedorRepository.Existe(id);
            if (!existe) return NotFound();

            await _fornecedorService.Remover(id);

            if (!OperacaoValida()) return RedirectToAction("Delete", id);

            NotificacaoSucesso("Fonecedor excluído com sucesso");
            return RedirectToAction(nameof(Index));
        }

        [Route("atualizar-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null) return NotFound();

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [Route("atualizar-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove(nameof(fornecedorViewModel.Nome));
            ModelState.Remove(nameof(fornecedorViewModel.Documento));
            if (!ModelState.IsValid)
                return PartialView("_AtualizarEndereco", fornecedorViewModel);

            var endereco = _mapper.Map<Endereco>(fornecedorViewModel.Endereco);
            await _fornecedorService.AtualizarEndereco(endereco);

            var url = Url.Action("ObterEndereco", "Fornecedor", new { id = endereco.FornecedorId });

            return Json(new { url, success = true });

        }

        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null) return NotFound();

            return PartialView("_DetalhesEndereco", fornecedor);
        }
        #endregion

        #region Metodos privados
        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObeterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObeterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObeterFornecedorProdutosEndereco(id));
        }
        #endregion


    }
}
