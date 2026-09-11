
$(function () {
	'use strict';




	$("#ddlProv").on("change", function () {

		var datos = new FormData();

		datos.append("Proveedor", $("#ddlProv").val());
		ObtenerAgencia(datos);

		ObtenerRazonSocial(datos);
	});



	function ObtenerAgencia(datos) {

		$('#ddlAgencia').empty();
		$.ajax({
			type: "POST",
			url: getAbsolutePath() + 'Obteneragencia',
			data: datos,
			dataType: "json",
			processData: false,  // tell jQuery not to process the data
			contentType: false,
			success: function (response) {

				var agencia = "";
				for (var i = 0; i < response.data.length; i++) {
					agencia = agencia + '<option value=' + response.data[i].Id + '>' + response.data[i].Valor + '</option>';
				}
				$('#ddlAgencia').append(agencia)
			},
			error: function (response) {
			}
		});
	}

	function ObtenerRazonSocial(datos) {

		$('#ddlRSocial').empty();
		$.ajax({
			type: "POST",
			url: getAbsolutePath() + 'ObtenerRazonSocial',
			data: datos,
			dataType: "json",
			processData: false,  // tell jQuery not to process the data
			contentType: false,
			success: function (response) {

				var razonsocial = "";
				for (var i = 0; i < response.data.length; i++) {
					razonsocial = razonsocial + '<option value=' + response.data[i].Id + '>' + response.data[i].Valor + '</option>';
				}
				$('#ddlRSocial').append(razonsocial)
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





