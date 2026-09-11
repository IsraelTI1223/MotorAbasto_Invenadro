using PortalGRFP.Business.Perfiles;
using PortalGRFP.Entities.Common;
using PortalGRFP.Data.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Response;
using PortalGRFP.Entities.Parameters;
using PortalGRFP.Utilities.TableType;
using System.Data;
using PortalGRFP.Entities.Models;
using PortalGRFP.Data.Modulos;

namespace PortalGRFP.Business.Perfiles
{
    public class PerfilBusiness
    {
        private readonly PerfilData perfilData;
        private string arbolModuloAccionesBase = "";
        private string tablaModuloAccionesBase = "";

        public PerfilBusiness()
        {
            this.perfilData = new PerfilData();
        }

        public ResponseList<PortalGRFP.Entities.Common.Perfil> GetAllPerfil(int Activo)
        {
            var response = new ResponseList<PortalGRFP.Entities.Common.Perfil>();
            try
            {
                response = perfilData.GetAllPerfil(Activo);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<PortalGRFP.Entities.Common.Perfil> GetPerfilById(int PerfilId)
        {
            var response = new ResponseList<PortalGRFP.Entities.Common.Perfil>();
            try
            {
                response = perfilData.GetPerfilById(PerfilId);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response InsertCTRLPERFIL(string nombrePerfil, int IdUSuario, List<string> moduloPermisos)
        {
            var response = new Response();
            try
            {
                var request = new PerfilParameter();

                request.Nombre = nombrePerfil;
                request.IdUsuario = IdUSuario;
                //request.Modulos = CargaModulos(moduloPermisos);
                request.Modulos = CargaModulosAccion(moduloPermisos);

                response = perfilData.InsertCTRLPERFIL(request);
                if (response.Message != null)
                {
                    if (response.Message.Trim().Length > 0)
                    {
                        response.Message = "Ya existe un perfil con el mismo nombre.";
                    }
                }
                else
                {
                    response.Message = response.Success ? "El perfil se registró correctamente." : "No se pudo registrar el perfil.";
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al registrar perfil." + ex.Message;
            }
            return response;
        }

        private DataTable CargaModulos(List<string> moduloPermisos)
        {
            var dt = ModuloType.GetDefinition();
            try
            {
                if (moduloPermisos.Count() > 0)
                {
                    foreach (string modulo in moduloPermisos)
                    {
                        if (modulo.Trim().Length > 0)
                        {
                            string[] items = modulo.Split(',');
                            dt.Rows.Add(
                                int.Parse(items[0].ToString()), //id de modulo
                                int.Parse(items[1].ToString()), //Actualizar
                                int.Parse(items[2].ToString()), //Consultar
                                int.Parse(items[3].ToString()), //Cargar
                                int.Parse(items[4].ToString()), //Eliminar
                                int.Parse(items[5].ToString()), //Insertar
                                int.Parse(items[6].ToString()) //Descargar
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
            }
            return dt;
        }

        private DataTable CargaModulosAccion(List<string> moduloAccion)
        {
            var dt = ModuloAccionType.GetDefinition();
            try
            {
                if (moduloAccion.Count() > 0)
                {
                    foreach (string modulo in moduloAccion)
                    {
                        if (modulo.Trim().Length > 0)
                        {
                            string[] items = modulo.Split('-');
                            dt.Rows.Add(
                                int.Parse(items[0].ToString()), //id de modulo
                                int.Parse(items[1].ToString()) //id de accion
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        // Módulos por perfil
        public ResponseList<CtrlPerfil> GetAllCtrlPerfilMod(int IdPerfil)
        {
            var response = new ResponseList<CtrlPerfil>();
            try
            {
                response = perfilData.GetAllCtrlPerfilMod(IdPerfil);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información." + ex.Message;
            }
            return response;
        }

        public Response UpdateCTRLPERFIL(int IdPerfil, bool Activo, string nombrePerfil, int IdUSuario, List<string> moduloPermisos)
        {
            var response = new Response();
            try
            {
                var request = new PerfilParameter();

                request.IdPerfil = IdPerfil;
                request.Activo = Activo;
                request.Nombre = nombrePerfil;
                request.IdUsuario = IdUSuario;
                //request.Modulos = CargaModulos(moduloPermisos);
                request.Modulos = CargaModulosAccion(moduloPermisos);

                response = perfilData.UpdateCTRLPERFIL(request);
                response.Message = response.Success ? "El perfil se actualizó correctamente." : "No se pudo actualizar el perfil.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar perfil." + ex.Message;
            }
            return response;
        }

        public ResponseList<PerfilModuloModel> PerfilModulos(int IdPerfil, ref string arbolModuloAcciones, ref string _tablaModuloAccionesBase)
        {
            arbolModuloAccionesBase = "";
            tablaModuloAccionesBase = "";
            var response = new ResponseList<PerfilModuloModel>();
            ModuloData moduloData = new ModuloData();
            List<Entities.Common.Modulo> lstModulo = moduloData.GetList().Result.ToList();
            try
            {
                if (lstModulo.Count() > 0)
                {
                    tablaModuloAccionesBase = "<table class=\"table table-striped\" id=\"tblModulos\">";
                    tablaModuloAccionesBase += "<thead class=\"thead-primary\">";
                    tablaModuloAccionesBase += "<tr>";
                    tablaModuloAccionesBase += "<th></th>";
                    
                    
                    arbolModuloAccionesBase = "<div class=\"well\" id=\"treeview_div\">";
                    arbolModuloAccionesBase += "<ul>";
                    arbolModuloAccionesBase += "<li>OPCIONES DE SISTEMA";
                    arbolModuloAccionesBase += "<ul>";
                    List<PerfilModuloAccion> lstPerfilModuloAccion = perfilData.GetPerfilModuloAccion(IdPerfil == 0 ? 1 : 2, IdPerfil);
                    List<PerfilModuloModel> lstPerfilModuloModel = ModuloRecursivo(lstModulo, lstPerfilModuloAccion, 0);

                    // encabezados tabla
                    tablaModuloAccionesBase += "<th>ID</th>";
                    tablaModuloAccionesBase += "<th>Modulo</th>";
                    List<PerfilModuloAccionModel> lstPerfilModuloAccionModel = lstPerfilModuloModel.FirstOrDefault().Acciones.ToList();

                    // fontawesome de encabezado
                    string strimagen = "fas fa-pen";
                    
                    foreach (var itemEncabzadoTabla in lstPerfilModuloAccionModel)
                    {
                        switch (itemEncabzadoTabla.Nombre.ToLower())
                        {
                            case "actualizar":
                                strimagen = "fas fa-pencil-alt";
                                break;
                            case "consultar":
                                strimagen = "fas fa-search";
                                break;
                            case "cargar":
                                strimagen = "fas fa-upload";
                                break;
                            case "eliminar":
                                strimagen = "fas fa-trash-alt";
                                break;
                            case "registrar":
                                strimagen = "fas fa-plus-circle";
                                break;
                            case "descargar":
                                strimagen = "fas fa-download";
                                break;
                        }

                        tablaModuloAccionesBase += "<th scope=\"col\"><i class='" + strimagen + "' style='color:white'>  " + itemEncabzadoTabla.Nombre + "</th>";
                    }

                    tablaModuloAccionesBase += "</tr>";
                    tablaModuloAccionesBase += "</thead>";
                    tablaModuloAccionesBase += "<tbody>";

                    //tablaModuloAccionesBase += "<tr ContieneModulo=\"0\">";
                    //tablaModuloAccionesBase += "";
                    //tablaModuloAccionesBase += "";
                    //tablaModuloAccionesBase += "";
                    //tablaModuloAccionesBase += "";

                    ArmaHtml(lstPerfilModuloModel);

                    tablaModuloAccionesBase += "</tbody>";
                    tablaModuloAccionesBase += "</table>";

                    arbolModuloAccionesBase += "</ul>";
                    arbolModuloAccionesBase += "</li>";
                    arbolModuloAccionesBase += "</ul>";
                    arbolModuloAccionesBase += "</div>";

                    
                    _tablaModuloAccionesBase = tablaModuloAccionesBase;


                    arbolModuloAcciones = arbolModuloAccionesBase;
                    response.Success = true;
                    response.Result = lstPerfilModuloModel;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public List<PerfilModuloModel> ModuloRecursivo(List<Entities.Common.Modulo> modulos, List<PerfilModuloAccion> lstPerfilModuloAccion, int? ModuloIdPadre = null)
        {
            List<PerfilModuloModel> modulosR = modulos.Where(x => x.IdPadre.Equals(ModuloIdPadre)).Select(item => new PerfilModuloModel
            {
                IdModulo = item.IdModulo,
                Nombre = item.NombreModulo,
                Estatus = 1,
                ModuloPadre= item,
                Acciones = lstPerfilModuloAccion.Where(pma => pma.IdModulo == item.IdModulo).Select(pmaitem => new PerfilModuloAccionModel { IdAccion = pmaitem.IdAccion, Nombre = pmaitem.Nombre, Estatus = pmaitem.EstatusAccion } ).ToList(),
                Hijos = ModuloRecursivo(modulos, lstPerfilModuloAccion, item.IdModulo)
            }).ToList();
            return modulosR;
        }

        public void ArmaHtml(List<PerfilModuloModel> lstPerfilModuloModel)
        {
            foreach (PerfilModuloModel item in lstPerfilModuloModel)
            {
                //if (item.Hijos.Count() > 0)
                //{ 
                //}
                int numeroAcciones = item.Acciones.Count();
                int numeroColdspan = item.Acciones.Count() + 2;
                if (item.Hijos.Count() > 0) 
                {
                    // con o sin hijos <td style="text-align:center"><img src="~/Content/img/add.png" style="cursor:pointer" IdModulo="@item.IdModulo" /></td>
                    tablaModuloAccionesBase += "<tr ContieneModulo=\"1\">";

                    tablaModuloAccionesBase += "<td style=\"text-align:center\"><img src=\"/Content/img/add.png\" style=\"cursor:pointer\" IdModulo=\"" + item.IdModulo + "\" /></td>";
                    tablaModuloAccionesBase += "<td>" + item.IdModulo + "</td>";
                    tablaModuloAccionesBase += "<td>" + item.Nombre + "</td>";
                    // los sig td dependen del numero de acciones
                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td></td>";
                    for (int itemTd = 1; itemTd <= numeroAcciones; itemTd++)
                    {
                        tablaModuloAccionesBase += "<td></td>";
                    }

                    tablaModuloAccionesBase += "</tr>";
                }

                if (item.Hijos.Count() > 0)
                {
                    tablaModuloAccionesBase += "<tr id=\"fila-" + item.IdModulo + "\"" + " ContieneModulo=\"0\" style=\"display:none; padding: 0px; margin: 0px\">";
                    tablaModuloAccionesBase += "<td></td>";
                    tablaModuloAccionesBase += "<td colspan=\"" + numeroColdspan + "\">"; // el coldpspan debe ser dinamico x numero de acciones

                    // tabla del hijo
                    tablaModuloAccionesBase += "<table class=\"table table-striped\" id=\"tblModulosHijo\">";
                    tablaModuloAccionesBase += "<thead class=\"thead-primary\">";
                    tablaModuloAccionesBase += "<tr>";

                    tablaModuloAccionesBase += "</tr>";
                    tablaModuloAccionesBase += "</thead>";
                    tablaModuloAccionesBase += "<tbody>";

                    tablaModuloAccionesBase += "<tr ContieneModulo=\"1\">";

                }

                if (item.Hijos.Count() > 0)
                {
                    arbolModuloAccionesBase += "<li>" + item.Nombre;
                    arbolModuloAccionesBase += "<ul>";

                    //tablaModuloAccionesBase += "</tr>";

                    //tablaModuloAccionesBase += "<table class=\"table table-striped\" id=\"tblModulosHijo\">";
                    //tablaModuloAccionesBase += "<thead class=\"thead-primary\">";
                    //tablaModuloAccionesBase += "<tr>";

                    //tablaModuloAccionesBase += "</tr>";
                    //tablaModuloAccionesBase += "</thead>";
                    //tablaModuloAccionesBase += "<tbody>";

                    //tablaModuloAccionesBase += "<tr ContieneModulo=\"1\">";

                    //tablaModuloAccionesBase += "<td></td>";
                    //tablaModuloAccionesBase += "<td>" + item.IdModulo + "</td>";
                    //tablaModuloAccionesBase += "<td>" + item.Nombre + "</td>";

                    ////nodos opciones del padre
                    ////cierra opciones del padre
                    //tablaModuloAccionesBase += "</tr>";

                    ArmaHtml(item.Hijos);

                    //tablaModuloAccionesBase += "</tr>";

                    //tablaModuloAccionesBase += "</tbody>";
                    //tablaModuloAccionesBase += "</table>";

                    arbolModuloAccionesBase += "</ul>";
                    arbolModuloAccionesBase += "</li>";
                }
                else
                {
                    arbolModuloAccionesBase += "<li>" + item.Nombre + "    ";

                    tablaModuloAccionesBase += "<tr>";
                    tablaModuloAccionesBase += "<td></td>";
                    tablaModuloAccionesBase += "<td>" + item.IdModulo + "</td>";
                    tablaModuloAccionesBase += "<td>" + item.Nombre + "</td>";

                    foreach (var itemAccion in item.Acciones)
                    {
                        arbolModuloAccionesBase += "  " + itemAccion.Nombre + "  " + "<input type=checkbox name=color " + (itemAccion.Estatus == 1 ? "checked=true" : "") + " value=" + item.IdModulo + "-" + itemAccion.IdAccion + ">";
                        string strimagen = "fas fa-pen";
                        switch (itemAccion.Nombre.ToLower())
                        {
                            case "actualizar":
                                strimagen = "fas fa-pencil-alt";
                                break;
                            case "consultar":
                                strimagen = "fas fa-search";
                                break;
                            case "cargar":
                                strimagen = "fas fa-upload";
                                break;
                            case "eliminar":
                                strimagen = "fas fa-trash-alt";
                                break;
                            case "registrar":
                                strimagen = "fas fa-plus-circle";
                                break;
                            case "descargar":
                                strimagen = "fas fa-download";
                                break;
                        }
                        tablaModuloAccionesBase += "<td><i class='" + strimagen + "' style='color:blue'></i>   <input type=checkbox name=\"color\"" + (itemAccion.Estatus == 1 ? "checked=true" : "") + " value=" + item.IdModulo + "-" + itemAccion.IdAccion + "></td>";
                    }
                    tablaModuloAccionesBase += "</tr>";
                    arbolModuloAccionesBase += "</li>";
                }

                if (item.Hijos.Count() > 0)
                {
                    // tabla del hijo
                    tablaModuloAccionesBase += "</tr>";
                    tablaModuloAccionesBase += "</tbody>";
                    tablaModuloAccionesBase += "</table>";
                    // tabla del hijo

                    tablaModuloAccionesBase += "</td>";
                    tablaModuloAccionesBase += "</tr>";
                }
            }
        }

        public List<PerfilModuloAccion> GetPerfilModuloAccion(int TipoOperacion, int IdPerfil)
        {
            try
            {
                return perfilData.GetPerfilModuloAccion(TipoOperacion, IdPerfil);
            }
            catch (Exception ex)
            {
                return new List<PerfilModuloAccion>{ };
            }
        }

    }
}
