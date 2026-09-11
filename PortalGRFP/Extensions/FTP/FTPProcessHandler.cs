using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PortalGRFP.Extensions.FTP
{
    public class FTPProcessHandler
    {

        #region Propiedades
        public String LocalPath = String.Empty;
        #endregion

        #region Variables y objetos de uso exclusivo de la clase

        private Exception MainException = null;

        #endregion

        #region Constructor de la clase

        public FTPProcessHandler()
        {
            // Do nothing
        }

        #endregion

        #region Metodos de uso publico de la clase

        public FTPExecutionResult EjecutaEnvioOCViaFTP(int OrdenCompra, String Sugerido)
        {
            FTPExecutionResult objResult = new FTPExecutionResult();

            try
            {
                // Carga los parametros de ejecucion
                FTPExecutionParameters objExecParams = new FTPExecutionParameters();
                Boolean boolLoadParams = LoadExecutionParams(OrdenCompra, Sugerido, ref objExecParams);

                if (boolLoadParams)
                {
                    // Crea el archivo de la orden de compra segun la plantilla del proveedor
                    Boolean boolCreateFile = CreateOCFile(OrdenCompra, Sugerido, objExecParams);

                    if (boolCreateFile)
                    {
                        // Envia el archivo al proveedor via FTP
                        Boolean boolSendFile = SendFileViaFTP(OrdenCompra, Sugerido, objExecParams);

                        if (boolSendFile)
                        {
                            // Finaliza la operacion
                            MainException = null;
                            objResult = new FTPExecutionResult()
                            {
                                IsComplete = true,
                                LastException = MainException,
                            };
                        }
                    }
                }

                // Finaliza la ejecucion
                if (MainException != null)
                {
                    // Registra el fallo
                    objResult = new FTPExecutionResult()
                    {
                        IsComplete = false,
                        LastException = MainException,
                    };
                }
            }
            catch (Exception ex)
            {
                // Finaliza y registra la excepcion
                objResult = new FTPExecutionResult()
                {
                    IsComplete = false,
                    LastException = ex,
                };
            }

            return objResult;
        }

        #endregion

        #region Funciones y subrutinas de uso exclusivo de la clase

        private Boolean LoadExecutionParams(int OrdenCompra, String Sugerido, ref FTPExecutionParameters ExecutionParams)
        {
            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_FTP_EXTENSION");
            db.AddInParameter(command, "@TipoBusqueda", DbType.String, "POR ORDEN COMPRA");
            db.AddInParameter(command, "@ParametroBuscar", DbType.Int32, OrdenCompra);
            db.AddInParameter(command, "@Sugerido", DbType.String, Sugerido);

           
            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);

            while (read.Read())
            {

                ExecutionParams = new FTPExecutionParameters()
                {
                    Id = Convert.ToInt32(read["Id"]),
                    IdProveedor = Convert.ToInt32(read["Id_Proveedor"]),
                    IdAgencia = Convert.ToInt32(read["Id_Agencia"]),
                    TipoFormato = Convert.ToInt32(read["Tipo_Formato"]),
                    URL = read["URL"] == DBNull.Value ? "" : read["URL"].ToString(),
                    Puerto = 21,
                    Directorio = read["Directorio"] == DBNull.Value ? "" : read["Directorio"].ToString(),
                    DirectorioRespaldo = read["Directorio_Respaldo"] == DBNull.Value ? "" : read["Directorio_Respaldo"].ToString(),
                    CopiaRespaldo = read["Copia_Respaldo"] == DBNull.Value ? "" : read["Copia_Respaldo"].ToString(),
                    EliminaOrigen = read["Elimina_Origen"] == DBNull.Value ? "" : read["Elimina_Origen"].ToString(),
                    Frecuencia = Convert.ToInt32(read["Frecuencia"]),
                    TiempoFrecuencia = read["Tiempo_Frecuencia"] == DBNull.Value ? "" : read["Tiempo_Frecuencia"].ToString(),
                    FechaProximaEjecucion = read["Fecha_Proxima_Ejecucion"] == DBNull.Value ? "" : read["Fecha_Proxima_Ejecucion"].ToString(),
                    HoraIni = read["Hora_Ini"] == DBNull.Value ? "" : read["Hora_Ini"].ToString(),
                    HoraFin = read["Hora_Fin"] == DBNull.Value ? "" : read["Hora_Fin"].ToString(),
                    UsuarioFTP = read["Usuario_FTP"] == DBNull.Value ? "" : read["Usuario_FTP"].ToString(),
                    ClaveFTP = read["Clave_FTP"] == DBNull.Value ? "" : read["Clave_FTP"].ToString(),
                    NomenclaturaArchivo = read["Nomenclatura_Archivo"] == DBNull.Value ? "" : read["Nomenclatura_Archivo"].ToString(),
                    FTPActivo = read["FTP_Activo"] == DBNull.Value ? "" : read["FTP_Activo"].ToString(),
                    IdRazonSocial = Convert.ToInt32(read["Id_RazonSocial"]),
                    NumeroFrecuencia = read["Numero_Frecuencia"] == DBNull.Value ? 0 : Convert.ToInt32(read["Numero_Frecuencia"]),
                    LapsoFrecuencia = read["Lapso_Frecuencia"] == DBNull.Value ? "" : read["Lapso_Frecuencia"].ToString(),
                    ExtensionFormato = read["Extension_Formato"] == DBNull.Value ? "" : read["Extension_Formato"].ToString(),
                };
            }

            return true;
        }

        private Boolean CreateOCFile(int OrdenCompra, String Sugerido, FTPExecutionParameters ExecutionParams)
        {
            Boolean boolCreacion = false;

            // Determina el tipo de archivo que se va a generar
            switch(ExecutionParams.IdProveedor)
            {
                case 1: // Marzam
                    boolCreacion = CreateOCFile_Marzam(OrdenCompra, Sugerido, ExecutionParams);
                    break;
                case 7: // Farmacos nacionales
                    boolCreacion = CreateOCFile_FarmacosNacionales(OrdenCompra, Sugerido, ExecutionParams);
                    break;
                case 9: // Nadro
                    boolCreacion = CreateOCFile_Nadro(OrdenCompra, Sugerido, ExecutionParams);
                    break;
                default:
                    break;
            }

            return boolCreacion;
        }

        private Boolean CreateOCFile_Marzam(int OrdenCompra, String Sugerido, FTPExecutionParameters ExecutionParams)
        {
            Boolean boolCreate = true;

            // Carga la informacion necesaria
            OrdenCompraHeader objHeader = CargaHeaderOrdenCompra(OrdenCompra, Sugerido);
            List<OrdenCompraDetail> objDetail = CargaDetailOrdenCompra(OrdenCompra, Sugerido);

            String strPFolder = "Marzam";

            // Verifica la carpeta destino
            if (CreateTemporaryFolder(strPFolder))
            {
                // Escribe el archivo destino
                int numExt = 1;
                String strDate = String.Format("{0:yyyyMMdd}", DateTime.Now);
                String strFolder = System.IO.Path.Combine(LocalPath, "Temporal", strPFolder, strDate);
                String strFile = System.IO.Path.Combine(strFolder, String.Format("FF99999{0:000}.dat", numExt));

                if (System.IO.File.Exists(strFile))
                {
                    while (System.IO.File.Exists(strFile))
                    {
                        numExt += 1;
                        strFile = System.IO.Path.Combine(strFolder, String.Format("FF99999{0:000}.dat", numExt));
                    }
                }

                using (FileStream objFile = File.Create(strFile))
                {
                    using(StreamWriter objWriter = new StreamWriter(objFile))
                    {
                        // Detail
                        //12345678901234567890123456789012345678901234567890123456789012345678901234567890
                        //| |    |            |  |        |       |
                        //0012345000000000000012300000000012345678

                        foreach (OrdenCompraDetail Item in objDetail)
                        {
                            objWriter.WriteLine(
                                String.Format("{0:00}{1:00000}{2}{3:000}{4:000000000}{5}{6}{7}{8}",
                                objHeader.Agencia,
                                objHeader.CuentaProveedor,
                                Right("0000000000000" + Item.ArticuloId, 13),
                                Item.Sugerido,
                                objHeader.IdOrdenCompra,
                                "00000000",
                                "000000000",
                                "0000000000",
                                "00000000000000000000"));
                        }

                        objWriter.Flush();
                    }

                    //objFile.Flush();
                    //objFile.Close();
                }
            }
            else
            {
                boolCreate = false;
            }

            // Finaliza
            return boolCreate;
        }

        private Boolean CreateOCFile_FarmacosNacionales(int OrdenCompra, String Sugerido, FTPExecutionParameters ExecutionParams)
        {
            Boolean boolCreate = true;

            // Carga la informacion necesaria
            OrdenCompraHeader objHeader = CargaHeaderOrdenCompra(OrdenCompra, Sugerido);
            List<OrdenCompraDetail> objDetail = CargaDetailOrdenCompra(OrdenCompra, Sugerido);

            String strPFolder = "FarmacosNacionales";

            // Verifica la carpeta destino
            if (CreateTemporaryFolder(strPFolder))
            {
                // Escribe el archivo destino
                String strDate = String.Format("{0:yyyyMMdd}", DateTime.Now);
                String strFolder = System.IO.Path.Combine(LocalPath, "Temporal", strPFolder, strDate);
                String strFile = System.IO.Path.Combine(strFolder, String.Format("{0:yyyyMMdd}_{1}.dat", DateTime.Now, OrdenCompra));

                using (FileStream objFile = File.Create(strFile))
                {
                    using (StreamWriter objWriter = new StreamWriter(objFile))
                    {
                        // Header
                        objWriter.WriteLine(String.Format("{0},{1},{2:dd/MM/yyyy},{3}",
                            objHeader.CuentaProveedor,
                            objHeader.CuentaProveedor,
                            objHeader.Fecha,
                            objHeader.IdOrdenCompra));

                        objWriter.Flush();

                        // Detail
                        foreach (OrdenCompraDetail Item in objDetail)
                        {
                            objWriter.WriteLine(String.Format("{0},{1}",
                                Item.ArticuloId,
                                Item.Sugerido));
                        }

                        objWriter.Flush();

                        // Resume
                        objWriter.WriteLine(String.Format("{0},{1}",
                            objDetail.Count,
                            objDetail.Sum(s => s.Sugerido)));

                        objWriter.Flush();
                    }

                    //objFile.Flush();
                    //objFile.Close();
                }
            }
            else
            {
                boolCreate = false;
            }

            // Finaliza
            return boolCreate;
        }

        private Boolean CreateOCFile_Nadro(int OrdenCompra, String Sugerido, FTPExecutionParameters ExecutionParams)
        {
            Boolean boolCreate = true;

            // Carga la informacion necesaria
            OrdenCompraHeader objHeader = CargaHeaderOrdenCompra(OrdenCompra, Sugerido);
            List<OrdenCompraDetail> objDetail = CargaDetailOrdenCompra(OrdenCompra, Sugerido);

            String strPFolder = "Nadro";

            // Verifica la carpeta destino
            if (CreateTemporaryFolder(strPFolder))
            {
                // Escribe el archivo destino
                int numExt = 1;
                String strDate = String.Format("{0:yyyyMMdd}", DateTime.Now);
                String strFolder = System.IO.Path.Combine(LocalPath, "Temporal", strPFolder, strDate);
                String strFile = System.IO.Path.Combine(strFolder, String.Format("NP{0}.{1:000}", "00S999", numExt));

                if (System.IO.File.Exists(strFile))
                {
                    while (System.IO.File.Exists(strFile))
                    {
                        numExt += 1;
                        strFile = System.IO.Path.Combine(strFolder, String.Format("NP{0}.{1:000}", "00S999", numExt));
                    }
                }

                using (FileStream objFile = File.Create(strFile))
                {
                    using (StreamWriter objWriter = new StreamWriter(objFile))
                    {
                        // Header
                        //123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
                        //||    |      |  ||              |
                        //NNADRO1234567000 00000000000000020211230
                        objWriter.WriteLine(String.Format("{0}{1}{2:0000000}{3:000}{4}{5:000000000000000}{6:yyyyMMdd}",
                            "N",
                            "NADRO",
                            objHeader.CuentaProveedor,
                            0,
                            " ",
                            objHeader.IdOrdenCompra,
                            objHeader.Fecha));

                        objWriter.Flush();

                        // Detail
                        //12345678901234567890
                        //|             |
                        //07501111222222000001
                        foreach (OrdenCompraDetail Item in objDetail)
                        {
                            objWriter.WriteLine(String.Format("{0}{1:000000}",
                                Right("00000000000000" + Item.ArticuloId, 14),
                                Item.Sugerido));
                        }

                        objWriter.Flush();
                    }

                    //objFile.Flush();
                    //objFile.Close();
                }
            }
            else
            {
                boolCreate = false;
            }

            // Finaliza
            return boolCreate;
        }

        private OrdenCompraHeader CargaHeaderOrdenCompra(int OrdenCompra, String Sugerido)
        {
            OrdenCompraHeader objReturn = null;

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            // Header
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_ORDEN_COMPRA_PROVEEDOR");
            db.AddInParameter(command, "@TipoBusqueda", DbType.String, "HEADER");
            db.AddInParameter(command, "@IdOrdenCompra", DbType.Int32, OrdenCompra);
            db.AddInParameter(command, "@IdSugerido", DbType.String, Sugerido);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);

            while (read.Read())
            {
                objReturn = new OrdenCompraHeader()
                {
                    IdOrdenCompra = Convert.ToInt32(read["IdOrdenCompra"]),
                    CodigoSucursal = Convert.ToInt32(read["CodigoSucursal"]),
                    IdProveedor = Convert.ToInt32(read["IdProveedor"]),
                    Total = Convert.ToDouble(read["Total"]),
                    Fecha = Convert.ToDateTime(read["Fecha"]),
                    Agencia = Convert.ToInt32(read["Agencia"]),
                    CuentaProveedor = Convert.ToInt32(read["CuentaProveedor"]),
                };
            }

            return objReturn;
        }

        private List<OrdenCompraDetail> CargaDetailOrdenCompra(int OrdenCompra, String Sugerido)
        {
            List<OrdenCompraDetail> objReturn = new List<OrdenCompraDetail>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            // Header
            var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_ORDEN_COMPRA_PROVEEDOR");
            db.AddInParameter(command, "@TipoBusqueda", DbType.String, "DETAIL");
            db.AddInParameter(command, "@IdOrdenCompra", DbType.Int32, OrdenCompra);
            db.AddInParameter(command, "@IdSugerido", DbType.String, Sugerido);

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);

            while (read.Read())
            {
                objReturn.Add(new OrdenCompraDetail()
                {
                    IdOrdenCompra = Convert.ToInt32(read["IdOrdenCompra"]),
                    ArticuloId = Convert.ToString(read["articulo_id"]),
                    CodigoSucursal = Convert.ToInt32(read["CodigoSucursal"]),
                    Sugerido = Convert.ToInt32(read["Sugerido"]),
                    Costo = Convert.ToDouble(read["pcio_Costo"]),
                    Iva = Convert.ToDouble(read["porc_iva"]),
                    Ieps = Convert.ToDouble(read["porc_ieps"]),
                });
            }

            return objReturn;
        }

        private Boolean CreateTemporaryFolder(String SubfolderName)
        {
            Boolean boolTempDir = true;

            try
            {
                String strDate = String.Format("{0:yyyyMMdd}", DateTime.Now);
                String strFolder = System.IO.Path.Combine(LocalPath, "Temporal", SubfolderName, strDate);

                if (!System.IO.Directory.Exists(strFolder))
                {
                    System.IO.Directory.CreateDirectory(strFolder);
                }
            }
            catch (Exception ex)
            {
                MainException = ex;
                boolTempDir = false;
            }

            return boolTempDir;
        }

        private Boolean SendFileViaFTP(int OrdenCompra, String Sugerido, FTPExecutionParameters ExecutionParams)
        {

            return true;
        }

        private String Right(String original, int numberCharacters)
        {
            return original.Substring(numberCharacters > original.Length ? 0 : original.Length - numberCharacters);
        }
        
        #endregion

    }

    public class OrdenCompraHeader
    {
        public int IdOrdenCompra { get; set; }
        public int CodigoSucursal { get; set; }
        public int IdProveedor { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
        public int Agencia { get; set; }
        public int CuentaProveedor { get; set; }

        public OrdenCompraHeader()
        {
            // Do nothing
        }
    }

    public class OrdenCompraDetail
    {
        public int IdOrdenCompra { get; set; }
        public String ArticuloId { get; set; }
        public int CodigoSucursal { get; set; }
        public int Sugerido { get; set; }
        public Double Costo { get; set; }
        public Double Iva { get; set; }
        public Double Ieps { get; set; }

        public OrdenCompraDetail()
        {
            // Do nothing
        }
    }

    public class FTPExecutionResult
    {
        public Boolean IsComplete { get; set; }
        public Exception LastException { get; set; }

        public FTPExecutionResult()
        {
            // Do nothing
        }
    }

    public class FTPExecutionParameters
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdAgencia { get; set; }
        public int TipoFormato { get; set; }
        public String URL { get; set; }
        public int Puerto { get; set; }
        public String Directorio { get; set; }
        public String DirectorioRespaldo { get; set; }
        public String CopiaRespaldo { get; set; }
        public String EliminaOrigen { get; set; }
        public int Frecuencia { get; set; }
        public String TiempoFrecuencia { get; set; }
        public String FechaProximaEjecucion { get; set; }
        public String HoraIni { get; set; }
        public String HoraFin { get; set; }
        public String UsuarioFTP { get; set; }
        public String ClaveFTP { get; set; }
        public String NomenclaturaArchivo { get; set; }
        public String FTPActivo { get; set; }
        public int IdRazonSocial { get; set; }
        public int NumeroFrecuencia { get; set; }
        public String LapsoFrecuencia { get; set; }
        public String ExtensionFormato { get; set; }

        public FTPExecutionParameters()
        {
            // Do nothing
         
        }
    }


}