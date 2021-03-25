using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Estudos.App.Web.Configurations
{
    public static class GlobalizationConfig
    {
        public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
        {
            var defaultCulture = new CultureInfo("pt-BR");

            app.UseRequestLocalization(opt =>
            {
                opt.DefaultRequestCulture = new RequestCulture(defaultCulture);
                opt.SupportedCultures = new List<CultureInfo> { defaultCulture };
                opt.SupportedUICultures = new List<CultureInfo> { defaultCulture };
            });

            return app;
        }
    }
}