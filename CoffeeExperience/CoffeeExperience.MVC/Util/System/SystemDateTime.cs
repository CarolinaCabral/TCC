namespace System
{
    public static class SystemDateTime
    {
        public static DateTime ToDataHoraInicial(this DateTime valor)
        {
            return new DateTime(valor.Year, valor.Month, valor.Day, 0, 0, 0);
        }

        public static DateTime ToDataHoraFinal(this DateTime valor)
        {
            return new DateTime(valor.Year, valor.Month, valor.Day, 23, 59, 59);
        }
    }
}
