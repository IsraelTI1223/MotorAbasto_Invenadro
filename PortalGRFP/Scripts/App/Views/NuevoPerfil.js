$(function () {
        'use strict';

        function validaDatos() {
            var varEstatus, varNombre, varModulosSeleccionados;
            varModulosSeleccionados = '';
            if (document.getElementById('Activo').checked) { varEstatus = true } else { varEstatus = false }
            varNombre = $("#Nombre").val();
            $("#tblModulos tbody tr").each(function (index) {
                var varIdModulo, varModulo, varRegistra, varActualiza, varElimina, varConsulta;
                $(this).children("td").each(function (index2) {
                    switch (index2) {
                        case 0:
                            varIdModulo = $(this).text();
                            if (document.getElementById('chk1' + varIdModulo).checked) { varRegistra = 1 } else { varRegistra = 0 }
                            if (document.getElementById('chk2' + varIdModulo).checked) { varActualiza = 1 } else { varActualiza = 0 }
                            if (document.getElementById('chk3' + varIdModulo).checked) { varElimina = 1 } else { varElimina = 0 }
                            if (document.getElementById('chk4' + varIdModulo).checked) { varConsulta = 1 } else { varConsulta = 0 }
                            varModulosSeleccionados += varIdModulo + ',' + varRegistra + ',' + varActualiza + ',' + varElimina + ',' + varConsulta + '|'
                            break;
                        case 1:
                            varModulo = $(this).text();
                            break;
                    }
                    $(this).css("background-color", "#ECF8E0");
                })
                //alert('ID:' + varIdModulo + ' - Modulo:' + varModulo + ' - Registra:' + varRegistra + ' - Actualiza:' + varActualiza + ' - Elimina:' + varElimina + ' - Consulta:' + varConsulta + ' - Estatus:' + varEstatus + ' - Nombre:' + varNombre);
            })
            //alert(varModulosSeleccionados);
            var datObject =
            {
                IdPerfil: 0,
                Nombre: varNombre,
                Activo: varEstatus,
                ModulosSeleccionados: varModulosSeleccionados,
            };
            
        }

    function bindEvents() {
        btnGuardar.bind('click', validaDatos);
    }

    var nuevoPerfil = (function () {
        return {
            initialize: function () {
                bindEvents();
            }
        };
    })();

    nuevoPerfil.initialize();

    });