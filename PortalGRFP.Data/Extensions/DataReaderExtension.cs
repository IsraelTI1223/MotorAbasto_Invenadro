namespace PortalGRFP.Data.Extensions
{
    using PortalGRFP.Entities.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;

    public static class DataReaderExtension
    {
        public static List<T> Reader<T>(this IDataReader reader, Func<IDataReader, T> projection)
        {
            var result = new List<T>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    result.Add(projection(reader));
                }
                reader.Close();
            }

            return result;
        }

        public static T Get<T>(this IDataReader reader, string name)
        {
            var convert = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                if (reader != null && convert != null && reader.GetOrdinal(name) >= default(int))
                {
                    return (T)convert.ConvertFromString(reader[name].ToString());
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

    }
}
