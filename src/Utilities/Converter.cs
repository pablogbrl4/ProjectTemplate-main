using System;

namespace Orizon.Rest.Chat.Utilities
{
    public static class Converter
    {
        public static DateTime? ConverterToDateTime(string value)
        {
            DateTime dtAux;
            DateTime.TryParse(value, out dtAux);
            if (dtAux == DateTime.MinValue)
                return null;
            return dtAux;
        }
    }
}
