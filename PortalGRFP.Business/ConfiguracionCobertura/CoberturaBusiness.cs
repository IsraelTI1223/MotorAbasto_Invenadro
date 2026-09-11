using PortalGRFP.Data;
using PortalGRFP.Data.ConfiguracionCobertura;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using PortalGRFP.Utilities.TableType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.ConfiguracionCobertura
{
    public class CoberturaBusiness
    {
        private readonly CoberturaData coberturaData;

        public CoberturaBusiness()
        {
            coberturaData = new CoberturaData();
        }

        public ResponseList<CatalogoGenerico_Conf> CargaCatalogos(int catalogo)
        {
            var response = new ResponseList<CatalogoGenerico_Conf>();
            try
            {
                response = coberturaData.GetFormas(catalogo);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response GuardarCobertura(CoberturaMontoPza cobertura, int usuario)
        {
            var response = new Response();
            try
            {
                var dt = CoberturaType.Definicion();

                DataRow renglon = dt.NewRow();
                renglon[0] = cobertura.Forma;
                renglon[1] = cobertura.Tipo;
                renglon[2] = cobertura.ClasficacionMonto;
                renglon[3] = cobertura.MontoDe;
                renglon[4] = cobertura.MontoHasta;
                renglon[5] = cobertura.ClasficacionPieza;
                renglon[6] = cobertura.PiezasDe;
                renglon[7] = cobertura.PiezasHasta;
                renglon[8] = cobertura.ParticipacionGrupo;
                renglon[9] = cobertura.ParticipacionDivision;
                renglon[10] = cobertura.DiasCoberturaMay;
                renglon[11] = cobertura.DiasCoberturaCed;
                renglon[12] = cobertura.DiasCoberturaPieCam;
                renglon[13] = cobertura.Periodicidad;
                renglon[14] = usuario;
                dt.Rows.Add(renglon);


                response = coberturaData.GuardarCobertura(dt);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public ResponseList<CoberturaMontoPzaExt> ConsultaCobertura(CoberturaMontoPza cobertura)
        {
            var response = new ResponseList<CoberturaMontoPzaExt>();
            try
            {
                response = coberturaData.ObtenerCoberturas(cobertura);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

        public Response EliminarCobertura(int forma, int tipo, int monto, int pieza)
        {
            var response = new Response();
            try
            {
                response = coberturaData.EliminarCobertura(forma, tipo, monto, pieza);
                response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }  
        
        public Response ValidarFormaBLL(int Idforma)
        {
            var response = new Response();
            try
            {
                response = coberturaData.ValidarFormaData(Idforma);
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }  
        
        public Response EliminarCambioFormaBLL(int Idforma, int Idusuario)
        {
            var response = new Response();
            try
            {
                response = coberturaData.EliminarCambioFormaData(Idforma, Idusuario);
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
    }
}
