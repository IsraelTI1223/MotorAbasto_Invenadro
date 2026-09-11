using OfficeOpenXml;
using PortalGRFP.Data.ComprasEspeciales;
using PortalGRFP.Entities;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using PortalGRFP.Utilities.TableType;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static PortalGRFP.Entities.Common.ListSucursalesGeneralesSugeridoValidarModel;

namespace PortalGRFP.Business.ComprasEspeciales
{
    public class SugeridoBusiness
    {
        private readonly SugeridoData sugeridovar;
        public Int64 NuevoFolio { get; set; }
        public SugeridoBusiness()
        {
            sugeridovar = new SugeridoData();
        }
        public ResponseList<ComboGenerico> Filtros(int accion)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = sugeridovar.Filtros(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<Combo4> FiltrosInvenadro(int accion,int user)
        {
            var response = new ResponseList<Combo4>();
            try
            {
                response = sugeridovar.FiltrosInvenadro(accion,user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<Combo4> FiltrosInvenadro2(int accion, int user)
        {
            var response = new ResponseList<Combo4>();
            try
            {
                response = sugeridovar.FiltrosInvenadro2(accion, user);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ConsultaInvenadro> GetProductoInvenadroListBLL(string Sucursal, string SKU,string Concepto)
        {
            var response = new ResponseList<ConsultaInvenadro>();
            try
            {
                response = sugeridovar.GetProductoInvenadroDatBLL(Sucursal, SKU, Concepto);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public ResponseList<Combo3> Filtros2(int accion)
        {
            var response = new ResponseList<Combo3>();
            try
            {
                response = sugeridovar.Filtros2(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public Response CargaSugerido(HttpFileCollectionBase httpFile, string fecha, int usuario)
        {
            var response = new Response();
            if (httpFile.AllKeys.Any())
            {
                var archivo = httpFile["fileExcel"];
                if (archivo != null && archivo.ContentLength > 0)
                {
                    try
                    {
                        var nombreArchivo = Path.GetFileName(archivo.FileName);
                        string fileContentType = archivo.ContentType;
                        byte[] fileBytes = new byte[archivo.ContentLength];
                        var data = archivo.InputStream.Read(fileBytes, 0, Convert.ToInt32(archivo.ContentLength));

                        var package = new ExcelPackage(archivo.InputStream);

                        var hojaActual = package.Workbook.Worksheets["sugerido"];
                        if (hojaActual == null)
                        {
                            response.Message = "Para leer el archivo, el nombre de la hoja debe ser 'sugerido'";
                            return response;
                        }
                        var hoja = hojaActual;
                        int noCol = hoja.Dimension.End.Column;
                        int noRenglon = hoja.Dimension.End.Row;
                        int i = 0;

                        List<ComprasEspecialesBean> listaEnExcel = new List<ComprasEspecialesBean>();

                        string fmto = string.Empty;
                        string tipoPedido = string.Empty;

                        for (i = 2; i <= noRenglon; i++)
                        {
                            ComprasEspecialesBean cvp = new ComprasEspecialesBean();

                            cvp.TipoPedido = hoja.Cells[i, 1].Value == null ? 0 : int.Parse(hoja.Cells[i, 1].Value.ToString());//tipoPedido,
                            cvp.TipoProveedor = hoja.Cells[i, 2].Value == null ? 0 : int.Parse(hoja.Cells[i, 2].Value.ToString());//TipoProveedor,
                            cvp.FechaGenOC = Convert.ToDateTime(fecha);
                            cvp.Suc_Id = hoja.Cells[i, 3].Value == null ? 0 : Int64.Parse(hoja.Cells[i, 3].Value.ToString());
                            cvp.Sku = hoja.Cells[i, 4].Value == null ? "" : hoja.Cells[i, 4].Value.ToString();
                            cvp.Cantidad = hoja.Cells[i, 5].Value == null ? 0 : int.Parse(hoja.Cells[i, 5].Value.ToString());
                                
                            decimal m;
                            var mto = hoja.Cells[i, 6].Value.ToString();
                            var mto2 = decimal.TryParse(mto, out m);
                            if (mto2)
                            {
                                if (decimal.Parse(mto) <= 0 && cvp.TipoPedido == 15)
                                    fmto += string.Format("{0}, ", i);
                                else
                                    cvp.Costo = hoja.Cells[i, 6].Value == null ? 0 : decimal.Parse(hoja.Cells[i, 6].Value.ToString());
                            }
                            else
                            {
                                fmto += string.Format("{0},", i);
                            }

                            if(cvp.TipoPedido==15)
                                tipoPedido += string.Format("{0},", i);

                            listaEnExcel.Add(cvp);
                        }

                        var consulta = listaEnExcel.Where(x => x.TipoPedido == 15).Count();

                        if (!string.IsNullOrEmpty(fmto) || (consulta>0 && consulta<listaEnExcel.Count))
                        {
                            response.Message = "Hay errores en el archivo";

                            if (!string.IsNullOrEmpty(fmto))
                            {
                                fmto = fmto.Remove(fmto.Length - 2);
                                response.Message += "\n\rEl valor del Costo no es valido deb ser mayor a 0\n\rFila Error:" + fmto;
                            }

                            if((consulta > 0 && consulta < listaEnExcel.Count))
                            {
                                tipoPedido = tipoPedido.Remove(tipoPedido.Length - 1);
                                response.Message += "\n\rHay pedido a pie de camión con otros tipos de pedidos\n\rFila Error:" + tipoPedido;
                            }
                            return response;
                        }



                        var dt = CommprasEspecialesType.Definicion();
                        var numLineas = listaEnExcel.Count();
                        foreach (var item in listaEnExcel)
                        {
                            DataRow renglon = dt.NewRow();
                            renglon[0] = item.TipoPedido;
                            renglon[1] = item.TipoProveedor;
                            renglon[2] = item.FechaGenOC;
                            renglon[3] = item.Suc_Id;
                            renglon[4] = item.Sku;
                            renglon[5] = item.Cantidad;
                            renglon[6] = numLineas;
                            renglon[7] = nombreArchivo;
                            renglon[8] = item.Costo;
                            renglon[9] = usuario;
                            dt.Rows.Add(renglon);
                        }

                        response = sugeridovar.CargarSugerido(dt);
                        response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
                    }
                    catch (Exception ex)
                    {
                        response.Message = "Error al realizar la consulta de información. " + ex.Message;
                        if (ex.Message.Contains("Violation of PRIMARY KEY"))
                        {
                            response.Message = "Ya existe compra especial del mismo tipo, mismo proveedor en la misma fecha";
                        }

                    }
                }
                else
                {
                    response.Message = "No se ha seleccionado un archivo";
                }
            }
            else
            {
                response.Message = "No se ha seleccionado un archivo";
            }
            return response;
        }
        //public Response CargaSugerido(HttpFileCollectionBase httpFile, int tipoPedido, int TipoProveedor, string fecha, int usuario)
        //{
        //    var response = new Response();
        //    if (httpFile.AllKeys.Any())
        //    {
        //        var archivo = httpFile["fileExcel"];
        //        if (archivo != null && archivo.ContentLength > 0)
        //        {
        //            try
        //            {
        //                var nombreArchivo = Path.GetFileName(archivo.FileName);
        //                string fileContentType = archivo.ContentType;
        //                byte[] fileBytes = new byte[archivo.ContentLength];
        //                var data = archivo.InputStream.Read(fileBytes, 0, Convert.ToInt32(archivo.ContentLength));

        //                var package = new ExcelPackage(archivo.InputStream);

        //                var hojaActual = package.Workbook.Worksheets["sugerido"];
        //                if (hojaActual == null)
        //                {
        //                    response.Message = "Para leer el archivo, el nombre de la hoja debe ser 'sugerido'";
        //                    return response;
        //                }
        //                var hoja = hojaActual;
        //                int noCol = hoja.Dimension.End.Column;
        //                int noRenglon = hoja.Dimension.End.Row;
        //                int i = 0;

        //                List<ComprasEspecialesBean> listaEnExcel = new List<ComprasEspecialesBean>();

        //                for (i = 2; i <= noRenglon; i++)
        //                {
        //                    ComprasEspecialesBean cvp = new ComprasEspecialesBean()
        //                    {
        //                        TipoPedido = tipoPedido,
        //                        TipoProveedor = TipoProveedor,
        //                        FechaGenOC = Convert.ToDateTime(fecha),
        //                        Suc_Id = hoja.Cells[i, 1].Value == null ? 0 : Int64.Parse(hoja.Cells[i, 1].Value.ToString()),
        //                        Sku = hoja.Cells[i, 2].Value == null ? "" : hoja.Cells[i, 2].Value.ToString(),
        //                        Cantidad = hoja.Cells[i, 3].Value == null ? 0 : int.Parse(hoja.Cells[i, 3].Value.ToString()),
        //                        Costo = hoja.Cells[i, 4].Value == null ? 0 : decimal.Parse(hoja.Cells[i, 4].Value.ToString()),


        //                    };
        //                    listaEnExcel.Add(cvp);
        //                }


        //                var dt = CommprasEspecialesType.Definicion();
        //                var numLineas = listaEnExcel.Count();
        //                foreach (var item in listaEnExcel)
        //                {
        //                    DataRow renglon = dt.NewRow();
        //                    renglon[0] = item.TipoPedido;
        //                    renglon[1] = item.TipoProveedor;
        //                    renglon[2] = item.FechaGenOC;
        //                    renglon[3] = item.Suc_Id;
        //                    renglon[4] = item.Sku;
        //                    renglon[5] = item.Cantidad;
        //                    renglon[6] = numLineas;
        //                    renglon[7] = nombreArchivo;
        //                    renglon[8] = item.Costo;
        //                    renglon[9] = usuario;
        //                    dt.Rows.Add(renglon);
        //                }

        //                response = sugeridovar.CargarSugerido(dt);
        //                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
        //            }
        //            catch (Exception ex)
        //            {
        //                response.Message = "Error al realizar la consulta de información. " + ex.Message;
        //                if (ex.Message.Contains("Violation of PRIMARY KEY"))
        //                {
        //                    response.Message = "Ya existe compra especial del mismo tipo, mismo proveedor en la misma fecha";
        //                }

        //            }
        //        }
        //        else
        //        {
        //            response.Message = "No se ha seleccionado un archivo";
        //        }
        //    }
        //    else
        //    {
        //        response.Message = "No se ha seleccionado un archivo";
        //    }
        //    return response;
        //}

        public ResponseList<ComprasEspecialesCargas> ConsultaPedidosCargados(Int64 folio, int user, string fecha)
        {
            var response = new ResponseList<ComprasEspecialesCargas>();
            try
            {
                ComprasEspecialesCargas cargas = new ComprasEspecialesCargas();
                var pedidos = sugeridovar.ConsultaPedidosCargados(folio, user, fecha);
                cargas.PedidosCargardosB = pedidos.Result;


                var rechazados = sugeridovar.ConsultaPedidosRechazado(folio, user, fecha);
                cargas.RechazadosB = rechazados.Result;

                List<ComprasEspecialesCargas> especialesCargas = new List<ComprasEspecialesCargas>();
                especialesCargas.Add(cargas);
                response.Result = especialesCargas;
                if (pedidos.Result.Count == 0)
                {
                    response.Message = "No hay Registros";
                }
                response.Success = pedidos.Result.Any();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<PedidosCargados> BorrarPedido(Int64 folio, int user, string fecha)
        {
            var response = new ResponseList<PedidosCargados>();
            try
            {
                response = sugeridovar.BorrarPedido(folio, user, fecha);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ComboGenerico> GeneralesSugeridoFiltros(int accion)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = sugeridovar.Filtros(accion);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response CrearSugerido(List<ValidaSugeridoBean> sugeridoBeans, int usuario)
        {
            var response = new Response();
            try
            {
                var dt = ValidaSugeridoType.Definicion();
                foreach (var item in sugeridoBeans)
                {
                    DataRow renglon = dt.NewRow();
                    renglon[0] = item.IdGrupo;
                    renglon[1] = item.IdSucursal;
                    renglon[2] = item.IdProveedor;
                    renglon[3] = item.Negados;
                    renglon[4] = item.CompraEspecial;
                    renglon[5] = item.ResurtidoNatural;
                    renglon[6] = item.Invenadro;
                    renglon[7] = item.EspecialesFarmacia;
                    renglon[8] = item.PieCamionMayorista;
                    renglon[9] = usuario;
                    dt.Rows.Add(renglon);

                }
                response = sugeridovar.CrearSugerido(dt);

                if(response.Message == "0")
                {
                    response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
                    this.NuevoFolio = sugeridovar.NuevoFolioSugerido;
                } 
                if(response.Message == "1")
                {
                    response.Message = response.Success ? "Algunas sucursales no tienen cuenta con los proveedores seleccionados." : "No se pudo completar la operación";
                    this.NuevoFolio = sugeridovar.NuevoFolioSugerido;
                }
                
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public Response ValidarNegadosBLL(ListSucursales model, long folioSugerido)
        {
            var response = new Response();
            try
            {
                response = sugeridovar.ValidarNegadosData(model, folioSugerido);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la petición. " + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public ResponseList<ComboGenerico> GetIncidenciasListBLL(int accion, string tiendas)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = sugeridovar.GetIncidenciasListData(accion, tiendas);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<SugeridoLogErrorModel> GetListLogErrorBLL(string incidencia, int accion, string tiendas)
        {
            var response = new ResponseList<SugeridoLogErrorModel>();
            try
            {
                response = sugeridovar.GetListLogErrorData(incidencia, accion, tiendas);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        public ResponseList<SugeridoLogErrorModel> GetDTLogErrorBLL(int accion, string tiendas, int usuario)
        {
            var response = new ResponseList<SugeridoLogErrorModel>();
            try
            {
                response = sugeridovar.GetDTLogErrorData(accion, tiendas, usuario);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<SugeridoCuenta> GetDTCuenta(Int64 NuevoFolio)
        {
            var response = new ResponseList<SugeridoCuenta>();
            try
            {
                response = sugeridovar.GetDTCuenta(NuevoFolio);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ReporteNegados> GetDTReporteNegados(string Sucursales,string fechaini, string fechafin)
        {
            var response = new ResponseList<ReporteNegados>();
            try
            {
                response = sugeridovar.GetDTReporteNegado(Sucursales, fechaini, fechafin);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response CalcularSugerido(Int64 Folio)
        {
            var response = new Response();
            try
            {
                response = sugeridovar.CalcularSugerido(Folio);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response AsignacionProveedor(Int64 Folio)
        {
            var response = new Response();
            try
            {
                response = sugeridovar.AsignacionProveedor(Folio);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response EnviarSugeridoBLL(int user, string fecha)
        {
            var response = new Response();
            try
            {
                response = sugeridovar.EnviarSugeridoData(user, fecha);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ConsultaSugerido> GetListadoSugerido(string idsugerido, string fcreacion, string sucs, string provs, string tipocalculo, string estatus,
                                                                string grupoProducto, string tipoPedido, int consultar, int usuario)
        {
            var response = new ResponseList<ConsultaSugerido>();
            try
            {
                ConsultaSugerido consulta = new ConsultaSugerido();
                var detalle = sugeridovar.GetListadoSugerido(idsugerido, fcreacion, sucs, provs, tipocalculo, estatus, grupoProducto, tipoPedido, consultar, usuario);
                consulta.Listado = detalle.Result;

                var montos = sugeridovar.GetMontosSugerido(idsugerido, fcreacion, sucs, provs, tipocalculo, estatus, grupoProducto, tipoPedido, consultar, usuario);
                consulta.Montos = montos.Result;

                List<ConsultaSugerido> beans = new List<ConsultaSugerido>();
                beans.Add(consulta);
                response.Result = beans;

                if (response.Result.Count == 0)
                {
                    response.Message = "No hay Registros";
                }
                response.Success = response.Result.Any();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ComboGenerico> ObtenerListaSucursales(Int64 idSugerido)
        {
            var response = new ResponseList<ComboGenerico>();
            try
            {
                response = sugeridovar.GetListaSucursales(idSugerido);
            }
            catch (Exception ex)
            {
                //response.Message = "Error al realizar la consulta de información. " + ex.Message;
                response.Message = "Ocurrió un error al procesar la informacion";
            }
            return response;
        }

        public ResponseList<ResumenEnt> GetResumen(Int64 idSugerido, int accion)
        {
            var response = new ResponseList<ResumenEnt>();
            try
            {
                response = sugeridovar.GetResumen(idSugerido, accion);

            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response ActualizarPedidoFinal(string idSugerido, string farmacia, string proveedor, string sku, string pedFinal, string PedModificado)
        {
            var response = new Response();
            try
            {
                response = sugeridovar.ActualizarPedidoFinal(idSugerido, farmacia, proveedor, sku, pedFinal, PedModificado);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response EliminarSkuSugerido(List<EliminarSugeridoSKU> model, int usuario)
        {
            var response = new Response();
            try
            {
                var dt = EliminarSugeridoType.Definicion();
                foreach (var item in model)
                {
                    DataRow renglon = dt.NewRow();
                    renglon[0] = item.IdSugerido;
                    renglon[1] = item.Farmacia;
                    renglon[2] = item.Proveedor;
                    renglon[3] = item.SKU;
                    renglon[4] = usuario;
                    dt.Rows.Add(renglon);

                }
                response = sugeridovar.EliminarSkuSugerido(dt);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<ResumenRechazos> GetRechazos(Int64 idSugerido)
        {
            var response = new ResponseList<ResumenRechazos>();
            try
            {
                response = sugeridovar.GetRechazos(idSugerido);

            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public byte[] DescargarExcel(string idsugerido, string fcreacion, string sucs, string provs, string tipocalculo, string estatus,
                                                                string grupoProducto, string tipoPedido, int consultar, int usuario)
        {
            

            var response = new ResponseList<ListadoSugerido>();
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Conciliación");
            byte[] xlsx = pck.GetAsByteArray();
            try
            {
                response = sugeridovar.GetListadoSugerido(idsugerido, fcreacion, sucs, provs, tipocalculo, estatus, grupoProducto, tipoPedido, consultar, usuario); 
                pck = new ExcelPackage();
                ws = pck.Workbook.Worksheets.Add("Consulta");
                ws.Cells["A1"].Value = "Fecha Creación Sugerido";
                ws.Cells["B1"].Value = "Id Sugerido";
                ws.Cells["C1"].Value = "Id Sucursal";
                ws.Cells["D1"].Value = "Sucursal";
                ws.Cells["E1"].Value = "Proveedor";
                ws.Cells["F1"].Value = "Articulo_Id";
                ws.Cells["G1"].Value = "Producto";
                ws.Cells["H1"].Value = "Costo Neto";
                ws.Cells["I1"].Value = "Tipo Pedido";
                ws.Cells["J1"].Value = "Existencia Pza";
                ws.Cells["K1"].Value = "Transito Pza";
                ws.Cells["L1"].Value = "Sugerido Inicial";
                ws.Cells["M1"].Value = "Negados Pza";
                ws.Cells["N1"].Value = "Compra Especial Pza";
                ws.Cells["O1"].Value = "Invenadro Pza";
                ws.Cells["P1"].Value = "Capacity";
                ws.Cells["Q1"].Value = "Ped Esp Farmacia";
                ws.Cells["R1"].Value = "Pol. ABC Dìas";
                ws.Cells["S1"].Value = "ABC";
                ws.Cells["T1"].Value = "Tipo Calculo";
                ws.Cells["U1"].Value = "Factor Empaque";
                ws.Cells["V1"].Value = "Pedido Final";
                ws.Cells["W1"].Value = "Pedido Final Mod";
                ws.Cells["X1"].Value = "PVD";

                int renglon = 2;
                foreach (var item in response.Result)
                {
                    ws.Cells[string.Format("A{0}", renglon)].Value = item.FechaCreacionSugerido;
                    ws.Cells[string.Format("B{0}", renglon)].Value = item.IdSugerido;
                    ws.Cells[string.Format("C{0}", renglon)].Value = item.IdSucursal;
                    ws.Cells[string.Format("D{0}", renglon)].Value = item.Sucursal;
                    ws.Cells[string.Format("E{0}", renglon)].Value = item.Proveedor;
                    ws.Cells[string.Format("F{0}", renglon)].Value = item.Articulo_Id;
                    ws.Cells[string.Format("G{0}", renglon)].Value = item.Producto;
                    ws.Cells[string.Format("H{0}", renglon)].Value = item.CostoNeto;
                    ws.Cells[string.Format("I{0}", renglon)].Value = item.TipoPedido;
                    ws.Cells[string.Format("J{0}", renglon)].Value = item.ExistenciaPza;
                    ws.Cells[string.Format("K{0}", renglon)].Value = item.TransitoPza;
                    ws.Cells[string.Format("L{0}", renglon)].Value = item.SugeridoInicial;
                    ws.Cells[string.Format("M{0}", renglon)].Value = item.NegadosPza;
                    ws.Cells[string.Format("N{0}", renglon)].Value = item.CompraEspecialPza;
                    ws.Cells[string.Format("O{0}", renglon)].Value = item.InvenadroPza;
                    ws.Cells[string.Format("P{0}", renglon)].Value = item.Capacity;
                    ws.Cells[string.Format("Q{0}", renglon)].Value = item.PedEspFarmacia;
                    ws.Cells[string.Format("R{0}", renglon)].Value = item.PolABCDias;
                    ws.Cells[string.Format("S{0}", renglon)].Value = item.ABC;
                    ws.Cells[string.Format("T{0}", renglon)].Value = item.TipoCalculo;
                    ws.Cells[string.Format("U{0}", renglon)].Value = item.FactorEmpaque;
                    ws.Cells[string.Format("V{0}", renglon)].Value = item.PedidoFinal;
                    ws.Cells[string.Format("W{0}", renglon)].Value = item.PedidoFinalMod;
                    ws.Cells[string.Format("X{0}", renglon)].Value = item.PVD;
                    renglon++;
                }
                //ws.Cells[string.Format("F{0}", renglon)].Value = "Total";
                //ws.Cells[string.Format("G{0}", renglon)].Value = response.Result.Sum(x => x.ImporteTotal).ToString(); ;
                ws.Cells["A:AZ"].AutoFitColumns();
                xlsx = pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            //List<ConciliacionVentaRappi> toExcel
            return xlsx;

        }
    }
}
