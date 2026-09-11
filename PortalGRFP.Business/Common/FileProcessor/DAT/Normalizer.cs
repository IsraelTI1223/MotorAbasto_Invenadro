using PortalGRFP.Entities.Models.DatLayouts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace PortalGRFP.Business.Common.FileProcessor.DAT
{
    /// <summary>
    /// Clase estatica que encapsula metodos para procesamiento de archivos .DAT
    /// </summary>
    public static class Normalizer
    {
        /// <summary>
        /// Metodo generico para aplicar el mapeo del layout
        /// </summary>
        /// <typeparam name="TModel">Mapeo</typeparam>
        /// <param name="layout">Lista de cadenas sin mapeo aplicado</param>
        /// <returns>Lista con datos mapeados</returns>
        public static List<IDatLayout> NormalizeLayout<TModel>(IEnumerable<string> layout)
            where TModel : LayoutBase
        {
            object metadata = Activator.CreateInstance(typeof(TModel));
            Dictionary<string, string> info = (metadata as TModel).GetMetadataLayout<TModel>();
            List<IDatLayout> result = new List<IDatLayout>();
            if (layout != null)
            {
                int check = (metadata as TModel).GetCheckSum();
                long itera = 0;
                foreach (string cadena in layout)
                {
                    itera++;
                    object instance = Activator.CreateInstance(typeof(TModel));
                    (instance as TModel).RowNum = itera;
                    if (!cadena.Length.Equals(check))
                    {                     
                        (instance as TModel).ErrorMessages.Add(SetMEssage(itera, cadena.Length, check));                        
                    }
                    foreach (KeyValuePair<string, string> tem in info)
                    {
                        string[] map = tem.Value.Split(':');
                        string valor = "";                                              
                        PropertyInfo prop = instance.GetType().GetProperty(tem.Key);
                        string name = prop.PropertyType.Name;
                        bool isNullable = name.Equals("Nullable`1");
                        name = GetTypeNameByNullable(prop, name);
                        try
                        {
                            valor = cadena.Substring(Int16.Parse(map[0]), Int16.Parse(map[1]));
                        }
                        catch (Exception ex)
                        {
                            (instance as TModel).ErrorMessages.Add($"La fila {itera}: no se pudo extrar el dato [{tem.Key}:({name}), {cadena},({map[0]},{map[1]}) size: {cadena.Length}]");
                            (instance as TModel).ErrorMessages.Add(ex.Message);
                        }
                        switch (name.ToUpper())
                        {
                            case "STRING":
                                prop.SetValue(instance, valor);
                                break;
                            case "DECIMAL":
                                decimal valido;
                                if (Decimal.TryParse(valor.Trim(), out valido))
                                {
                                    prop.SetValue(instance, valido);
                                }
                                else
                                {
                                    (instance as TModel).ErrorMessages.Add(SetMEssage(itera, map, name, tem.Key, valor));
                                }
                                break;
                            case "INT32":
                                TryParseInt32(valor.Trim(), (instance as IDatLayout), prop, itera, map, name, tem.Key, isNullable);
                                break;
                            case "INT64":
                                long pasa2;
                                if (Int64.TryParse(valor.Trim(), out pasa2))
                                {
                                    prop.SetValue(instance, pasa2);
                                }
                                else
                                {
                                    (instance as TModel).ErrorMessages.Add(SetMEssage(itera, map, name, tem.Key, valor));
                                }
                                break;
                            case "DATETIME":
                                DateTime fecha;
                                if (DateTime.TryParseExact(valor.Trim(), map[2],
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out fecha))
                                {
                                    prop.SetValue(instance, fecha);
                                }
                                else
                                {
                                    (instance as TModel).ErrorMessages.Add(SetMEssage(itera, map, name, tem.Key, valor));
                                }
                                break;
                        }
                    }
                    result.Add(instance as TModel);
                }
            }
            return result;
        }

        private static string GetTypeNameByNullable(PropertyInfo prop, string name)
        {
            string nombre = name;
            if (name.Equals("Nullable`1"))
            {
                var full = prop.GetMethod.ReturnType.FullName.Split(',');
                var tem = full[0].Split('.');
                nombre = tem[tem.Length - 1];
            }
            return nombre;
        }
        private static void TryParseInt32(string valor, IDatLayout instance,
            PropertyInfo prop, long itera, string[] map, string name, string key,
            bool isNullable = false)
        {
            int pasa;
            if (Int32.TryParse(valor, out pasa))
            {
                prop.SetValue(instance, pasa);
            }
            else if (!isNullable)
            {
                instance.GetErrorMessages().Add(SetMEssage(itera, map, name, key, valor));
            }
            if (!string.IsNullOrEmpty(valor))
            {
                for (int i = 0; i < valor.Length; i++)
                {
                    if (!Char.IsNumber(valor, i))
                    {
                        instance.GetErrorMessages().Add(SetMEssage(itera, map, "int", key, valor));
                    }
                }
            }
        }
        /// <summary>
        /// Metodo para dar formato al mensaje de error
        /// </summary>
        /// <param name="row">Fila</param>
        /// <param name="oriden">Tamaño de la cadena origen</param>
        /// <param name="destino">Tamaño del mapeo</param>
        /// <returns>Mensaje de error</returns>
        private static string SetMEssage(long row, int oriden, int destino)
            => $"La fila: {row}, no cumple con el tamaño indicado en el Layout(mapeo) [cadena : {oriden}, mapeo : {destino}]";
        /// <summary>
        /// Metodo para dar formato al mensaje de error
        /// </summary>
        /// <param name="row">Fila</param>
        /// <param name="map">Mapeo</param>
        /// <param name="destino">Nombre del campo</param>
        /// <param name="tipo">Tipo de dato</param>
        /// <param name="valor">Valor</param>
        /// <returns>Mensaje de error</returns>
        private static string SetMEssage(long row, string[] map, string destino, string tipo, string valor)
           => $"La fila: {row}, contiene un error de mapeo({(tipo.Equals("DateTime") ? string.Join(":", map) : $"{map[0]}:{map[1]}")}) => [{destino}, {tipo}, {valor}]";
    }
}