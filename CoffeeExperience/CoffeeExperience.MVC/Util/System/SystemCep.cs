namespace System
{
    public static class SystemCep
    {
        public static string ToCepFormatado(this string cep)
        {
            return cep.PadLeft(8, '0');
        }
    }
}
