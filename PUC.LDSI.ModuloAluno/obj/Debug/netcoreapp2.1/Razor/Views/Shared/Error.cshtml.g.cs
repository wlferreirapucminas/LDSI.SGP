#pragma checksum "C:\Users\user\source\repos\entrega4\PUC.LDSI.ModuloAluno\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7927fb394a845ebb5d8b69b9e82a1fd7815df6dc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PUC.LDSI.ModuloAluno.Pages.Views.Shared.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(PUC.LDSI.ModuloAluno.Pages.Views.Shared.Views_Shared_Error))]
namespace PUC.LDSI.ModuloAluno.Pages.Views.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\user\source\repos\entrega4\PUC.LDSI.ModuloAluno\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 1 "C:\Users\user\source\repos\entrega4\PUC.LDSI.ModuloAluno\Views\_ViewImports.cshtml"
using PUC.LDSI.ModuloAluno;

#line default
#line hidden
#line 2 "C:\Users\user\source\repos\entrega4\PUC.LDSI.ModuloAluno\Views\_ViewImports.cshtml"
using PUC.LDSI.ModuloAluno.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7927fb394a845ebb5d8b69b9e82a1fd7815df6dc", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0760690a83d44e6cc9c28761c51f37049e085930", @"/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"025521faddbbbf0d8c4035ec06dc045788e642a3", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\user\source\repos\entrega4\PUC.LDSI.ModuloAluno\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
            BeginContext(60, 172, true);
            WriteLiteral("<h1 class=\"text-danger\">Ops!</h1>\n<h4 style=\"margin-top:30px;\" class=\"text-danger\">Um erro ocorreu durante o processamento do seu pedido.</h4>\n<h3 style=\"margin-top:30px;\">");
            EndContext();
            BeginContext(233, 18, false);
#line 7 "C:\Users\user\source\repos\entrega4\PUC.LDSI.ModuloAluno\Views\Shared\Error.cshtml"
                        Write(Model.ErrorMessage);

#line default
#line hidden
            EndContext();
            BeginContext(251, 118, true);
            WriteLiteral("</h3>\n<p style=\"margin-top:30px;\"><a href=\"javascript:window.history.back();\" class=\"btn btn-default\">Voltar</a></p>\n\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
