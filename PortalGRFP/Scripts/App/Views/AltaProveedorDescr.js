
$(function () {
	'use strict';




	$("#ddlProv").on("change", function () {

		var datos = new FormData();

		datos.append("Proveedor", $("#ddlProv").val());
		ObtenerDescrProveedor(datos);
	});



	function ObtenerDescrProveedor(datos) {

		$('#ddlDescr').empty();
		$.ajax({
			type: "POST",
			url: getAbsolutePath() + 'Obtenerdescrproveedor',
			data: datos,
			dataType: "json",
			processData: false,  // tell jQuery not to process the data
			contentType: false,
			success: function (response) {

				var descrproveedor = "";
				for (var i = 0; i < response.data.length; i++) {
					descrproveedor = descrproveedor + '<option value=' + response.data[i].Id + '>' + response.data[i].Valor + '</option>';
				}
				$('#ddlDescr').append(descrproveedor)
			},
			error: function (response) {
			}
		});
	}








	function getAbsolutePath() {
		var loc = window.location;
		var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
		return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
	}



	//$("#DTPedEsp tbody").on('click', 'tr', function () {

	//	var rowData = $(this).children("td").map(function () {
	//		return $(this).text();
	//	}).get();

	//	$("#idInput").val(rowData[0]);
	//	$("#nombreInput").val(rowData[1]);
	//});



});





