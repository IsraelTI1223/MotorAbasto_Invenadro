using PortalGRFP.Business.Common.FileProcessor.DAT;
using PortalGRFP.Data.ETL;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalGRFP.Business.ETL
{
    public class LayoutLoader<TModel> : ILoaderLayout
        where TModel : LayoutBase, IDatLayout
    {
        private TipoCargas _tipoCargas;
        private LayoutTypes _layoutTypes;
        private readonly LayoutDataProcessor<TModel> _repository = new LayoutDataProcessor<TModel>();
        public LayoutLoader(TipoCargas tipoCargas, LayoutTypes layoutTypes)
        {
            _tipoCargas = tipoCargas;
            _layoutTypes = layoutTypes;
        }

        public void ProcessData(IEnumerable<IDatLayout> datos, string nombreArchivo, int idUsuario)
        {
            if (datos != null && datos.Count() > 0)
            {
                var muestra = datos.FirstOrDefault();
                var config = _repository.GetLoadConfig((int)muestra.GetTipoCarga());
                if (config != null)
                {
                    if (config.NumeroCargas < config.ArchivosPorDia)
                    {
                        _repository.ProcessData(datos.Select(s => s as TModel), nombreArchivo, idUsuario);
                    }
                    else
                    {
                        throw new Exception($"Solo están permitidos un máximo de {config.ArchivosPorDia} archivos {muestra.GetTipoCarga()} por día, y existen {config.NumeroCargas} cargados. Última carga: {config.FechaUltimaCarga}.");
                    }
                }
                else {
                    throw new Exception($"Está fuera de tiempo para cargar un archivo de tipo: {muestra.GetTipoCarga()}");
                }
            }
            else {
                throw new Exception("Sin datos para procesar");
            }
        }

        public TipoCargas GetLoadLayoutType() => _tipoCargas;

        public LayoutTypes GetLayoutType() => _layoutTypes;
    }
}