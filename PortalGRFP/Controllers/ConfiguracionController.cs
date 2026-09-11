using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts;
using PortalGRFP.Extensions;
using PortalGRFP.Guards;
using PortalGRFP.Models.ETL;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Controllers
{
    [SessionGuard(moduloPadre: 5, 
        errormessage: "El usuario no tiene permisos para entrar al módulo de cargas PMP")]
    public class ConfiguracionController : Controller
    {
        #region [Vistas]      
        [SessionGuard(subModuloHijo: 19, idAccion: Acciones.Consultar)]
        public ActionResult Cargar()
        {
            //permisos en pantalla, para validar el veo o no un boton
            ViewBag.PuedeCargar = this.IsCanActive(5, 19, Acciones.Cargar);
            return View(this.GetHistory().Result);
        }
        #endregion        

        #region [Ajax result]
        [HttpPost]
        [SessionGuard(subModuloHijo: 19, idAccion: Acciones.Consultar,
            errormessage: "El usuario no tiene permiso para consultar el historial de cargas PMP")]
        public JsonResult GetHistoryLoad() => Json(this.GetHistory().Result);

        [HttpPost]
        [SessionGuard(errormessage: "El usuario no tiene permiso para ejecutar una carga de layout PMP",
            subModuloHijo: 19, idAccion: Acciones.Cargar)]
        public JsonResult FileUpload()
        {
            var file = Request.Files[0];
            var response = new FileUploadResult();
            if (file != null && file.ContentLength > 0)
            {
                #region [Guardado del archivo en servidor]
                var sf = file.SaveFile();
                response.Messages.Add(file.FileName);
                #endregion

                #region [Lectura del archivo]
                foreach (var d in sf)
                {
                    var tem = this.ReadDatFile(d.Value);
                    if (tem != null)
                    {
                        response.Messages.AddRange(tem.Messages);
                        if (tem.Result != null)
                        {
                            var error = tem.Result.Select(s => s as LayoutBase).Where(f => f.ErrorMessages.Count > 0).ToList();
                            var ok = tem.Result.Select(s => s as LayoutBase).Where(f => f.ErrorMessages.Count == 0).ToList();
                            response.TableError = error;
                            response.RowsErrorResult = error.Count;
                            response.RowsOKResult = ok.Count;
                            response.TimeProcess = tem.ElapsedMilliseconds;
                            var loadResult = this.LoadDatFile(tem.Result, file.FileName);
                            if (loadResult != null)
                            {
                                response.TimeLoadProcess = loadResult.ElapsedMilliseconds;
                                response.Messages.AddRange(loadResult.Messages);
                            }
                        }
                    }
                }
                #endregion
            }
            return Json(response);
        }
        #endregion
    }
}