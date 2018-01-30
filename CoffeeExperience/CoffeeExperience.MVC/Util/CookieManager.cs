using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeExperience.MVC.Util
{
    public static class Cookie
    {
        #region Métodos Private

        private static string setValor(string valor)
        {
            return valor.ToCriptografaQueryString();
        }

        private static string getValor(string valor)
        {
            return valor.ToDescriptografaQueryString();
        }

        #endregion

        public static void RemoverCookie(string nomeCookie)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomeCookie];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void SetCookie(string nomeCookie, string valor)
        {
            HttpCookie cookie = new HttpCookie(nomeCookie);
            cookie.Value = setValor(valor);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookie(string nomeCookie, string parametro, string valor)
        {
            parametro = parametro.ToCriptografaQueryString();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomeCookie];
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(cookie.Values.Get(parametro)))
                    cookie.Values.Set(parametro, setValor(valor));
                else
                    cookie.Values.Add(parametro, setValor(valor));
            }
            else
            {
                cookie = new HttpCookie(nomeCookie);
                cookie.Values.Add(parametro, setValor(valor));
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string nomeCookie)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomeCookie];
            if (cookie != null)
                return getValor(cookie.Value);
            return string.Empty;
        }

        public static string GetCookie(string nomeCookie, string parametro)
        {
            parametro = parametro.ToCriptografaQueryString();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomeCookie];
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(cookie.Values.Get(parametro)))
                    return getValor(cookie.Values.Get(parametro));
            }
            return string.Empty;
        }

        public static void SetExpire(string nomeCookie, DateTime DataExpiracao)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomeCookie];
            if (cookie != null)
            {
                cookie.Expires = DataExpiracao;
            }
        }

        public static void SetCookieExpire(string nomeCookie, string valor, DateTime tempoExpiracao)
        {
            HttpCookie cookie = new HttpCookie(nomeCookie);
            cookie.Value = setValor(valor);
            cookie.Expires = tempoExpiracao;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}