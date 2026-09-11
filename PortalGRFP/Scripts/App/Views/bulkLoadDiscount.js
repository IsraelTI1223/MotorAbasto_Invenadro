$(function () {
    'use strict';

    var $btnBulkLoadLDCOM = $('#btnBulkLoadDiscount'),
        $fileLDCOM = $('#fileLDCOM'),
        $feedback = $('#feedback'),
        $btnUploadFileModal = $('#btnUploadFileModal'),
        $lblFileName = $('#lblFileName'),
        $uploadFileModal = $('#uploadFileModal');


        

    function file_onChage(e) {
        $feedback.hide()
        $lblFileName.text((e.target.files.length > 0) ? e.target.files[0].name : 'Seleccionar archivo');
    }

    function btnUploadFileModal_onClick() {
              
        var Tipo = $("#selectCliente").val();   
        var subtipo = $("#subtipo").val();
        var SubClientes = $("#selectSubCliente option:selected").text(); 
        var idPerfil = document.getElementById('IdPerfil').value;

        if(Tipo == 0) {
            swal("¡Error no ha seleccionado ningún cliente!", '', "error");
            return;
        }

        if (subtipo == 2 || idPerfil == 4) {
            if (SubClientes == "SUBCLIENTES") {
                swal("¡Error no ha seleccionado ningún SubCliente!", '', "error");
                return;
            }
            else {
                $fileLDCOM.val('');
                $feedback.hide();
                $('#uploadFileModal').modal('show');
            }

        }
        else {
            $fileLDCOM.val('');
            $feedback.hide();
            $('#uploadFileModal').modal('show');
        }

      
    }

    function btnBulkLoadLDCOM_onClick() {
        if ($fileLDCOM.val() !== '') {
            showLoading();
            $feedback.hide();
            var fileUpload = $fileLDCOM.get(0);
            var fData = new FormData();
            var file = fileUpload.files[0];
            var val = $("#selectCliente option:selected").val();            
            var tipo = $("#subtipo option:selected").text()    
            var val2 = $("#subtipo").val();
            var subtipo = $("#subtipo").val();        
            var idPerfil = document.getElementById('IdPerfil').value;
            var subcliente = (subtipo == 2 || idPerfil == 4) ? $("#selectSubCliente").val() : 0;
            var NClientes = $("#selectCliente option:selected").text();
            var NSubcliente = $("#selectSubCliente option:selected").text();
            

            fData.append(file.name, file);
            fData.append("LayoutType", "LDCOM");
            fData.append("IdCliente", val);     
            fData.append("tipoPerfil", tipo);
            fData.append("IdSubtipo", val2);
            fData.append("IdSubCliente", subcliente);
            fData.append("NombreCliente", NClientes);
            fData.append("NombreSubcliente", NSubcliente);
           

            $.ajax({
                type: "POST",
                url: "FileUpload",
                data: fData,
                contentType: false,
                processData: false,
                success: function (response) {
                    hideLoading();
                    if (response.Success) {
                        $uploadFileModal.modal('hide');
                        swal(response.Message, '', "success").then((value) => {
                            window.location.href = "/BulkLoadDiscount/Index";
                        });
                    } else {
                        swal(response.Message, '', "error");
                    }
                },
                error: function (e) {
                    hideLoading();
                    swal("¡Error al intentar enviar el archivo al servidor!", '', "error");
                }
            })

        } else {
            $feedback.show();
        }
    }

    function bindEvents() {
        $btnBulkLoadLDCOM.bind('click', btnBulkLoadLDCOM_onClick);
        $btnUploadFileModal.bind('click', btnUploadFileModal_onClick);
        $fileLDCOM.bind('change', file_onChage);
    }

    var bulkLoadLdcom = (function () {
        return {
            initialize: function () {
                bindEvents();
                $('#tbulkLoadDiscount').dataTable({
                    responsive: true,
                    language: {
                        searchPlaceholder: 'Buscar',
                        url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'
                    }
                });
            }
        };
    })();





    $('#subtipo').change(function () {
        var fData = new FormData();
        
        var val = $("#subtipo").val();
        var subtipo = $("#subtipo").val();
        fData.append("IdSubtipo", val);
        var idPerfil = document.getElementById('IdPerfil').value;

        if (idPerfil == 1) {

            if (val == 1 || val == 3 || val == 4) {
                document.getElementById('IdSubTipoPerfil').style.display = "none";
                document.getElementById('TipoCliente').style.display = "block";
                document.getElementById('TipoSubCliente').style.display = "none";

            }
            if (val == 2) {
                document.getElementById('TipoCliente').style.display = "none";
                document.getElementById('TipoSubCliente').style.display = "block";
            }


        }

        
        showLoading();
        $('#selectCliente option').remove();

        $.ajax({
            type: "POST",
            url: "GetClientesBySubTipo",
            data: fData,
            contentType: false,
            processData: false,
            success: function (response) {
                hideLoading();
                if (val == 1 || val == 3 || val == 4) {
                    $('#selectCliente').append('<option value="0">SUBCLIENTES</option>');
                }
                else {
                    $('#selectCliente').append('<option value="0">CLIENTES</option>');
                }
               
                if (response.Success) {                
                   
                    $.each(response.Result, function (index, cliente) {
                        $('#selectCliente').append('<option value=' + cliente.IdCliente + '>' + (val == 2? cliente.Nombre : cliente.SubCliente)  + ' ' + '-' + ' ' + 'RFC:' + cliente.RFC + '</option>');
                    });
                    $('#selectCliente').focus();
                    
                } else {
                    swal(response.Message, '', "error");
                }
            },
            error: function (e) {
                hideLoading();
                swal("¡Error al intentar enviar el archivo al servidor!", '', "error");
            }
        })
    });



    $('#selectCliente').change(function () {
        var fData = new FormData();
        var val = $("#selectCliente").val();
        var subtipo = $("#subtipo").val();
        var idPerfil = document.getElementById('IdPerfil').value; 
        
        fData.append("IdSubCliente", val);
       
        
        $('#selectSubCliente option').remove();

        if (subtipo == 2 || idPerfil == 4) {
           
            document.getElementById('IdSubTipoPerfil').style.display = "block";

            showLoading();
            $('#selectSubCliente').prop('disabled', false);
            $.ajax({
                type: "POST",
                url: "GetClientesBySubCliente",
                data: fData,
                contentType: false,
                processData: false,
                success: function (response) {
                    hideLoading();
                    $('#selectSubCliente').append('<option value="0">SUBCLIENTES</option>');
                    if (response.Success) {

                        $.each(response.Result, function (index, cliente) {
                            $('#selectSubCliente').append('<option value=' + cliente.Id_SubCliente + '>' + cliente.SubCliente + ' ' + '-' + ' ' + 'RFC:' + cliente.RFC + '</option>');
                        });
                        $('#selectSubCliente').focus();

                    } else {
                        swal(response.Message, '', "error");
                    }
                },
                error: function (e) {
                    hideLoading();
                    swal("¡Error al intentar enviar el archivo al servidor!", '', "error");
                }
            })
        } else {
            document.getElementById('IdSubTipoPerfil').style.display = "none";
            $('#selectSubCliente').prop('disabled', 'disabled');
        }

        
    });





    bulkLoadLdcom.initialize();
});