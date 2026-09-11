$(function () {
    'use strict';

    var IndexRappi = (function () {
        return {
            initialize: function () {
                showLoading();
                //bindEvents();
                $('#tblConciliacionVentasMensual').DataTable({
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

    function file_onChage(e) {
        $lblFileName.text((e.target.files.length > 0) ? e.target.files[0].name : 'Seleccionar archivo');
    }

    var $lblFileName = $('#lblFileName');


    function bindEvents() {
        $('#fileExcel').bind('change', file_onChage);
        $('.ConciliarSistema').bind('click', ConciliarSistema_onClick);
    }
    var $modalConciliarAutomaticamente = $('#modalConciliarAutomaticamente'),
        $hdnAnio = $('#hdnAnio'),
        $hdnMes = $('#hdnMes');

    function ConciliarSistema_onClick(e) {
        $hdnAnio.val($(this).attr('data-anio'));
        $hdnMes.val($(this).attr('data-mes'));
        MostrarBotones($hdnAnio.val(), $hdnMes.val());
        $modalConciliarAutomaticamente.modal('show');
    }

    function MostrarBotones(anio, mes) {
        showLoading();
        var datos = new FormData();
        datos.append("anio", anio);
        datos.append("mes", mes);

        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'MostrarBotonesCadena',
            data: datos,
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();

                for (var i = 0; i < response.Result.length; i++) {
                    if (response.Result[i].Valor == 'Comercializadora Farmacéutica del Sureste, S.A. de C.V.') {
                        $("#btnFDU").css("display", "block");
                    }
                    if (response.Result[i].Valor == 'Farmacias San Francisco de Asís, S.A. de C.V.') {
                        $("#btnSFA").css("display", "block");
                    }
                    if (response.Result[i].Valor == 'Controladora de Farmacias, S.A.P.I. de C.V.') {
                        $("#BtnCofar").css("display", "block");
                    }
                    if (response.Result[i].Valor == 'Farmacia Bella Vista, S.A.P.I. de C.V.') {
                        $("#btnBella").css("display", "block");
                    }
                }
            },
            error: function (response) {
            }
        });
    }

    $("#EnviarExcel").click(function () {

        var formulario = document.getElementById('frmExcel');
        var archivo = $("#fileExcel").val();
        if (archivo != "") {
            showLoading();
            var formData = new FormData(formulario);
            $.ajax({
                type: "POST",
                url: getAbsolutePath() + 'ConciliacionMensualRappi',
                processData: false,
                contentType: false,
                data: formData,
                success: function (response) {
                    hideLoading();
                    if (response.Success) {
                        $("#modalExcel").modal('hide');
                        swal(response.Message, '', "success").then((value) => {
                            window.location.href = "/ConciliacionRappi/Index";
                        });
                    } else {
                        swal(response.Message, '', "error");
                    }
                },
                error: function (e) {
                    hideLoading();
                    swal("¡Error inesperado en la carga!", '', "error");
                }

            })
        } else {
            swal("¡No se ha seleccionado un archivo!", '', "error");
        }
    });

    $("#btnFDU").on("click", function () {

        AplicarConciliacionSistema(1);
    });
    $("#btnBella").on("click", function () {

        AplicarConciliacionSistema(2);
    });
    $("#BtnCofar").on("click", function () {

        AplicarConciliacionSistema(3);
    });
    $("#btnSFA").on("click", function () {

        AplicarConciliacionSistema(4);
    });

    function AplicarConciliacionSistema(opcion) {
        showLoading();


        var aplicado = new FormData();
        aplicado.append("anio", $("#hdnAnio").val());
        aplicado.append("mes", $("#hdnMes").val());
        aplicado.append("opcion", opcion);



        $.ajax({
            type: "POST",
            url: getAbsolutePath() + 'AplicarConciliacion',
            data: aplicado,
            dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function (response) {
                hideLoading();
                if (response.Success) {
                    $modalConciliarAutomaticamente.modal('hide');
                    swal(response.Message, '', "success").then((value) => {
                        window.location.href = "/ConciliacionRappi/Index";
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

    IndexRappi.initialize();

    function getAbsolutePath() {
        var loc = window.location;
        var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
        return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
    }

});