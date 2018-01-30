using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Services;
using CoffeeExperience.MVC.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    public static class MVCExtensoes
    {
        #region Hover

        public static string HoverAction(this HtmlHelper helper, string action)
        {
            if (helper.ViewContext.RequestContext.RouteData.Values["action"].Equals(action))
                return "active";
            return string.Empty;
        }

        public static string HoverControler(this HtmlHelper helper, string controller)
        {
            if (helper.ViewContext.RequestContext.RouteData.Values["controller"].Equals(controller))
                return "active";
            return string.Empty;
        }

        public static string Hover(this HtmlHelper helper, string action, string controller)
        {
            if (helper.ViewContext.RequestContext.RouteData.Values["controller"].Equals(controller) && helper.ViewContext.RequestContext.RouteData.Values["action"].Equals(action))
                return "active";
            return string.Empty;
        }

        #endregion

        #region Pegar Nome Cliente

        public static string NomeAlunoLogado(this HtmlHelper helper)
        {
            string retorno = "";
            string Nome = Cookie.GetCookie("pref", "NomeCliente");
            if (!string.IsNullOrEmpty(Nome))
                if (Nome.Contains(" "))
                    Nome = Nome.Substring(0, Nome.IndexOf(' '));
            return retorno + Nome;
        }

        #endregion

        #region Select List

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string defaultOption, string defaultOptionValue)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();

            items.Insert(0, new SelectListItem()
            {
                Text = defaultOption,
                Value = defaultOptionValue,
                Selected = true
            });
            return items;
        }

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string selected)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();

            items.Find(x => x.Value.Equals(selected)).Selected = true;

            return items;
        }

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();
            return items;
        }

        public static List<SelectListItem> SelectListVazio()
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            itens.Add(new SelectListItem { Text = "Selecione", Value = "" });
            return itens;
        }

        public static List<SelectListItem> SelectListSimNao(bool Verdadeiro)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            if (Verdadeiro)
            {
                itens.Add(new SelectListItem { Text = "Sim", Value = "true", Selected = true });
                itens.Add(new SelectListItem { Text = "Não", Value = "false" });
            }
            else
            {
                itens.Add(new SelectListItem { Text = "Sim", Value = "true" });
                itens.Add(new SelectListItem { Text = "Não", Value = "false", Selected = true });
            }
            return itens;
        }

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            List<int> enumerableSelecionados)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f),
                Selected = enumerableSelecionados.Contains(Convert.ToInt32(value(f)))
            }).ToList();
            return items;
        }

        #endregion

        #region CheckBoxlist

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name,
            IEnumerable<SelectListItem> items, string checkBoxCssClass, string labelCssClass)
        {
            var output = new StringBuilder();
            var optionValue = string.Empty;
            foreach (var item in items)
            {
                optionValue = "name_" + item.Value;
                output.Append("<li><label for=\"" + optionValue + "\" class=\"" + labelCssClass + "\">" + item.Text);
                output.Append("<input type=\"checkbox\" id=\"" + optionValue + "\" name=\"MyOptions\" value=\"" + item.Value + "\" class=\"" + labelCssClass + "\"" + (item.Selected ? "checked" : "") + "/>");
                output.Append("</label></li>");
            }

            return new MvcHtmlString(output.ToString());
        }

        #endregion

        #region datas

        public static string ToShortDate(this DateTime? data)
        {
            if (data.HasValue)
                return data.Value.ToShortDateString();
            return string.Empty;
        }

        #endregion

        public static string SimNao(this bool value)
        {
            return (value) ? "Sim" : "Não";
        }

        #region Gerar Menu Automático

        public static string GerarMenu(this UrlHelper helper)
        {
           
            UnitOfWork unit = new UnitOfWork();
            ServiceManager service = new ServiceManager(unit);
            
            StringBuilder menu = new StringBuilder();

            string controller = string.Empty;
            string action = string.Empty;

            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("controller"))
                controller = (string)routeValues["controller"];
            if (routeValues.ContainsKey("action"))
                action = (string)routeValues["action"];

            menu.Append("<li><a class=\"linkTab\" href=" + helper.Action("Index", "Laboratory") + " name=\"Laboratório\" data-tab-id=\"laboratories\"> <span class=\"nav-label\">Laboratórios</span></a></li>");
            menu.Append("<li><a id=\"idLotMenu\" class=\"linkTab\" href=" + helper.Action("Index", "Lot") + " name=\"Lote\" data-tab-id=\"lots\"> <span class=\"nav-label\">Lotes</span></a></li>");
            menu.Append("<li><a id=\"idAnaliseMenu\" class=\"linkTab\" href=" + helper.Action("Index", "Analysis") + " name=\"Análise\" data-tab-id=\"analysis\"> <span class=\"nav-label\">Análises</span></a></li>");



            return menu.ToString();
        }

        #endregion

        #region Gerar Link Detalhe

        

        public static string GerarBotaoCriarCategoria(this UrlHelper helper)
        {
            StringBuilder html = new StringBuilder();
            
            html.Append("<a class=\"btn btn-success pull-left\" onclick=\"openTab('Criar Categoria', 'CategoryEvent/Create', 'acao-criar-categoria')\" href=\"javascript: void(0)\">Criar Categoria</a>");
            
            return html.ToString();
        }

        public static string GerarBotaoCriarLaboratorio(this UrlHelper helper)
        {
            StringBuilder html = new StringBuilder();
            
            html.Append("<a class=\"btn btn-success pull-left\" onclick=\"openTab('Cadastrar Laboratório', 'Laboratory/Create', 'acao-criar-laboratorio')\" href=\"javascript: void(0)\">Cadastrar Laboratório</a>");
            
            return html.ToString();
        }

        public static string GerarBotaoCriarLot(this UrlHelper helper)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<a class=\"btn btn-success pull-left\" onclick=\"openTab('Cadastrar Lote', 'Lot/Create', 'acao-criar-lot')\" href=\"javascript: void(0)\">Cadastrar Lote</a>");

            return html.ToString();
        }
        public static string GerarBotaoCriarProduto(this UrlHelper helper, string LotCode)
        {
            StringBuilder html = new StringBuilder();

             html.Append("<a class=\"btn btn-success pull-left\" onclick=\"openTab('Cadastrar Produto', 'Product/Create?LotCode="+LotCode+"', 'acao-criar-produto-"+LotCode+"')\" href=\"javascript: void(0)\">Cadastrar Produto</a>");

            return html.ToString();
        }

        public static string GerarBotaoCriarAnalise(this UrlHelper helper)
        {           

            StringBuilder html = new StringBuilder();

            html.Append("<a class=\"btn btn-success pull-left\" onclick=\"openTab('Cadastrar Análise', 'Analysis/CreateOrEdit', 'acao-criar-analise')\" href=\"javascript: void(0)\">Cadastrar Análise</a>");

            return html.ToString();
        }

        public static string GerarBotaoCriarResultado(this UrlHelper helper, int AnalysisId)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<a class=\"btn btn-success pull-left\" onclick=\"openTab('Cadastrar Resultado', 'QualityResult/CreateOrEdit?AnalysisId=" + AnalysisId + "', 'acao-criar-resultado-"+AnalysisId+"')\" href=\"javascript: void(0)\">Cadastrar Resultado de Qualidade</a>");
            return html.ToString();
        }
        #endregion

    }

    public class SelectItemFull
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public bool Selecionado { get; set; }

        public SelectItemFull()
        {

        }

        public SelectItemFull(int id, string valor, bool selecionado)
        {
            this.Id = id;
            this.Valor = valor;
            this.Selecionado = selecionado;
        }

    }
}