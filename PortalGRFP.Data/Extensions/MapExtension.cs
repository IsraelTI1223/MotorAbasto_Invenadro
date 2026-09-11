namespace PortalGRFP.Data.Extensions
{
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Common.MantenimientoArticulo;
    using PortalGRFP.Entities.Common.Sugerido;
    using PortalGRFP.Entities.Response;
    using System;
    using System.Data;
    using System.Dynamic;
    using static PortalGRFP.Entities.Common.ApiPedidosModel;
    using static PortalGRFP.Entities.Common.ComprasEspeciales.ConsultaPedidosModel;
    using static PortalGRFP.Entities.Common.MantenimientoArticulo.ArticuloPermisoCompraModel;


    public static class MapExtension
    {
        public static Response ToResponse(this IDataReader reader)
        {
            return new Response
            {
                Message = reader.Get<string>("Message"),
                Success = reader.Get<bool>("Success")
            };
        }

        public static DescuentoHdr ToDescuentoHdr(this IDataReader reader)
        {
            return new DescuentoHdr
            {
                IdHdr = reader.Get<int>("IdHdr"),
                TipoCarga = reader.Get<string>("TipoCarga"),
                NombreArchivo = reader.Get<string>("NombreArchivo"),
                TotalRegistros = reader.Get<int>("TotalRegistros"),
                TotalCargados = reader.Get<int>("TotalCargados"),
                TotalErroneos = reader.Get<int>("TotalErrores"),
                FechaRegistro = reader.Get<string>("FechaRegistro"),
                FechaActualizacion = reader.Get<string>("FechaActualizacion"),
                UsuarioRegistro = reader.Get<string>("UsuarioRegistro"),
                NombreCliente = reader.Get<string>("Nombre"),
                NombreSubCliente = reader.Get<string>("NombreSubCliente")
            };
        }

        public static Modulo ToModulo(this IDataReader reader)
        {
            return new Modulo
            {
                IdModulo = reader.Get<int>("IdModulo"),
                NombreModulo = reader.Get<string>("Modulo"),
                Componente = reader.Get<string>("Componente"),
                IconoBase = reader.Get<string>("IconoBase"),
                Opciones = reader.Get<int>("Opciones"),
                IdPadre = reader.Get<int>("IdPadre"),
            };
        }

        public static Perfil ToPerfil(this IDataReader reader)
        {
            return new Perfil
            {
                IdPerfil = reader.Get<int>("IdPerfil"),
                Nombre = reader.Get<string>("Nombre"),
                Activo = reader.Get<bool>("Activo")
            };
        }

        public static BusquedaTipoPerfil TipoBusqueda(this IDataReader reader)
        {
            return new BusquedaTipoPerfil
            {
                IdCliente = reader.Get<int>("IdCliente"),
                IdTipo = reader.Get<int>("IdTipo"),
                Nombre = reader.Get<string>("Nombre"),
                RFC = reader.Get<string>("RFC"),
                Id_SubCliente = reader.Get<int>("Id_SubCliente"),
                SubCliente = reader.Get<string>("SubCliente")
            };
        }


        public static BusquedaTipoPerfilPopsae TipoBusquedaPopsae(this IDataReader reader)
        {
            return new BusquedaTipoPerfilPopsae
            {
                IdTipo = reader.Get<int>("IdTipo"),
                Descripcion = reader.Get<string>("Descripcion"),
                Activo = reader.Get<bool>("Activo"),
                FechaRegistro = reader.Get<DateTime>("FechaRegistro"),
                IdPerfil = reader.Get<int>("IdPerfil")
            };
        }

        public static CtrlPerfil ToCtrlPerfil(this IDataReader reader)
        {
            return new CtrlPerfil
            {
                IdCtrlPerfil = reader.Get<int>("IdCtrlPerfil"),
                IdPerfil = reader.Get<int>("IdPerfil"),
                IdModulo = reader.Get<int>("IdModulo"),
                Nombre = reader.Get<string>("Nombre"),
                Registra = reader.Get<bool>("Registra"),
                Actualiza = reader.Get<bool>("Actualiza"),
                Elimina = reader.Get<bool>("Elimina"),
                Consulta = reader.Get<bool>("Consulta"),
                Activo = reader.Get<bool>("Activo")
            };
        }


        public static ConfiguracionCarga ToConfiguracionCarga(this IDataReader reader)
        {
            return new ConfiguracionCarga
            {
                ArchivosPorDia = reader.Get<int>("ArchivosPorDia"),
                HoraFin = reader.Get<TimeSpan>("HoraFin"),
                HoraInicio = reader.Get<TimeSpan>("HoraInicio"),
                IdCarga = reader.Get<int>("IdCarga"),
                IdTipoCarga = reader.Get<int>("IdTipoCarga"),
                Nombre = reader.Get<string>("Nombre"),
                Tipo = reader.Get<string>("Tipo")
            };
        }

        public static ModuloAccion ToModuloAccion(this IDataReader reader)
        {
            return new ModuloAccion
            {
                IdModulo = reader.Get<int>("IdModulo"),
                NombreModulo = reader.Get<string>("Modulo"),
                //Componente = reader.Get<string>("Componente"),
                //IconoBase = reader.Get<string>("IconoBase"),
                Actualizar = reader.Get<int>("Actualizar") == 1 ? true : false,
                Consultar = reader.Get<int>("Consultar") == 1 ? true : false,
                Cargar = reader.Get<int>("Cargar") == 1 ? true : false,
                Eliminar = reader.Get<int>("Eliminar") == 1 ? true : false,
                Insertar = reader.Get<int>("Insertar") == 1 ? true : false,
                Descargar = reader.Get<int>("Descargar") == 1 ? true : false,
                Opciones = reader.Get<int>("Opciones")
            };
        }

        public static Accion ToAccion(this IDataReader reader)
        {
            return new Accion
            {
                IdAccion = reader.Get<int>("IdAccion"),
                Nombre = reader.Get<string>("Nombre")
            };
        }

        public static PerfilModuloAccion ToPerfilModuloAccion(this IDataReader reader)
        {
            return new PerfilModuloAccion
            {
                IdModulo = reader.Get<int>("IdModulo"),
                IdPadre = reader.Get<int>("IdPadre"),
                Modulo = reader.Get<string>("Modulo"),
                IdAccion = reader.Get<int>("IdAccion"),
                Nombre = reader.Get<string>("Nombre"),
                Opciones = reader.Get<int>("Opciones"),
                EstatusAccion = reader.Get<int>("EstatusAccion")
            };
        }

        public static ConciliacionVentaRappi ToConciliacionVentaRappi(this IDataReader reader)
        {
            return new ConciliacionVentaRappi
            {
                DiaOperacion = reader.Get<DateTime>("Dia Operación"),
                BinesCard = reader.Get<string>("BIN"),
                CodigoAutorizacion = reader.Get<string>("AUTH_CODE"),
                Conciliate = reader.Get<string>("Conciliate"),
                FormaPago = reader.Get<string>("Forma de Pago"),
                IdSucursal = reader.Get<string>("Id Sucursal"),
                Iteracion = reader.Get<string>("Iteracion"),

                Marca = reader.Get<string>("Marca"),
                RazonSocial = reader.Get<string>("Razón Social"),
                OrderId = reader.Get<string>("Order ID"),
                TicketUrl = reader.Get<string>("Ticket URL"),
                Ultimos4Digitos = reader.Get<string>("LAST_FOUR_DIGITS"),
                Store = reader.Get<string>("Store"),
                TicketVenta = reader.Get<string>("Ticket"),
                StoreID = reader.Get<string>("Store ID"),
                ImporteTotal = reader.Get<decimal>("Importe Total"),
                Observacion = reader.Get<string>("Observacion")
            };
        }

        public static ReporteCatProductosModel ToReporteCatProdExcel(this IDataReader dr)
        {
            return new ReporteCatProductosModel
            {
                Sku = dr.Get<string>("Sku"),
                Descripcion = dr.Get<string>("Descripcion"),
                Grupo = dr.Get<string>("Grupo"),
                Familia = dr.Get<string>("Familia"),
                Categoria = dr.Get<string>("Categoria"),
                Clasificacion = dr.Get<string>("Clasificacion"),
                Fecha = dr.Get<string>("fecha"),
            };
        }

        public static ReporteSucursalesNuevasModel ToReporteSucursalesNuevasExcel(this IDataReader dr)
        {
            return new ReporteSucursalesNuevasModel
            {
                Cadena = dr.Get<string>("CADENA"),
                Subcadena = dr.Get<string>("SUBCADENA"),
                Id = dr.Get<string>("ID"),
                Nombre = dr.Get<string>("Nombre"),
                Regiones = dr.Get<string>("REGIONES"),
                Fecha = dr.Get<string>("Fecha")
            };
        }

        public static ReporteInvenadroModel ToReporteInvenadroExcel(this IDataReader dr)
        {
            return new ReporteInvenadroModel
            {
                Fecha_Consulta = dr.Get<string>("Fecha_Consulta"),
                Marca = dr.Get<string>("Marca"),
                Suc_id = dr.Get<int>("Suc_id"),
                Nombre_Corto = dr.Get<string>("Nombre_Corto"),
                Articulo_id = dr.Get<string>("Articulo_id"),
                DescripcionCorta = dr.Get<string>("DescripcionCorta"),
                Optimo = dr.Get<int>("Optimo"),
                Fecha_optimo = dr.Get<string>("Fecha_optimo")
            };
        }

        public static ConsultaArticuloModel ToReporteArticulosExcel(this IDataReader dr)
        {
            return new ConsultaArticuloModel
            {
                SKU = dr.Get<string>("SKU"),
                DESCRIPCION = dr.Get<string>("DESCRIPCION"),
                Grupo = dr.Get<string>("Grupo"),
                Familia = dr.Get<string>("Familia"),
                Tipo_Proveedor = dr.Get<string>("Tipo_Proveedor"),
                Estatus = dr.Get<string>("Estatus"),
                Empaque = dr.Get<string>("Empaque"),
                Controlado = dr.Get<string>("Controlado"),
                Refrigerado = dr.Get<string>("Refrigerado"),
                AltaEspecialidad = dr.Get<string>("AltaEspecialidad"),
                PermisoDevolver = dr.Get<string>("PermisoDevolver"),
                Invenadro = dr.Get<string>("Invenadro")

            };
        }


        public static ConsultaArticuloSucursalModel ToReporteArticulosSucursalExcel(this IDataReader dr)
        {
            return new ConsultaArticuloSucursalModel
            {
                ID_Sucursal = dr.Get<int>("ID_Sucursal"),
                SKU = dr.Get<string>("SKU"),
                DESCRIPCION_CORTA = dr.Get<string>("DESCRIPCION_CORTA"),
                GRUPO = dr.Get<string>("GRUPO"),
                FAMILIA = dr.Get<string>("FAMILIA"),
                Existencia = dr.Get<string>("Existencia"),
                PVD = dr.Get<string>("PVD"),
                Fecha_Venta = dr.Get<string>("Fecha_Venta"),
                piezas_total = dr.Get<string>("piezas_total")

            };
        }

        public static ConsultaSucursales ToReporteSucursalExcel(this IDataReader dr)
        {
            return new ConsultaSucursales
            {
                Cadena = dr.Get<string>("Cadena"),
                Marca = dr.Get<string>("Marca"),
                Suc_Id = dr.Get<string>("Suc_Id"),
                Suc_Nombre = dr.Get<string>("Suc_Nombre"),
                Licencia = dr.Get<string>("Licencia"),
                Sdom = dr.Get<string>("Sdom"),
                hrs = dr.Get<string>("hrs"),
                INVENADRO = dr.Get<string>("INVENADRO"),
                ESTATUS = dr.Get<string>("ESTATUS"),
                CIUDAD = dr.Get<string>("CIUDAD"),
                ESTADO = dr.Get<string>("ESTADO")

            };
        }


        public static ComboSucursalesProveedor ToReporteSucursalProveedoresExcel(this IDataReader dr)
        {
            return new ComboSucursalesProveedor
            {
                Nombre_Marca = dr.Get<string>("Nombre_Marca"),
                CodigoSucursal = dr.Get<string>("CodigoSucursal"),
                Suc_Nombre = dr.Get<string>("Suc_Nombre"),
                Proveedor = dr.Get<string>("Proveedor"),
                Agencia = dr.Get<string>("Agencia"),
                lunes = dr.Get<string>("lunes"),
                martes = dr.Get<string>("martes"),
                miercoles = dr.Get<string>("miercoles"),
                jueves = dr.Get<string>("jueves"),
                viernes = dr.Get<string>("viernes"),
                sabado = dr.Get<string>("sabado"),
                domingo = dr.Get<string>("domingo")

            };
        }
        public static ComboGenerico ToComboGenerico(this IDataReader reader)
        {
            return new ComboGenerico
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),

            };
        }
        public static ComboGenerico ToComboGenericoResumen(this IDataReader reader)
        {
            return new ComboGenerico
            {
                IDstring = reader.Get<string>("Id"),
                Valor = reader.Get<string>("Valor"),

            };
        }

        public static ApiCatObjects ToApiCatObjects(this IDataReader reader)
        {
            return new ApiCatObjects
            {
                id_action = reader.Get<int>("IdAction"),
                spLoad = reader.Get<string>("spLoad"),
                spGetRequest = reader.Get<string>("spGetRequest"),
                spSetResponse = reader.Get<string>("spSetResponse"),
                spSendResponse = reader.Get<string>("spSendResponse"),
            };
        }

        public static Combo4 ToComboInv(this IDataReader reader)
        {
            return new Combo4
            {
                Filtro = reader.Get<string>("Filtro"),

            };
        }



        public static TicketVenta ToConciliacionTicketventa(this IDataReader reader)
        {
            return new TicketVenta
            {
                DiaOperacion = reader.Get<DateTime>("Fecha"),
                Sucursal = reader.Get<string>("Sucursal"),
                Subtotal = reader.Get<float>("subtotal"),
                Iva = reader.Get<float>("iva"),
                Total = reader.Get<float>("total"),
                Ticket = reader.Get<string>("ticket"),

            };
        }

        public static ResumenCargaRappi ToResumenCargaRappi(this IDataReader reader)
        {
            return new ResumenCargaRappi
            {
                Anio = reader.Get<int>("Anio_Archivo"),
                Mes = reader.Get<int>("Mes_Archivo"),
                Lineas = reader.Get<int>("Lineas"),
                Importe = reader.Get<decimal>("Importe"),
                FechaCarga = reader.Get<DateTime>("ODTTC"),
                Porcentaje = reader.Get<decimal>("Porcentaje")
            };
        }





        /*EXTENSIONES MOTOR DE ABASTO*/

        public static SincronizacionInput ToConsultaMTA(this IDataReader reader)
        {
            return new SincronizacionInput
            {
                id_input = reader.Get<int>("id_input"),
                NameInput = reader.Get<string>("NameInput"),
                Porcentaje = reader.Get<int>("Porcentaje"),
                Fecha = reader.Get<string>("fecha_actualizacion")
            };
        }

        public static ABC ToConsultaABC(this IDataReader reader)
        {
            return new ABC
            {
                IdABC = reader.Get<int>("IdABC"),
                Piezas = reader.Get<string>("Piezas"),
                Montos = reader.Get<string>("Montos"),
                ClasFinal = reader.Get<string>("ClasFinal")
            };
        }





        public static DetalleInput ToConsultaDetalleMTA(this IDataReader reader)
        {
            return new DetalleInput
            {
                CodigoSucursal = reader.Get<int>("CodigoSucursal"),
                Nombre_Marca = reader.Get<string>("Nombre_Marca"),
                Registros = reader.Get<int>("Registros"),
                Motivo = reader.Get<string>("Motivo")
            };
        }

        public static DetalleInputVenta ToVentaDetalleMTA(this IDataReader reader)
        {
            return new DetalleInputVenta
            {
                IDTienda = reader.Get<int>("IDTienda"),
                Tienda = reader.Get<string>("Tienda"),
                Cadena = reader.Get<string>("Cadena"),
                Status_Venta = reader.Get<string>("Status_Venta"),
                Venta_Piezas = reader.Get<decimal>("Venta_Piezas"),
                Venta_Historica = reader.Get<decimal>("Venta_Historica"),
                Diferencia = reader.Get<string>("Diferencia"),
                Fecha_Venta = reader.Get<string>("Fecha_Venta"),
                Status_Existencia = reader.Get<string>("Status_Existencia"),
                Existencia_Piezas = reader.Get<decimal>("Existencia_Piezas"),
                Fecha_Existencias = reader.Get<string>("Fecha_Existencias")
            };
        }

        public static DetalleInputInvenadro ToInvenadroDetalleMTA(this IDataReader reader)
        {
            return new DetalleInputInvenadro
            {
                Tiendas = reader.Get<int>("Tiendas"),
                Cadena = reader.Get<string>("Cadena"),
                Optimo = reader.Get<string>("Optimo"),
                Fecha = reader.Get<string>("Fecha")

            };
        }

        public static DetalleInputNegado ToNegadoDetalleMTA(this IDataReader reader)
        {
            return new DetalleInputNegado
            {
                Tienda = reader.Get<int>("Tienda"),
                Cadena = reader.Get<string>("Cadena"),
                Negados = reader.Get<string>("Negados"),
                Fecha = reader.Get<string>("Fecha"),
                NegadosSin = reader.Get<decimal>("NegadosSin")

            };
        }

        public static DetalleInputLista ToListaDetalleMTA(this IDataReader reader)
        {
            return new DetalleInputLista
            {
                Proveedor = reader.Get<string>("Proveedor"),
                Estatus = reader.Get<string>("Estatus"),
                NumLineas = reader.Get<decimal>("NumLineas"),
                Fecha = reader.Get<string>("Fecha")

            };
        }

        public static DetalleInputTransitt ToTransitDetalleMTA(this IDataReader reader)
        {
            return new DetalleInputTransitt
            {
                Tienda = reader.Get<string>("Tienda"),
                Cadena = reader.Get<string>("Cadena"),
                OC_Transito = reader.Get<decimal>("OC_Transito"),
                Fecha = reader.Get<string>("Fecha"),
                lineas = reader.Get<int>("lineas")

            };
        }

        public static Invenadro ToInvenadroRegla(this IDataReader reader)
        {
            return new Invenadro
            {
                LimiteCostoProductos = reader.Get<bool>("LimiteCostoProductos"),
                Monto = reader.Get<decimal>("Monto"),
                PiezasProductos = reader.Get<int>("PiezasProductosNuevos"),
                PVD = reader.Get<bool>("Comprar_c_PVD"),
                InvenadroCero = reader.Get<bool>("Invenadro_cero"),
                ActivarProducto = reader.Get<bool>("activarProducto"),
                InactivarProductos = reader.Get<bool>("inactivarProducto"),
                PorcentajeCoincidencia = reader.Get<decimal>("PorcentajeCoincidencia"),
                Actualizacion_automatica = reader.Get<bool>("Actualizacion_automatica"),
                diasPVD = reader.Get<int>("diasPVD"),
                diasAutomatico = reader.Get<int>("diasAutomatico"),
            };
        }

        public static Relacion_Invenadro_Aut ToRelacionInvenadro(this IDataReader reader)
        {
            return new Relacion_Invenadro_Aut
            {
                SKU = reader.GetString(0),
                NombreSKU = reader.GetString(1),
                PVD = reader.GetDecimal(2),
                Optimo = reader.GetInt32(3),
                EstatusProducto = reader.GetString(4),
                IdSucursal = reader.GetString(5),
                NombreSucursal = reader.GetString(6),
                Motivo = reader.GetString(7)
            };
        }

        public static Excluidos_Invenadro_Aut ToExcluidosInvenadro(this IDataReader reader)
        {
            return new Excluidos_Invenadro_Aut
            {
                IdSucursal = reader.GetString(0),
                NombreSucursal = reader.GetString(1),
                SKU = reader.GetString(2),
                NombreSKU = reader.GetString(3),
                OptimoAnterior = reader.GetInt32(4),
                OptimoActualizado = reader.GetInt32(5),
                PVD = reader.GetDecimal(6),
                RelacionInvenadroVenta = reader.GetString(7),
                Motivo = reader.GetString(8)
            };
        }
        public static Excluidos_Invenadro_Aut ToDetalleEstatusInvenadro(this IDataReader reader)
        {
            return new Excluidos_Invenadro_Aut
            {
                IdSucursal = reader.GetString(0),
                NombreSucursal = reader.GetString(1),
                SKU = reader.GetString(2),
                NombreSKU = reader.GetString(3),
                OptimoActualizado = reader.GetInt32(4),
                InvenadroMontoNuevo = reader.GetDecimal(5),
                RelacionInvenadroVenta = reader.GetString(6),
                EstatusProducto = reader.GetString(7),
            };
        }
        public static Resumen_Invenadro ToResumenInvenadro(this IDataReader reader)
        {
            return new Resumen_Invenadro
            {
                EstatusProducto = reader.GetString(0),
                Motivo = reader.GetString(1),
                FarmaciasSKU = reader.GetInt32(2),
                Piezas = reader.GetInt32(3),
                MontoInvenadro = reader.GetDecimal(4),
                oFlag = reader.GetString(5),
                oEx = reader.GetString(6),
            };
        }

        public static InfoCDR ToBalanceCDRs(this IDataReader reader)
        {
            return new InfoCDR
            {
                Id = reader.GetInt32(0),
                Agencia_id = reader.GetInt32(1),
                Nombre = reader.GetString(2),
                agencia_codigo_interno = reader.GetString(3),
                Farmacias = reader.GetInt32(4),
                MontoCDR = reader.GetDecimal(5),
                MontoNecesidad = reader.GetDecimal(6),
                PedidosRealizar = reader.GetInt32(7),
                PorcentajeCompraMayor = reader.GetDecimal(8),
                PorcentajeCompraMenor = reader.GetDecimal(9),
                MontoCompraMayor = reader.GetDecimal(10),
                MontoCompraMenor = reader.GetDecimal(11),
                Usuario = reader.GetInt32(12),
                oFlag = reader.GetString(13),
                oDTTC = reader.GetDateTime(14),
                oDTTM = reader.GetDateTime(15)
            };
        }

        public static CatalogoGenerico_Conf ToCatalogoGenerico_Conf(this IDataReader reader)
        {
            return new CatalogoGenerico_Conf
            {
                Id_Catalogo = reader.Get<int>("ID"),
                Valor_Catalogo = reader.Get<string>("VALOR")
            };
        }


        public static CoberturaMontoPza ToCoberturaMontoPza(this IDataReader reader)
        {
            return new CoberturaMontoPza
            {
                Forma = reader.Get<int>("IDFORMA"),
                Tipo = reader.Get<int>("ID_TIPO"),
                Periodicidad = reader.Get<int>("FRECUENCIA"),
                DiasCoberturaCed = reader.Get<int>("DIAS_COBERTURA_CEDIS"),
                PiezasDe = reader.Get<int>("PIEZASDE"),
                PiezasHasta = reader.Get<int>("	PIEZASHASTA"),
                MontoDe = reader.Get<decimal>("MONTODE"),
                MontoHasta = reader.Get<decimal>("MONTOHASTA"),
                ParticipacionDivision = reader.Get<bool>("PORC_PARTICIPACION_DIVISION"),
                DiasCoberturaPieCam = reader.Get<int>("DIAS_COBERTURA_PIECAMION"),
                DiasCoberturaMay = reader.Get<int>("DIAS_COBERTURA_MAYORISTA"),
                ParticipacionGrupo = reader.Get<bool>("PORC_PARTICIPACION_GRUPO"),
                ClasficacionPieza = reader.Get<int>("ID_CLASIFICACIONPZA"),
                ClasficacionMonto = reader.Get<int>("ID_CLASIFICACIONMONTO")

            };
        }

        public static Autocomplete ToAutocomplete(this IDataReader reader)
        {
            return new Autocomplete
            {
                Id = reader.Get<int>("ID"),
                Valor = reader.Get<string>("VALOR")
            };
        }

        public static AgenciaProveedorExt ToAgenciaProveedor(this IDataReader reader)
        {
            return new AgenciaProveedorExt
            {
                Proveedor = reader.Get<string>("PROVEEDOR"),
                IdProveedor = reader.Get<int>("PROVEEDOR_ID"),
                IdAgencia = reader.Get<int>("AGENCIA_ID"),
                Agencia = reader.Get<string>("AGENCIA"),
                CatalogoAutomatico = reader.Get<bool>("CATALOGO_AUTOMATICO"),
                FacturaAutomatica = reader.Get<bool>("FACTURA_AUTOMATICA"),
                OrdenCompraAutomatica = reader.Get<bool>("OCOMPRA_AUTOMATICA"),
                PermiteRemisiones = reader.Get<bool>("PERMITE_REMISIONES"),
                RespuestaFaltante = reader.Get<bool>("RESPUESTA_FALTANTE")
            };
        }

        public static CoberturaMontoPzaExt ToCoberturaMontoPzaExt(this IDataReader reader)
        {
            return new CoberturaMontoPzaExt
            {
                Forma = reader.Get<int>("IDFORMA"),
                Tipo = reader.Get<int>("ID_TIPO"),
                Periodicidad = reader.Get<int>("FRECUENCIA"),
                DiasCoberturaCed = reader.Get<int>("DIAS_COBERTURA_CEDIS"),
                PiezasDe = reader.Get<int>("PIEZASDE"),
                PiezasHasta = reader.Get<int>("PIEZASHASTA"),
                MontoDe = reader.Get<decimal>("MONTODE"),
                MontoHasta = reader.Get<decimal>("MONTOHASTA"),
                ParticipacionDivision = reader.Get<bool>("PORC_PARTICIPACION_DIVISION"),
                DiasCoberturaPieCam = reader.Get<int>("DIAS_COBERTURA_PIECAMION"),
                DiasCoberturaMay = reader.Get<int>("DIAS_COBERTURA_MAYORISTA"),
                ParticipacionGrupo = reader.Get<bool>("PORC_PARTICIPACION_GRUPO"),
                ClasficacionPieza = reader.Get<int>("ID_CLASIFICACIONPZA"),
                ClasficacionMonto = reader.Get<int>("ID_CLASIFICACIONMONTO"),
                Forma_Desc = reader.Get<string>("FORMA"),
                Tipo_Desc = reader.Get<string>("TIPO"),
                LetraMonto = reader.Get<string>("monto_clasif"),
                LetraPza = reader.Get<string>("pieza_clasif"),
                IdGrupo = reader.Get<int>("IdGrupo"),
                IdDivision = reader.Get<int>("iddivision"),
            };
        }

        public static ComboGenericoProveedorCompra ToComboProveedor(this IDataReader reader)
        {
            return new ComboGenericoProveedorCompra
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),
                Descr = reader.Get<string>("Descr")

            };
        }


        public static ComboGenerico ToComboFormatoProveedor(this IDataReader reader)
        {
            return new ComboGenerico
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),


            };
        }

        public static ComboGenerico ToComboFrecuenciaProveedor(this IDataReader reader)
        {
            return new ComboGenerico
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),


            };
        }

        public static ComboGenericoProv ToComboProv(this IDataReader reader)
        {
            return new ComboGenericoProv
            {
                PROVEEDOR_ID = reader.Get<int>("PROVEEDOR_ID"),
                Nombre = reader.Get<string>("Nombre"),
                //AGENCIA_ID = reader.Get<int>("AGENCIA_ID")

            };
        }

        public static PorcentajeProv ToConsultaPRV(this IDataReader reader)
        {
            return new PorcentajeProv
            {
                Id = reader.Get<int>("Id"),
                Proveedor = reader.Get<string>("Proveedor"),
                Descr = reader.Get<string>("Descr"),
                Empates = reader.Get<int>("Empates"),
                ProteccionCosto = reader.Get<int>("ProteccionCosto"),
                PlanCrecimiento = reader.Get<int>("PlanCrecimiento"),
                NumProductos = reader.Get<int>("NumProductos"),
            };
        }


        public static ProveedoresAgenciaFTP ToConsultaFTP(this IDataReader reader)
        {
            return new ProveedoresAgenciaFTP
            {
                Id = reader.Get<int>("Id"),
                Proveedor = reader.Get<string>("Proveedor"),
                Agencia = reader.Get<string>("Agencia"),
                Tipo_Formato = reader.Get<string>("Tipo_Formato"),
                Nomenclatura_Archivo = reader.Get<string>("Nomenclatura_Archivo"),
                URL = reader.Get<string>("URL"),
                Directorio = reader.Get<string>("Directorio"),
                Directorio_Respaldo = reader.Get<string>("Directorio_Respaldo"),
                Frecuencia = reader.Get<string>("Frecuencia"),

            };
        }


        public static MantenimientoSucursal ToMantenimientoSucursal(this IDataReader reader)
        {
            return new MantenimientoSucursal
            {
                IdSucursal = reader.Get<int>("idSucursal"),
                Grupo = reader.Get<int>("idGrupo"),
                Invenadro = reader.Get<bool>("aplicaINvenadro"),
                Negados = reader.Get<bool>("aplicaNegados"),
                PVD = reader.Get<bool>("aplicaPVD"),
            };
        }

        public static ProveedorAgencia ToProveedorAgencia(this IDataReader reader)
        {
            return new ProveedorAgencia
            {
                IdProveedor = reader.Get<int>("IdProveedor"),
                IdCliente = reader.Get<int>("IdCliente"),
                IdAgencia = reader.Get<int>("IdAgencia"),
                Agencia = reader.Get<string>("Agencia")

            };
        }


        public static SucursalProveedorExt ToSucursalProveedorExt(this IDataReader reader)
        {
            return new SucursalProveedorExt
            {
                IdProveedor = reader.Get<int>("id_proveedor"),
                IdSucursal = reader.Get<int>("id_suc"),
                IdAgencia = reader.Get<int>("Agencia"),
                ClienteProveedor = reader.Get<int>("No_clientProv"),
                DiasCobertura = reader.Get<int>("dias_CobAdicional"),
                LeadTime = reader.Get<int>("lead_time"),
                Lunes = reader.Get<int>("lunes"),
                Martes = reader.Get<int>("martes"),
                Miercoles = reader.Get<int>("miercoles"),
                Jueves = reader.Get<int>("jueves"),
                Viernes = reader.Get<int>("viernes"),
                Sabado = reader.Get<int>("sabado"),
                Domingo = reader.Get<int>("domingo"),
                NomProveedor = reader.Get<string>("NomProv"),
                NomAgencia = reader.Get<string>("NomAgencia"),
                Calendario = reader.Get<string>("Calendario"),
            };
        }

        public static CadenasSucursalesModel ToCadenaSucursales(this IDataReader reader)//
        {
            return new CadenasSucursalesModel
            {
                ID = reader.Get<string>("Id"),
                Valor = reader.Get<string>("Valor")
            };
        }

        public static LogErrorPermisoCompraModel ToLogErrorPermisoCompra(this IDataReader reader)
        {
            return new LogErrorPermisoCompraModel
            {
                IdSucursal = reader.Get<int>("IdSucursal"),
                SKU = reader.Get<string>("SKU"),
                oEx = reader.Get<string>("oEx"),
            };
        }

        public static GruposListViewsModel ToGrupoListCompra(this IDataReader reader)
        {
            return new GruposListViewsModel
            {
                ID = reader.Get<int>("idGrupo"),
                Valor = reader.Get<string>("grupo")
            };
        }

        public static ExcepcionesInvenadroModel ToConsultaExcepciones(this IDataReader reader)
        {
            return new ExcepcionesInvenadroModel
            {
                IdSucursal = reader.Get<int>("Sucursal"),
                NombreSucursal = reader.Get<string>("NombreSucursal"),
                SKU = reader.Get<string>("SKU"),
                DescripcionSKU = reader.Get<string>("DescripcionSKU"),
                FechaInicio = reader.Get<string>("FechaInicio"),
                FechaFin = reader.Get<string>("FechaFin"),
                UsuarioRealizo = reader.Get<string>("UsuarioRealizo"),

            };
        }

        public static TopeDeCompraModel ToConsultaTopeRec(this IDataReader reader)
        {
            return new TopeDeCompraModel
            {
                IdSucursal = reader.Get<int>("Sucursal"),
                Sucursal = reader.Get<string>("NombreSucursal"),
                SKU = reader.Get<string>("SKU"),
                DescripcionSKU = reader.Get<string>("DescripcionSKU"),
                Limite = reader.Get<int>("Limite"),
                Concepto = reader.Get<string>("Concepto"),
                Motivo = reader.Get<string>("Motivo"),
                Usuario = reader.Get<string>("UsuarioRealizo"),

            };
        }
        public static TopeDeCompraModel ToConsultaTope (this IDataReader reader)
        {
            return new TopeDeCompraModel
            {
                IdSucursal = reader.Get<int>("IdSucursal"),
                Sucursal = reader.Get<string>("Sucursal"),
                SKU = reader.Get<string>("SKU"),
                DescripcionSKU = reader.Get<string>("DescripcionSKU"),
                Limite = reader.Get<int>("Limite"),
                FechaInicio = reader.Get<string>("FechaInicio"),
                FechaFin = reader.Get<string>("FechaFin"),
                Concepto = reader.Get<string>("Concepto"),
                Usuario = reader.Get<string>("Usuario"),

            };
        }

        public static PedidosConsultaModel ToPedidosListCompra(this IDataReader dr)
        {
            return new PedidosConsultaModel
            {
                /* Folio = dr.Get<string>("folio"),
                 Proveedor = dr.Get<string>("Nombre"),
                 FechaOC = dr.Get<DateTime>("fecha_oc"),
                 FechaCarga = dr.Get<DateTime>("fecha_carga"),
                 Estatus = dr.Get<string>("Estatus"),
                 Tipo = dr.Get<string>("TipoDesc"),
                 Piezas = dr.Get<int>("Piezas"),
                 Id_Suc = dr.Get<int>("id_suc"),
                 IdMantto = dr.Get<int>("idMantto"),
                 IdSucursal = dr.Get<int>("idSucursal"),
                 IdGrupo = dr.Get<int>("idGrupo"),
                 Invenadro = dr.Get<bool>("aplicaINvenadro"),
                 Negados = dr.Get<bool>("aplicaNegados"),
                 CodigoSucursalFT = dr.Get<int>("codigoSucursalFT")*/
                Folio = dr.Get<long>("Folio"),
                Id_proveedor = dr.Get<int>("Id_proveedor"),
                Proveedor = dr.Get<string>("Proveedor").Replace(" ", ""),
                Fecha_Carga = dr.Get<string>("Fecha_Carga"),
                Fecha_Aplicacion = dr.Get<string>("Fecha_Aplicacion"),
                IdSucursal = dr.Get<int>("IdSucursal"),
                Estatus = dr.Get<string>("Estatus"),
                Id_tipo = dr.Get<int>("Id_tipo"),
                Tipo = dr.Get<string>("Tipo"),
                Importe = dr.Get<decimal>("Importe"),
                Piezas = dr.Get<int>("Cantidad")
            };
        }


        public static ConsultaArticuloSucursalModel ToResumenCargaArticulosSucursal(this IDataReader reader)
        {
            return new ConsultaArticuloSucursalModel
            {
                ID_Sucursal = reader.Get<int>("ID_Sucursal"),
                SKU = reader.Get<string>("SKU"),
                DESCRIPCION_CORTA = reader.Get<string>("DESCRIPCION_CORTA"),
                GRUPO = reader.Get<string>("GRUPO"),
                FAMILIA = reader.Get<string>("FAMILIA"),
                Existencia = reader.Get<string>("Existencia"),
                PVD = reader.Get<string>("PVD"),
                Fecha_Venta = reader.Get<string>("Fecha_Venta"),
                piezas_total = reader.Get<string>("piezas_total"),


            };
        }

        public static ConsultaSucursales ToResumenCargaSucursales(this IDataReader reader)
        {
            return new ConsultaSucursales
            {
                Cadena = reader.Get<string>("Cadena"),
                Marca = reader.Get<string>("Marca"),
                Suc_Id = reader.Get<string>("Suc_Id"),
                Suc_Nombre = reader.Get<string>("Suc_Nombre"),
                Licencia = reader.Get<string>("Licencia"),
                Sdom = reader.Get<string>("Sdom"),
                hrs = reader.Get<string>("hrs"),
                INVENADRO = reader.Get<string>("INVENADRO"),
                ESTATUS = reader.Get<string>("ESTATUS"),
                CIUDAD = reader.Get<string>("CIUDAD"),
                ESTADO = reader.Get<string>("ESTADO"),


            };
        }

        public static ComboSucursalesProveedor ToResumenCargaSucursalesProveedores(this IDataReader reader)
        {
            return new ComboSucursalesProveedor
            {
                Nombre_Marca = reader.Get<string>("Nombre_Marca"),
                CodigoSucursal = reader.Get<string>("CodigoSucursal"),
                Suc_Nombre = reader.Get<string>("Suc_Nombre"),
                Proveedor = reader.Get<string>("Proveedor"),
                Agencia = reader.Get<string>("Agencia"),
                lunes = reader.Get<string>("lunes"),
                martes = reader.Get<string>("martes"),
                miercoles = reader.Get<string>("miercoles"),
                jueves = reader.Get<string>("jueves"),
                viernes = reader.Get<string>("viernes"),
                sabado = reader.Get<string>("sabado"),
                domingo = reader.Get<string>("domingo"),


            };
        }


        public static OCSugerido ToResumenCargaOC(this IDataReader reader)
        {
            return new OCSugerido
            {
                idSugerido = reader.Get<string>("idSugerido"),
                Orden_Compra = reader.Get<string>("Orden_Compra"),
                id_Sucursal = reader.Get<string>("id_Sucursal"),
                Nombre_Sucursal = reader.Get<string>("Nombre_Sucursal"),
                Nombre_Proveedor = reader.Get<string>("Nombre_Proveedor"),
                Importe = reader.Get<string>("Importe"),
                status = reader.Get<string>("status")

            };
        }

        public static PedidosCargados ToPedidosCargados(this IDataReader dr)
        {
            return new PedidosCargados
            {
                Folio = dr.Get<Int64>("folio"),
                NombreArchivo = dr.Get<string>("nombreArchivo"),
                TipoPedido = dr.Get<string>("Nombre"),
                LineasPedido = dr.Get<int>("lineas_pedido"),
                LineasCargadas = dr.Get<int>("lineas_cargadas"),
                Estatus = dr.Get<string>("Estatus"),
            };
        }

        public static Rechazados ToRechazados(this IDataReader dr)
        {
            return new Rechazados
            {
                Folio = dr.Get<Int64>("folio"),
                Proveedor = dr.Get<string>("Nombre"),
                FechaCarga = dr.Get<string>("fecha_carga"),
                FechaAplicacion = dr.Get<string>("fecha_aplicacion"),
                Tipo = dr.Get<string>("Tipo"),
                Farmacia = dr.Get<string>("descripcion"),
                SKU = dr.Get<string>("SKU"),
                Descripcion = dr.Get<string>("DESCRIPCION_CORTA"),
                TipoIncidencia = dr.Get<string>("tipo_incidencia")
            };
        }


        public static ComboProveedor ToComboProveedorReporte(this IDataReader reader)
        {
            return new ComboProveedor
            {
                id_proveedor = reader.Get<int>("id_proveedor"),
                Nombre = reader.Get<string>("Nombre")


            };
        }

        public static ListaSugeridoModel ToListaSugerido(this IDataReader reader)
        {
            return new ListaSugeridoModel
            {
                IDSugerido = reader.Get<string>("idSugerido"),
                FechaCalculo = reader.Get<string>("FechaCalculo"),
                Estatus = reader.Get<string>("Estatus"),
                Negado = reader.Get<bool>("negados")
            };
        }

        public static ValidacionMontoSugeridoModel ToListMontosValidar(this IDataReader reader)
        {
            return new ValidacionMontoSugeridoModel
            {

                Id = reader.Get<int>("Id"),
                IdSugerido = reader.Get<long>("IdSugerido"),
                IdSucursal = reader.Get<int>("IdSucursal"),
                Sucursal = reader.Get<string>("Sucursal"),
                MontoPromedio = reader.Get<decimal>("MontoPromedio"),
                MontoPedido = reader.Get<decimal>("MontoPedido"),
                MontoFueraLimite = reader.Get<decimal>("MontoFueraLimite"),
                PorcentajeVariacion = reader.Get<decimal>("PorcentajeVariacion"),
                Autorizado = reader.Get<bool>("Autorizado"),
                PiezasXFarmacia = reader.Get<int>("PiezasXFarmacia")
            };
        }

        public static Combo3 ToCombo3(this IDataReader reader)
        {
            return new Combo3
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),
                IdFiltro = reader.Get<int>("Filtro")
            };
        }

        public static Combo4 ToComboInv3(this IDataReader reader)
        {
            return new Combo4
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),
                Filtro = reader.Get<string>("Filtro")
            };
        }

        public static ConsultaInvenadro ToComboInvConsulta(this IDataReader reader)
        {
            return new ConsultaInvenadro
            {
                Sucursal = reader.Get<string>("Sucursal"),
                SKU = reader.Get<string>("SKU"),
                Descripcion = reader.Get<string>("Descripcion"),
                Grupo = reader.Get<string>("Grupo"),
                Familia = reader.Get<string>("Familia"),
                Existencia = reader.Get<int>("Existencia"),
                Invenadro = reader.Get<int>("Invenadro")
            };
        }


        public static ComboGenerico ToComboSucursalCadena(this IDataReader reader)
        {
            return new ComboGenerico
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor")


            };
        }

        public static ReporteArticulosProveedorModel ToProveedorAgenciaProd(this IDataReader reader)
        {
            return new ReporteArticulosProveedorModel
            {
                IdProveedor = reader.Get<int>("PROVEEDOR_ID"),
                IdAgencia = reader.Get<int>("AGENCIA_ID"),
                Sku = reader.Get<string>("SKU"),
                DescripcionCorta = reader.Get<string>("DescripcionCorta"),
                Existencia = reader.Get<int>("Existencia"),
                Costo = reader.Get<decimal>("Costo"),
                Controlado = reader.Get<string>("Controlado"),
                Refrigerado = reader.Get<string>("Refrigerado"),
                PMP = reader.Get<decimal>("PMP"),
                Empaque = reader.Get<int>("Empaque")

            };
        }

        public static ComboGenerico ToComboSucursalGrupo(this IDataReader reader)
        {
            return new ComboGenerico
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor")


            };
        }

        public static Combo4 ToCombo4(this IDataReader reader)
        {
            return new Combo4
            {
                Id = reader.Get<int>("Id"),
                Valor = reader.Get<string>("Valor"),
                Filtro = reader.Get<string>("Filtro")
            };
        }

        public static LogInvenadro ToLogInvenadro(this IDataReader reader)
        {
            return new LogInvenadro
            {
                Sucursal = reader.Get<int>("SUC_ID"),
                SKU = reader.Get<String>("SKU"),
                Descripcion = reader.Get<string>("descripcion"),
                Optimo = reader.Get<int>("optimo"),
                Error = reader.Get<string>("ERROR"),
                Fecha = reader.Get<string>("fecha")
            };
        }

        public static LogRanking ToLogRanking(this IDataReader reader)
        {
            return new LogRanking
            {
                IdAgencia = reader.Get<int>("IdAgencia"),
                SKU = reader.Get<String>("SKU"),
                Fecha = reader.Get<string>("Fecha"),
                ERROR = reader.Get<string>("ERROR")
            };
        }
        public static ConteoRanking ToConteoRanking(this IDataReader reader)
        {
            return new ConteoRanking
            {
                Fecha = reader.Get<string>("Fecha"),
                TotalRegistros = reader.Get<int>("TotalRegistros"),
                Cargados = reader.Get<int>("Cargados"),
                Rechazados = reader.Get<int>("Rechazados")
            };
        }
        public static LogExclusiones ToLogExclusiones(this IDataReader reader)
        {
            return new LogExclusiones
            {
                IdProveedor = reader.Get<int>("idproveedor"),
                Proveedor = reader.Get<String>("proveedor"),
                SKU = reader.Get<string>("sku"),
                Descripcion = reader.Get<string>("descripcioncorta"),
                Laboratorio = reader.Get<string>("laboratorio"),
                Error = reader.Get<string>("error")
            };
        }

        public static SugeridoLogErrorModel ToListLogError(this IDataReader reader)
        {
            return new SugeridoLogErrorModel
            {
                FechaCarga = reader.Get<string>("FechaCarga"),
                Fecha_Validacion = reader.Get<string>("Fecha_Validacion"),
                Motivo_Negado = reader.Get<string>("Motivo_Negado"),
                Sucursal = reader.Get<string>("Sucursal"),
                SKU = reader.Get<string>("SKU"),
                Descripcion = reader.Get<string>("Descripcion"),
                Piezas_rechazadas = reader.Get<int>("Piezas_rechazadas"),
                Incidencia = reader.Get<string>("Incidencia"),



            };
        }

        public static SugeridoCuenta ToListDTCuenta(this IDataReader reader)
        {
            return new SugeridoCuenta
            {
                idSugerido = reader.Get<Int64>("idSugerido"),
                idSucursal = reader.Get<int>("idSucursal"),
                id_proveedor = reader.Get<int>("id_proveedor"),
                CADENA = reader.Get<string>("CADENA"),
                RAZON_SOCIAL = reader.Get<string>("RAZON_SOCIAL"),
                Desc_tienda = reader.Get<string>("Desc_tienda"),
                Desc_proveedor = reader.Get<string>("Desc_proveedor"),
            };
        }

        public static ReporteNegados ToListDTRptNegados(this IDataReader reader)
        {
            return new ReporteNegados
            {
                FechaCarga = reader.Get<string>("FechaCarga"),
                FechaValidacion = reader.Get<string>("FechaValidacion"),
                MotivoNegado = reader.Get<string>("MotivoNegado"),
                IdSucursal = reader.Get<string>("IdSucursal"),
                NumSucursal = reader.Get<string>("NumSucursal"),
                Nombre = reader.Get<string>("Nombre"),
                SKU = reader.Get<string>("SKU"),
                DESCRIPCION = reader.Get<string>("DESCRIPCION"),
                CantidadAceptada = reader.Get<int>("CantidadAceptada"),
                CantidadNegada = reader.Get<int>("CantidadNegada"),
                Incidencia = reader.Get<string>("Incidencia"),

            };
        }

        public static ListadoSugerido ToListadoSugerido(this IDataReader reader)
        {
            return new ListadoSugerido
            {
                FechaCreacionSugerido = reader.Get<string>("FechaCreacionSugerido"),
                IdSugerido = reader.Get<Int64>("IdSugerido"),
                IdSucursal = reader.Get<int>("IdSucursal"),
                Sucursal = reader.Get<string>("Sucursal"),
                Proveedor = reader.Get<string>("Proveedor"),
                Articulo_Id = reader.Get<string>("Articulo_Id"),
                Producto = reader.Get<string>("Producto"),
                CostoNeto = reader.Get<decimal>("CostoNeto"),
                TipoPedido = reader.Get<string>("TipoPedido"),
                ExistenciaPza = reader.Get<int>("ExistenciaPza"),
                TransitoPza = reader.Get<int>("TransitoPza"),
                SugeridoInicial = reader.Get<decimal>("SugeridoInicial"),
                NegadosPza = reader.Get<int>("NegadosPza"),
                CompraEspecialPza = reader.Get<int>("CompraEspecialPza"),
                InvenadroPza = reader.Get<int>("InvenadroPza"),
                Capacity = reader.Get<int>("Capacity"),
                PedEspFarmacia = reader.Get<int>("PedEspFarmacia"),
                PolABCDias = reader.Get<int>("PolABCDias"),
                ABC = reader.Get<string>("ABC"),
                TipoCalculo = reader.Get<string>("TipoCalculo"),
                FactorEmpaque = reader.Get<int>("FactorEmpaque"),
                PedidoFinal = reader.Get<decimal>("PedidoFinal"),
                PedidoFinalMod = reader.Get<decimal>("PedidoFinalMod"),
                PVD = reader.Get<decimal>("pvd")

            };
        }

        public static SugeridoAplicadoModel ToListaSugeridoAplicado(this IDataReader reader)
        {
            return new SugeridoAplicadoModel
            {
                IdSugerido = reader.Get<long>("IdSugerido"),
                //  IdSucursal = reader.Get<int>("IdSucursal"),
                //Nombre = reader.Get<string>("Nombre"),
                IdProveedor = reader.Get<int>("IdProveedor"),
                Grupo = reader.Get<string>("Grupo"),
                Proveedor = reader.Get<string>("Proveedor"),
                IdGrupo = reader.Get<int>("IdGrupo"),

                // pcio_Costo = reader.Get<decimal>("pcio_Costo"),
                //CostoCalculado = reader.Get<decimal>("CostoCalculado")

            };
        }

        public static OrdenCompraModel ToListaOrdenCompra(this IDataReader reader)
        {
            return new OrdenCompraModel
            {
                IdSugerido = reader.Get<long>("IdSugerido"),
                Orden_Compra = reader.Get<int>("Orden_Compra"),
                Id_Sucursal = reader.Get<int>("Id_Sucursal"),
                Nombre_Sucursal = reader.Get<string>("Nombre_Sucursal"),
                IdProveedor = reader.Get<int>("IdProveedor"),
                Proveedor = reader.Get<string>("Proveedor"),
                Importe = reader.Get<decimal>("Importe"),
                Estatus = reader.Get<string>("Estatus"),


            };
        }


        public static DetalladoSugerido ToDetalladoSugerido(this IDataReader reader)
        {
            return new DetalladoSugerido
            {
                FechaSugerido = reader.Get<string>("FechaSugerido"),
                IdSugerido = reader.Get<Int64>("IdSugerido"),
                IdSucursal = reader.Get<int>("IdSucursal"),
                Sucursal = reader.Get<string>("Sucursal"),
                Proveedor = reader.Get<string>("Proveedor"),
                Articulo_Id = reader.Get<string>("Articulo_Id"),
                Producto = reader.Get<string>("Producto"),
                CostoNeto = reader.Get<decimal>("CostoNeto"),
                TipoPedido = reader.Get<string>("tipoPedido"),
                Cantidad = reader.Get<decimal>("sugerido")
            };
        }

        public static ResumenEnt ToResumenSugerido(this IDataReader reader)
        {
            return new ResumenEnt
            {
                IdSucursal = reader.Get<int>("IDSUCURSAL"),
                Sucursal = reader.Get<string>("SUCURSAL"),
                Proveedor = reader.Get<string>("PROVEEDOR"),
                Monto = reader.Get<string>("COSTO"),
                Piezas = reader.Get<string>("PIEZAS"),
                TipoCompra = reader.Get<string>("TIPOPEDIDO")

            };
        }

        public static ConsultaProductosModel ToConsultaProductos(this IDataReader dr)
        {
            return new ConsultaProductosModel
            {
                SKU = dr.Get<string>("SKU"),
                Descripcion = dr.Get<string>("Descripcion"),
                GRUPO = dr.Get<string>("Grupo"),
                Familia = dr.Get<string>("Familia"),
                FORMA_SURTIR_FR = dr.Get<string>("FORMA_SURTIR_FR"),
                FORMA_SURTIR_LD = dr.Get<string>("FORMA_SURTIR_LD"),
                FORMA_SURTIR_SSAE = dr.Get<string>("FORMA_SURTIR_SSAE"),
                ESTATUS_PRODUCTO = dr.Get<string>("ESTATUS_PRODUCTO"),
                Empaque = dr.Get<string>("Empaque"),
                Controlado = dr.Get<string>("Controlado"),
                Refrigerado = dr.Get<string>("Refrigerado"),
                AltaEspecialidad = dr.Get<string>("AltaEspecialidad"),
                DERECHO_DEVOLUCION_CADUCIDAD = dr.Get<string>("DERECHO_DEVOLUCION_CADUCIDAD"),
                DERECHO_DEVOLUCION_PROVEEDOR = dr.Get<string>("DERECHO_DEVOLUCION_PROVEEDOR"),
                DERECHO_DEVOLUCION_VENTA = dr.Get<string>("DERECHO_DEVOLUCION_VENTA"),
                Invenadro = dr.Get<string>("Invenadro")
                //PROVEEDOR = dr.Get<string>("PROVEEDOR"),
                //FACTOR_EMPAQUE = dr.Get<string>("FACTOR_EMPAQUE"),
                //AGRUPADOR_ARTICULO = dr.Get<string>("AGRUPADOR_ARTICULO"),
                //CATEGORIA = dr.Get<string>("CATEGORIA"),
                //GRUPO = dr.Get<string>("GRUPO"),
                //IEPS = dr.Get<string>("IEPS"),
                //IVA = dr.Get<string>("IVA"),
                //MARCA = dr.Get<string>("MARCA"),
                //PRESENTACION = dr.Get<string>("PRESENTACION"),
                //TIPO_PRODUCTO = dr.Get<string>("TIPO_PRODUCTO"),
                //ESTATUS_PRODUCTO = dr.Get<string>("ESTATUS_PRODUCTO")

            };
        }

        public static ReporteArticulosSucursalModel ToArticulosSucursal(this IDataReader reader)
        {
            return new ReporteArticulosSucursalModel
            {
                ID_Sucursal = reader.Get<int>("ID_Sucursal"),
                SKU = reader.Get<string>("SKU"),
                DESCRIPCION_CORTA = reader.Get<string>("DescripcionCorta"),
                GRUPO = reader.Get<string>("GRUPO"),
                FAMILIA = reader.Get<string>("FAMILIA"),
                Estatus = reader.Get<string>("Estatus"),
                Existencia = reader.Get<string>("Existencia"),
                Transito = reader.Get<string>("Transito"),
                PVD = reader.Get<string>("PVD"),
                Invenadro = reader.Get<string>("Invenadro"),
                RotABC = reader.Get<string>("RotABC"),
                PoliticaDias = reader.Get<string>("PoliticaDias"),
                FechaActivo = reader.Get<string>("FechaActivo"),
                FechaInactivo = reader.Get<string>("FechaInactivo"),
                //piezas_total = reader.Get<string>("piezas_total"),
                piezas_total = reader.Get<string>("monto"),
                Capacity = reader.Get<int>("Capacity"),
                Motivo = reader.Get<string>("Motivo"),
                TipoProveedor = reader.Get<string>("TipoProveedor")
            };
        }

        public static EstadisticaCompras ToEstadisticaCompras(this IDataReader reader)
        {
            return new EstadisticaCompras
            {
                SucId = reader.Get<int>("idsucursal"),
                Sucursal = reader.Get<string>("sucursal"),
                Existencia = reader.Get<int>("existencia"),
                Transito = reader.Get<int>("transito"),
                PVD = reader.Get<decimal>("pvd"),
                ABC = reader.Get<string>("abc"),
                PoliticaDias = reader.Get<int>("politica"),
                Invenadro = reader.Get<int>("invenadro"),
                StockMin = reader.Get<int>("stock"),
                Activo = reader.Get<string>("activo"),
                FechaUltimaVenta = reader.Get<string>("fecha")
            };
        }

        public static EstadisticaProveedor ToEstadisticaProveedor(this IDataReader reader)
        {
            return new EstadisticaProveedor
            {
                Agencia = reader.Get<string>("agencia"),
                Descripcion = reader.Get<string>("Producto"),
                Existencia = reader.Get<int>("existencia"),
                Empaque = reader.Get<int>("empque"),
                Costo = reader.Get<decimal>("costo")
            };
        }

        public static EstadisticaVenta28dias ToEstadisticaVenta28dias(this IDataReader reader)
        {
            return new EstadisticaVenta28dias
            {
                Dia = reader.Get<int>("dia"),
                Contado = reader.Get<int>("credito"),
                Sad = reader.Get<int>("sad"),
                Total = reader.Get<int>("total")
            };
        }

        public static AutocompleteString ToAutocompleteString(this IDataReader reader)
        {
            return new AutocompleteString
            {
                Id = reader.Get<string>("ID"),
                Valor = reader.Get<string>("VALOR")
            };
        }

        public static DetalleSugeridoMontos ToDetalleSugeridoMontos(this IDataReader reader)
        {
            return new DetalleSugeridoMontos
            {
                MontosPedido = reader.Get<decimal>("monto"),
                MontoPiezas = reader.Get<decimal>("piezas"),
                MontoFueraLimite = reader.Get<decimal>("fueraLimite")
            };
        }

        public static ResumenRechazos ToResumenRechazos(this IDataReader reader)
        {
            return new ResumenRechazos
            {
                IdSucursal = reader.Get<int>("idsucursal"),
                Sucursal = reader.Get<string>("sucursal"),
                SKU = reader.Get<string>("sku"),
                Descripcion = reader.Get<string>("descripcion"),
                Configuracion = reader.Get<int>("configuracion"),
                Descontinuado = reader.Get<int>("descontinuado"),
                Empaque = reader.Get<int>("empaque"),
                Licencia = reader.Get<int>("licencia"),
                NoPublicado = reader.Get<int>("nopublicado"),
                Regla_Compra_Esp = reader.Get<int>("reglacompraesp"),
                Regla_Negados = reader.Get<int>("reglanegados"),
                Sku_Inactivo = reader.Get<int>("skuinactivo"),
            };
        }


        public static PermisoArticuloSucursalModel ToPermisosArticuloSucursal(this IDataReader reader)
        {
            return new PermisoArticuloSucursalModel
            {
                Cadena = reader.Get<string>("Cadena"),
                IdSucursal = reader.Get<int>("IdSucursal"),
                Sucursal = reader.Get<string>("Sucursal"),
                SKU = reader.Get<string>("SKU"),
                DESCRIPCION = reader.Get<string>("DESCRIPCION"),
                ActivoCompra = reader.Get<string>("ActivoCompra"),
                ActivoVenta = reader.Get<string>("ActivoVenta")
            };
        }

        public static ReporteInvenadroAutModel ToReporteInvenadroAut(this IDataReader reader)
        {
            return new ReporteInvenadroAutModel
            {
                IdSucursal = reader.Get<int>("IdSucursal"),
                Sucursal = reader.Get<string>("Sucursal"),
                SKU = reader.Get<string>("SKU"),
                Descripcion = reader.Get<string>("Descripcion"),
                PVD = reader.Get<decimal?>("PVD"),
                InventarioObjetivo = reader.Get<decimal?>("InventarioObjetivo"),
                MontoObjetivo = reader.Get<decimal?>("MontoObjetivo"),
                OptimoAnterior = reader.Get<int?>("OptimoAnterior"),
                InvMontoAnterior = reader.Get<decimal?>("InvMontoAnterior"),
                OptimoActualizado = reader.Get<int?>("OptimoActualizado"),
                InvMontoNuevo = reader.Get<decimal?>("InvMontoNuevo"),
                RelacionInvenadroVenta = reader.Get<string>("RelacionInvenadroVenta"),
                EstatusProducto = reader.Get<string>("EstatusProducto"),
                PorcDifPiezas = reader.Get<decimal?>("PorcDifPiezas"),
                PorcDifMonto = reader.Get<decimal?>("PorcDifMonto")
            };
        }
    }
}


