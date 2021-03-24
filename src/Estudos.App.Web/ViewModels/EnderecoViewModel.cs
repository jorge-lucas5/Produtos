using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Estudos.App.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.Web.ViewModels
{
    public class EnderecoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        
        public string Logradouro { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Número")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Numero { get; set; }
       
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O campo {0} precisa ter {1} caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Estado { get; set; }

        /* EF Relation */
        public FornecedorViewModel Fornecedor { get; set; }

        [HiddenInput]
        public Guid FornecedorId { get; set; }

    }
}