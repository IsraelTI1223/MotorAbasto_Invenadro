$(function () {
    'use strict';

    var IndexReportes = (function () {
        return {
            initialize: function () {
                showLoading();
                bindEvents();
                $('#dtCatproduct').DataTable({
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

    IndexReportes.initialize();


    $("#btnProd").click(function () {
        $.post('@Url.Action("ConsultDataReport", "Reportes")', { flag: 'prod' }, function (data) {

        });
    });




});
