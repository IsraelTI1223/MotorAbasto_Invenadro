$(function () {
    'use strict';

    var configuracionCarga = {
        IdCarga: 10,
        ArchivosPorDia: 0,
        HoraInicio: '',
        HoraFin: ''
    };

    var $editConfigurationModal = $('#editConfigurationModal'),
        $hdnIdCarga = $('#hdnIdCarga'),
        $txtArchivosXDia = $('#txtArchivosXDia'),
        $txtHoraInicio = $('#txtHoraInicio'),
        $txtHoraFin = $('#txtHoraFin'),
        $feedback = $('#feedback');

    function editConfiguration_onClick(e) {
        $hdnIdCarga.val($(this).attr('data-idCarga'));
        $txtArchivosXDia.val($(this).attr('data-archivos-por-dia'))
        $txtHoraInicio.val($(this).attr('data-hora-inicio'));
        $txtHoraFin.val($(this).attr('data-hora-fin'));
        $editConfigurationModal.modal('show');
    }

    function fileConfigurationMerge() {
        showLoading();
        //$.post('FileUploadConfiguration/Merge', configuracionCarga, function (response) {

        //});

        $.ajax({
            type: "POST",
            url: "/FileUploadConfiguration/Merge",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "configuration": configuracionCarga }),
            dataType: "json",
            processData: false,
            success: function (response) {
                hideLoading();
                if (response.Success) {
                    $editConfigurationModal.modal('hide');
                    swal(response.Message, '', "success").then((value) => {
                        window.location.href = "/FileUploadConfiguration/Index";
                    });
                } else {
                    swal(response.Message, '', "error");
                }
            },
            error: function (e) {
                hideLoading();
                swal("¡Error inesperado en la edición!", '', "error");
            }
        })
    }

    function btnEditFileUploadConfiguration_onClick() {
        $feedback.html('');
        if ($txtArchivosXDia.val() !== '' && !isNaN($txtArchivosXDia.val()) && parseInt($txtArchivosXDia.val()) >= 1) {
            if ($txtHoraInicio.val !== '' && $txtHoraFin.val() !== '') {
                configuracionCarga.IdCarga = $hdnIdCarga.val();
                configuracionCarga.ArchivosPorDia = $txtArchivosXDia.val();
                configuracionCarga.HoraInicio = $txtHoraInicio.val();
                configuracionCarga.HoraFin = $txtHoraFin.val();
                fileConfigurationMerge();
            } else {
                $txtHoraInicio.focus();
                $feedback.html("Es necesario configure la hora de inicio y fin.");
            }
        } else {
            $txtArchivosXDia.focus();
            $feedback.html("El número de archivos por día debe ser mayor a 0");
        }
    }

    function bindEvents() {
        $('.editConfiguration').bind('click', editConfiguration_onClick);
        $('#btnEditFileUploadConfiguration').bind('click', btnEditFileUploadConfiguration_onClick);
    }

    var fileUpLoadConfiguration = (function () {
        return {
            initialize: function () {
                $feedback.html('');
                bindEvents();
                $('#tFileUploadConfiguration').dataTable({
                    responsive: true,
                    language: {
                        searchPlaceholder: 'Buscar',
                        url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'
                    }
                });
            }
        };
    })();

    fileUpLoadConfiguration.initialize();
});