using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Estudos.App.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace Estudos.App.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            DataCadastro = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Descrição")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Descricao { get; set; }
        public string Imagem { get; set; }

        [NotMapped]
        [DisplayName("Imagem do Produto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public IFormFile ImagemUpload { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Moeda]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        /* EF Relation */
        public FornecedorViewModel Fornecedor { get; set; }
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
    }
}
