$(function () {
    'use strict';
    const language = { url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json' };

    //#region [Tablas]
    let tbHistory = $('#tb-history').DataTable({
        language: language, order: [[3, "desc"]],
        columnDefs: [
            { name: 'TipoCarga', targets: 0, data: 'TipoCarga' },
            { name: 'NombreArchivo', targets: 1, data: 'NombreArchivo' },
            { name: 'UsuarioRegistro', targets: 2, data: 'UsuarioRegistro' },
            { name: 'FechaRegistro', targets: 3, data: 'FechaRegistro' },
            { name: 'TotalCargados', targets: 4, data: 'TotalCargados' },
            { name: 'TotalErroneos', targets: 5, data: 'TotalErroneos' }
        ]
    });
    let tbError = $('#tb-result').DataTable({
        language: language,
        columnDefs: [
            { name: 'Fila', targets: 0, data: 'Fila', width: '10%' },
            { name: 'SKU', targets: 1, data: 'SKU', width: '25%'  },
            { name: 'Mensaje', targets: 2, data: 'Mensaje' }
        ]
    });
    //#endregion

    //#region [Bind]
    let $btnBulkLoadLDCOM = $('#btnBulkLoadDiscount'),
        $fileLDCOM = $('#fileLDCOM'),
        $feedback = $('#feedback'),
        $btnUploadFileModal = $('#btnUploadFileModal'),
        $lblFileName = $('#lblFileName'),
        $uploadFileModal = $('#uploadFileModal'),
        $hFlagPuedeCargar = $("#hFlagPuedeCargar");
    //#endregion

    //#region [funciones]
    const
        format = num => {
            return numeral(num).format('0,0');
        },
        refreshLogs = () => {
            $.post("GetHistoryLoad",
                {},
                result => {
                    //console.log("resultado de duplicar", result);
                    tbHistory.clear();
                    tbHistory.rows.add(result).draw(false);
                }, "json")
                .fail(error => {
                    console.log(error);
                    if (error.status == 401) {
                        window.location = '/Home/ErrorPermiso';
                    }
                });          
        },
        setTableError = table => {
            tbError.clear();
            let tem = [];
            console.log("errors", table);
            table.forEach(row => {
                row.ErrorMessages.forEach(error => {
                    let sku = row["SKU"] != null ? row.SKU : null;
                    sku = sku != null ? sku : (row["CodigoEAN"] != null ? row.CodigoEAN : null);
                    tem.push({
                        Fila: row.RowNum,
                        SKU: sku,
                        Mensaje: error
                    });
                });
            });
            //console.log("errores=>", tem);
            tbError.rows.add(tem).draw(false);
        },
        setIndicators = response => {
            $("#panelResult").removeAttr("style");
            let tot = response.RowsErrorResult + response.RowsOKResult;
            $("#RowsErrorResult").text(format(response.RowsErrorResult));
            $("#RowsOKResult").text(format(response.RowsOKResult));
            $("#TimeLoadProcess").text(format(response.TimeLoadProcess / 1000));
            $("#TimeProcess").text(format(response.TimeProcess));
            $("#okSpanTot").text(format(tot));
            $("#errorSpanTot").text(format(tot));
            $("#TotalRows").text('Registros encontrados: ' + format(tot));
            let msgs = '<ul>';
            response.Messages.forEach(m => { msgs += `<li>${m}</li>`; });
            msgs += '</ul>';
            $("#FileResult").html(msgs);
            setTableError(response.TableError);
        },
        file_onChage = e => {
            $feedback.hide();
            $lblFileName.text((e.target.files.length > 0) ? e.target.files[0].name : 'Seleccionar archivo');
        },
        btnUploadFileModal_onClick = () => {
            //$fileLDCOM.val();
            $feedback.hide();
        },
        btnBulkLoadLDCOM_onClick = () => {
            if ($fileLDCOM.val() !== '') {
                showLoading();
                $feedback.hide();
                let fileUpload = $fileLDCOM.get(0);
                let fData = new FormData();
                let file = fileUpload.files[0];
                fData.append(file.name, file);
                $.ajax({
                    type: "POST",
                    url: "FileUpload",
                    data: fData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        hideLoading();
                        //console.log('respuesta=>', response);
                        $uploadFileModal.modal('hide');
                        if (response.Messages.length > 1) {                            
                            swal('Error, Layout no válido.', response.Messages[1], "error");
                        }
                        setIndicators(response);
                        refreshLogs();
                    },
                    error: function (e) {
                        hideLoading();
                        swal("¡Error al intentar enviar el archivo al servidor!", '', "error");
                    }
                })

            } else {
                $feedback.show();
            }
        },
        bindEvents = () => {
            $btnBulkLoadLDCOM.bind('click', btnBulkLoadLDCOM_onClick);
            $btnUploadFileModal.bind('click', btnUploadFileModal_onClick);
            $fileLDCOM.bind('change', file_onChage);
        };
    //#endregion

    bindEvents();

    //#region [Aplicando permisos para mostrar componentes]
    if ($hFlagPuedeCargar.val() == "True") {
        $btnUploadFileModal.removeAttr("style");
    }
    //#endregion
});