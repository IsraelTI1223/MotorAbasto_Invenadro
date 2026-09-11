using System.Text.RegularExpressions;

namespace PortalGRFP.Utilities.Files
{
    public static class CellRegex
    {
        public static bool IsDecimal(string value)
        {
            try
            {
                return Regex.IsMatch(value, @"^-?[0-9]*\.?[0-9]+$");
            }
            catch
            {
                return false;
            }
        }

        public static bool IsBool(string value)
        {
            try
            {
                return value.Equals("0") || value.Equals("1") || value.Equals("FALSO") || value.Equals("VERDADERO") || value.Equals("False") || value.Equals("True");
            }
            catch
            {
                return false;
            }
        }
    }
}
