namespace Estudos.App.Web.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            ErroCode = 500;
            Titulo = "Ops! Erro Interno do Servidor!";
            Mensagem = "Infelizmente, estamos com problemas para carregar a p�gina que voc� est� procurando. Volte daqui a pouco.";
        }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
