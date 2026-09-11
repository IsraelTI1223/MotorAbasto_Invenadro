$(function () {
    'use strict';

    var IndexRappi = (function () {
        return {
            initialize: function () {
                showLoading();
                //bindEvents();
                $('#dtDataProd').DataTable({
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

    IndexRappi.initialize();

});