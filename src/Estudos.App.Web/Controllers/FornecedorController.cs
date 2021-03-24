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
    public class FornecedorController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        #region Actions
        public async Task<IActionResult> Index()
        {
            var consulta = await _fornecedorRepository.ObterTodos();
            var lista = _mapper.Map<IEnumerable<FornecedorViewModel>>(consulta);
            return View(lista);
        }

        public async Task<IActionResult> Details(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Adicionar(fornecedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObeterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Atualizar(fornecedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var existe = await _fornecedorRepository.Existe(id);
            if (!existe) return NotFound();

            await _fornecedorRepository.Remover(id);

            return RedirectToAction(nameof(Index));
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
