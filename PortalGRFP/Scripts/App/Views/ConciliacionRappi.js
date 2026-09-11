$(function () {
    'use strict';

    var imagen = new Image();

    var $conciliacionManualModal = $('#conciliacionManualModal'),
        $hdnOrderId = $('#hdnOrderId'),
        $hdnOperacion = $('#hdnOperacion'),
        $hdnIdSucursal = $('#hdnIdSucursal'),
        $hdnImporteTotal = $('#hdnImporteTotal'),
        $imgTicket = $('#imgTicket');

    function conciliacionManual_onClick(e) {
        $hdnOrderId.val($(this).attr('data-OrderId'));
        $hdnOperacion.val($(this).attr('data-Operacion'));
        $hdnIdSucursal.val($(this).attr('data-IdFarmacia'));
        $hdnImporteTotal.val($(this).attr('data-ImporteTotal'));

        var datos = new FormData();
        datos.append("cadena", $("#ddlCadena").val());
        ObtenerFarmacias(datos);
        $("#txtTicket").val("");
        $('#tblConciliar').empty();
        $imgTicket.attr("src", $(this).attr('data-urlTicket'));
        imageZoom("imgTicket", "myresult");
        $("#btnConsultar").css("display", "block");
        $("#btnAplica").css("display", "none");
        $("#dvIncidencia").css("display", "none");
        $("#hdnBandera").val("0");
        $conciliacionManualModal.modal('show');
    }

    var $conciliacionCardModal = $('#conciliacionCardModal'),
        $hdnRazonsCard = $('#hdnRazons'),
        $hdnOperacionCard = $('#hdnOperacion'),
        $hdnIdSucursalCard = $('#hdnIdSucursal'),
        $hdnImporteTotalCard = $('#hdnImporteTotal'),
        $hdnIdRazonCard = $("#hdnIdRazon"),
        $tblTarjeta = $('#tblConciliarCard');


    function conciliacionCard_onClick(e) {
        $hdnRazonsCard.val($(this).attr('data-Razons'));
        $hdnOperacionCard.val($(this).attr('data-Operacion'));
        $hdnIdSucursalCard.val($(this).attr('data-IdFarmacia'));
        $hdnImporteTotalCard.val($(this).attr('data-ImporteTotal'));
        $hdnIdRazonCard.val($("#ddlCadena option:contains(" + $hdnRazonsCard.val() + ")").val());

        var datos = new FormData();
        datos.append("cadena", $("#ddlCadena option:contains(" + $hdnRazonsCard.val() + ")").val());
        datos.append("farmacia", $hdnIdSucursalCard.val());
        datos.append("fecha", $hdnOperacionCard.val());
        datos.append("importe", $hdnImporteTotalCard.val())

        $('#tblConciliarCard').empty();
        ConsultarMonto(datos);
        $("#dvIncidenciacard").css("display", "none");
        $("#hdnBanderacard").val("0");
        $conciliacionCardModal.modal('show');
    }

    function btnAplicaCard_onClick(e) {
        showLoading();
        var fecha = $hdnOperacion.val().substring(0, 10);
        var fecha2 = fecha.split('/')[2] + "-" + fecha.split('/')[1] + "-" + fecha.split('/')[0];

        var renglon = $tblTarjeta.rows('.selected').data();
        var _ticket = renglon.context[0].aoData[0]._aData.Ticket;

        //var c = renglon.cell(node).node().val();
        var manual = new FormData();
        manual.append("razons", $hdnIdRazonCard.val());
        manual.append("operacion", fecha2);
        manual.append("idSucursal", $hdnIdSucursal.val());
        manual.append("importeTotal", $hdnImporteTotal.val());
        manual.append("accion", "0");
        manual.append("incidencia", "0");
        manual.append("observacion", "NA");
        manual.append("ticket", _ticket);

        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'ConciliacionManualCard',
            data: manual,
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();
                if (response.Success) {
                    $conciliacionCardModal.modal('hide');
                    swal(response.Message, '', "success").then((value) => {
                        window.location.href = "/ConciliacionRappi/Conciliaciones?anio=" + $("#hndAnio").val() + "&mes=" + $("#hndMes").val();
                    });
                } else {
                    swal(response.Message, '', "error");
                }
            },
            error: function (e) {
                hideLoading();
                swal("¡Error inesperado en la conciliación!", '', "error");
            }

        });
    }

    function btnNoAplicaCard_onClick(e) {
        if ($("#hdnBanderacard").val() == "0") {
            $("#dvIncidenciacard").css("display", "block");
            $("#hdnBanderacard").val("1");
            $("#txtObservacionCard").val("");
            ObtenerIncidencias("1");
        } else if ($("#hdnBanderacard").val() == "1") {
            swal({
                title: '¿Estas seguro?',
                text: "Revise la informacón si no desea aplicar la conciliación",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        showLoading();
                        var fecha = $hdnOperacion.val().substring(0, 10);
                        var fecha2 = fecha.split('/')[2] + "-" + fecha.split('/')[1] + "-" + fecha.split('/')[0]

                        var manual = new FormData();
                        manual.append("razons", $hdnIdRazonCard.val());
                        manual.append("operacion", fecha2);
                        manual.append("idSucursal", $hdnIdSucursalCard.val());
                        manual.append("importeTotal", $hdnImporteTotalCard.val());
                        manual.append("accion", "1");
                        manual.append("incidencia", $("#ddlIncidenciacard").val());
                        manual.append("observacion", $("#txtObservacioncard").val());
                        manual.append("ticket", "");

                        $.ajax({
                            type: "POST",
                            url: getAbsolutePath() + 'ConciliacionManualCard',
                            data: manual,
                            dataType: "json",
                            processData: false,  // tell jQuery not to process the data
                            contentType: false,
                            success: function (response) {
                                hideLoading();
                                if (response.Success) {
                                    $conciliacionCardModal.modal('hide');
                                    $("#dvIncidenciacard").css("display", "none");
                                    $("#hdnBanderacard").val("0");
                                    swal(response.Message, '', "success").then((value) => {
                                        window.location.href = "/ConciliacionRappi/Conciliaciones?anio=" + $("#hndAnio").val() + "&mes=" + $("#hndMes").val();
                                    });
                                } else {
                                    swal(response.Message, '', "error");
                                }
                            },
                            error: function (e) {
                                hideLoading();
                                swal("¡Error inesperado en la conciliación!", '', "error");
                            }

                        });
                    }
                });
        }
    }


    $("#chkPorMonto").click(function () {
        if ((this).checked) {
            $("#btnConsultar").css("display", "none");
            $("#btnAplica").css("display", "block");
        } else {
            $("#btnConsultar").css("display", "block");
            $("#btnAplica").css("display", "none");
        }
    });

    $('#tblConciliarCard tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');

        }
        else {
            $tblTarjeta.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    ;

    function ConsultarMonto(datos) {
        showLoading();

        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'MontoConciliar',
            data: datos,
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();
                $tblTarjeta = $('#tblConciliarCard').DataTable({
                    data: response.data,
                    destroy: true,
                    dom: 'Bfrtip',
                    columns: [
                        { data: "Ticket", title: 'Ticket' },
                        { data: "DiaOperacion", title: "Fecha" },
                        { data: "Sucursal", title: "Farmacia" },
                        { data: "Subtotal", title: 'SubTotal' },
                        { data: "Iva", title: 'Iva' },
                        { data: "Total", title: 'Total' }
                    ],
                    "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        var FechaInicio = new Date(parseInt(aData.DiaOperacion.replace("/Date(", "").replace(")/", ""), 10));
                        $('td:eq(1)', nRow).html(FechaInicio.getDate() + "/" + (FechaInicio.getMonth() + 1) + "/" + FechaInicio.getFullYear());

                    },
                    scrollCollapse: false,
                    info: false,
                    paging: false,
                    //responsive: true,
                    searching: false

                });

                if (response.data.length > 0) {
                    $("#btnConsultar").css("display", "none");
                    $("#btnAplica").css("display", "block");
                }
            },
            error: function (response) {
            }
        });
    }

    function btnAplica_onClick(e) {
        showLoading();
        $("#btnConsultar").css("display", "block");
        $("#btnAplica").css("display", "none");
        var fecha = $hdnOperacion.val().substring(0, 10);
        var fecha2 = fecha.split('/')[2] + "-" + fecha.split('/')[1] + "-" + fecha.split('/')[0]

        var manual = new FormData();
        manual.append("orderId", $hdnOrderId.val());
        manual.append("operacion", fecha2);
        manual.append("idSucursal", $hdnIdSucursal.val());
        manual.append("importeTotal", $hdnImporteTotal.val());
        manual.append("accion", "0");
        manual.append("incidencia", "0");
        manual.append("observacion", "NA");
        if ((this).checked) {
            manual.append("ticket", '0');
        } else {
            manual.append("ticket", $("#txtTicket").val());
        }


        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'ConciliacionManual',
            data: manual,
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();
                if (response.Success) {
                    $conciliacionManualModal.modal('hide');
                    swal(response.Message, '', "success").then((value) => {
                        window.location.href = "/ConciliacionRappi/Conciliaciones?anio=" + $("#hndAnio").val() + "&mes=" + $("#hndMes").val();
                    });
                } else {
                    swal(response.Message, '', "error");
                }
            },
            error: function (e) {
                hideLoading();
                swal("¡Error inesperado en la conciliación!", '', "error");
            }

        });
    }

    function btnNoAplica_onClick(e) {
        if ($("#hdnBandera").val() == "0") {
            $("#dvIncidencia").css("display", "block");
            $("#hdnBandera").val("1");
            $("#txtObservacion").val("");
            ObtenerIncidencias("0");
        } else if ($("#hdnBandera").val() == "1") {
            swal({
                title: '¿Estas seguro?',
                text: "Revise la informacón si no desea aplicar la conciliación",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        showLoading();
                        var fecha = $hdnOperacion.val().substring(0, 10);
                        var fecha2 = fecha.split('/')[2] + "-" + fecha.split('/')[1] + "-" + fecha.split('/')[0]

                        var manual = new FormData();
                        manual.append("orderId", $hdnOrderId.val());
                        manual.append("operacion", fecha2);
                        manual.append("idSucursal", $hdnIdSucursal.val());
                        manual.append("importeTotal", $hdnImporteTotal.val());
                        manual.append("accion", "1");
                        manual.append("incidencia", $("#ddlIncidencia").val());
                        manual.append("observacion", $("#txtObservacion").val());
                        manual.append("ticket", "NA");


                        $.ajax({
                            type: "POST",
                            url: getAbsolutePath() + 'ConciliacionManual',
                            data: manual,
                            dataType: "json",
                            processData: false,  // tell jQuery not to process the data
                            contentType: false,
                            success: function (response) {
                                hideLoading();
                                if (response.Success) {
                                    $conciliacionManualModal.modal('hide');
                                    $("#dvIncidencia").css("display", "none");
                                    $("#hdnBandera").val("0");
                                    swal(response.Message, '', "success").then((value) => {
                                        window.location.href = "/ConciliacionRappi/Conciliaciones?anio=" + $("#hndAnio").val() + "&mes=" + $("#hndMes").val();
                                    });
                                } else {
                                    swal(response.Message, '', "error");
                                }
                            },
                            error: function (e) {
                                hideLoading();
                                swal("¡Error inesperado en la conciliación!", '', "error");
                            }

                        });
                    }
                });

        }
    }

    function bindEvents() {
        $('.conciliacionManual').bind('click', conciliacionManual_onClick);
        $('.conciliacionCard').bind('click', conciliacionCard_onClick);
        $('#btnNoAplica').bind('click', btnNoAplica_onClick);
        $('#btnAplica').bind('click', btnAplica_onClick);
        $('#btnNoAplicaCard').bind('click', btnNoAplicaCard_onClick);
        $('#btnAplicaCard').bind('click', btnAplicaCard_onClick);

    }

    var conciRappi = (function () {
        return {
            initialize: function () {
                showLoading();
                bindEvents();
                imageZoom("imgTicket", "myresult");
                $('#tblConciliacionRappi').DataTable({
                    responsive: true,
                    scrollCollapse: true,
                    info: true,
                    paging: true,
                    language: {
                        searchPlaceholder: 'Buscar',
                        url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'
                    }
                });
                hideLoading();
            }
        };
    })();

    conciRappi.initialize();

    $("#ddlCadena").on("change", function () {

        var datos = new FormData();

        datos.append("cadena", $("#ddlCadena").val());
        ObtenerFarmacias(datos);
    });

    function ObtenerFarmacias(datos) {
        showLoading();
        $('#ddlFarmacia').empty();
        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'Obtenerfarmacias',
            data: datos,
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();
                var farmacias = "";
                for (var i = 0; i < response.data.length; i++) {
                    farmacias = farmacias + '<option value=' + response.data[i].Id + '>' + response.data[i].Valor + '</option>';
                }
                $('#ddlFarmacia').append(farmacias)
            },
            error: function (response) {
            }
        });
    }

    function ObtenerIncidencias(tipo) {
        showLoading();
        if (tipo == "0") {
            $('#ddlIncidencia').empty();
        } else {
            $('#ddlIncidenciacard').empty();
        }

        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'ObtenerCatalogoIncidencias',
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();
                var incidencias = "";
                for (var i = 0; i < response.data.length; i++) {
                    incidencias = incidencias + '<option value=' + response.data[i].Id + '>' + response.data[i].Valor + '</option>';
                }
                if (tipo == "0") {
                    $('#ddlIncidencia').append(incidencias)
                } else {
                    $('#ddlIncidenciacard').append(incidencias)
                }

            },
            error: function (response) {
            }
        });
    }

    $("#btnConsultar").on("click", function () {
        showLoading();
        if ($("#txtTicket").val() == "") {
            swal("Es requisito capturar el ticket de la imagen", '', "error")
        } else {

            var consultar = new FormData();
            consultar.append("cadena", $("#ddlCadena").val());
            consultar.append("farmacia", $("#ddlFarmacia").val());
            consultar.append("ticket", $("#txtTicket").val());

            $.ajax({
                type: "POST",
                url: getAbsolutePath() + 'TicketConciliar',
                data: consultar,
                dataType: "json",
                processData: false,  // tell jQuery not to process the data
                contentType: false,
                success: function (response) {
                    hideLoading();
                    $('#tblConciliar').DataTable({
                        data: response.data,
                        destroy: true,
                        dom: 'Bfrtip',
                        columns: [
                            { data: "DiaOperacion", title: "Fecha" },
                            { data: "Ticket", title: 'Ticket' },
                            { data: "Sucursal", title: "Farmacia" },
                            { data: "Subtotal", title: 'SubTotal' },
                            { data: "Iva", title: 'Iva' },
                            { data: "Total", title: 'Total' }
                        ],
                        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                            var FechaInicio = new Date(parseInt(aData.DiaOperacion.replace("/Date(", "").replace(")/", ""), 10));
                            $('td:eq(0)', nRow).html(FechaInicio.getDate() + "/" + (FechaInicio.getMonth() + 1) + "/" + FechaInicio.getFullYear());

                        },
                        scrollCollapse: false,
                        info: false,
                        paging: false,
                        //responsive: true,
                        searching: false

                    });

                    if (response.data.length > 0) {
                        $("#btnConsultar").css("display", "none");
                        $("#btnAplica").css("display", "block");
                    }
                },
                error: function (response) {
                }
            });
        }

    });


    function getAbsolutePath() {
        var loc = window.location;
        var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
        return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
    }
});

