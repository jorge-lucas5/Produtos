using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Estudos.App.Web.Extensions
{
    public class BadgeBooleanTagHelper : TagHelper
    {
        public bool Ativo { get; set; } = true;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var classe = "badge badge-success";
            var texto = "Sim";
            if (!Ativo)
            {
                classe = "badge badge-danger";
                texto = "Não";
            }
            output.TagName = "span";
            var content = await output.GetChildContentAsync();
            var target = content.GetContent();
            output.Attributes.SetAttribute("class", classe);

            if (!string.IsNullOrEmpty(target))
                texto = target;
            output.Content.SetContent(texto);
        }
    }
}
