using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Infra.CrossCutting.System
{
    public static class SystemStringHelper
    {
        public static string TempoFormatado(this TimeSpan time)
        {
            DateTime dt = new DateTime(time.Ticks);
            return dt.ToString("HH:mm:ss");
        }

        public static int[] ToArrayOfInt(this string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                string[] arrayString = valor.Split(',');

                return arrayString.Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToArray();
            }
            return null;
        }

        public static string[] ToArrayOfStringComSeparator(this string valor, char separator)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                string[] arrayString = valor.Split(separator);

                return arrayString.ToArray();
            }
            return null;
        }

        public static string ToPassword(this string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                string result = string.Empty;

                for (int i = 0; i < valor.Length; i++)
                {
                    result = result + "*";
                }
                return result;
            }
            return null;
        }

        public static string Config(this string valor)
        {
            return ConfigurationManager.AppSettings[valor];
        }

        public static string ToUrlSistemaFormatada(this string Url)
        {
            string nome = Url.Remove(0, 7);
            return nome.Remove((nome.Length - 1), 1);
        }

        public static string GetHtmlString(this string valor, int tamanho)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                //valor = System.Text.RegularExpressions.Regex.Replace(valor, @"<[^>]*>", "");
                valor = valor.Replace("&nbsp;", " ");

                string saida = string.Empty;
                string[] palavras;
                palavras = valor.Split(' ');
                int count;
                for (count = 0; count < palavras.Length; count++)
                {
                    if ((palavras[count].Length < (tamanho - 3)) &&
                        (saida.Length + 1 + palavras[count].Length) < (tamanho - 3))
                    {
                        saida += ((count == (0)) ? string.Empty : " ") + palavras[count];
                    }
                    else
                    {
                        saida += "...";
                        break;
                    }
                }
                return saida;
            }
            return string.Empty;
        }

        public static string GetHtmlStringGoogle(this string valor, int tamanho)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                valor = valor.Replace("<br/>", ".");
                //valor = System.Text.RegularExpressions.Regex.Replace(valor, @"<[^>]*>", "");
                valor = valor.Replace("&nbsp;", " ");

                string saida = string.Empty;
                string[] palavras;
                palavras = valor.Split(' ');
                int count;
                for (count = 0; count < palavras.Length; count++)
                {
                    if ((palavras[count].Length < (tamanho - 3)) &&
                        (saida.Length + 1 + palavras[count].Length) < (tamanho - 3))
                    {
                        saida += ((count == (0)) ? string.Empty : " ") + palavras[count];
                    }
                    else
                    {
                        saida += "...";
                        break;
                    }
                }
                return saida;
            }
            return string.Empty;
        }

        public static string GetString(this string valor, int tamanho)
        {
            string saida = string.Empty;
            if (!string.IsNullOrEmpty(valor))
            {
                string[] palavras;
                palavras = valor.Split(' ');
                int count;
                for (count = 0; count < palavras.Length; count++)
                {
                    if ((palavras[count].Length < (tamanho - 3)) &&
                        (saida.Length + 1 + palavras[count].Length) < (tamanho - 3))
                    {
                        saida += ((count == (0)) ? string.Empty : " ") + palavras[count];
                    }
                    else
                    {
                        saida += "...";
                        break;
                    }
                }
            }
            return saida;
        }

        public static string ToStringSeo(this string valor)
        {
            return valor.ToLower().Trim()
                .Replace("–", "-")
                .Replace(" - ", "-")
                .Replace(" ", "-")
                .Replace("ª", "")
                .Replace("º", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("?", "")
                .Replace("!", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace("|", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("_", "-")
                .Replace("$", "")
                .Replace("#", "")
                .Replace("+", "")
                .Replace("§", "")
                .Replace("@", "")
                .Replace("/", "")
                .Replace("\\", "")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("Ş", "")
                .Replace("ş", "")
                .Replace("°", "")
                .Replace("ă", "a")
                .Replace("á", "a")
                .Replace("ŕ", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ã", "a")
                .Replace("â", "a")
                .Replace("à", "a")
                .Replace("é", "e")
                .Replace("č", "e")
                .Replace("ę", "e")
                .Replace("ë", "e")
                .Replace("ê", "e")
                .Replace("í", "i")
                .Replace("ě", "i")
                .Replace("ď", "i")
                .Replace("ő", "o")
                .Replace("õ", "o")
                .Replace("ó", "o")
                .Replace("ň", "o")
                .Replace("ö", "o")
                .Replace("ô", "o")
                .Replace("ú", "u")
                .Replace("ů", "u")
                .Replace("ü", "u")
                .Replace("ç", "c")
                .Replace("--", "-")
                .Replace("--", "-");
        }
    }

    internal class Text
    {
    }
}

