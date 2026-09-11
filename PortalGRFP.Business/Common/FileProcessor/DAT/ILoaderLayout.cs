using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts;
using System.Collections.Generic;

namespace PortalGRFP.Business.Common.FileProcessor.DAT
{
    public interface ILoaderLayout
    {
        TipoCargas GetLoadLayoutType();
        LayoutTypes GetLayoutType();
        void ProcessData(IEnumerable<IDatLayout> datos, string nombreArchivo, int idUsuario);            
    }
}