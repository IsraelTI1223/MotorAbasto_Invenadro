---
name: new-report
description: "Crear un reporte en Portal GRFP con grid DataTables, filtros de Grupo y Sucursal, y boton de exportacion. Usar cuando: reporte con tabla, exportar Excel o CSV, grid con filtros de sucursal/grupo, reporte con SP parametrizado, descargar datos."
---

# new-report — Reporte con Grid y Exportacion

Patron basado en los reportes existentes del proyecto (VistaReporteInvenadroAut, ReporteEstadisticaCompras).

---

## PASO 1 — Entidad (`PortalGRFP.Entities/Common/NombreReporteModel.cs`)

```csharp
namespace PortalGRFP.Entities.Common
{
    public class NombreReporteModel
    {
        public int      IdSucursal   { get; set; }
        public string   Sucursal     { get; set; }
        public string   SKU          { get; set; }
        public string   Descripcion  { get; set; }
        public decimal? PVD          { get; set; }
        // agregar columnas del SP
    }
}
```

---

## PASO 2 — Mapper (`PortalGRFP.Data/Extensions/MapExtension.cs`)

```csharp
public static NombreReporteModel ToNombreReporte(this IDataReader reader)
{
    return new NombreReporteModel
    {
        IdSucursal  = reader.Get<int>("IdSucursal"),
        Sucursal    = reader.Get<string>("Sucursal"),
        SKU         = reader.Get<string>("SKU"),
        Descripcion = reader.Get<string>("Descripcion"),
        PVD         = reader.Get<decimal?>("PVD"),
    };
}
```

---

## PASO 3 — Data (`PortalGRFP.Data/`)

```csharp
// GET con ResponseList
public ResponseList<NombreReporteModel> GetNombreReporteData(string sucursales, string sku)
{
    var response = new ResponseList<NombreReporteModel>();
    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
    var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_NOMBRE_REPORTE");
    db.AddInParameter(command, "@sucursales", DbType.String, sucursales);
    db.AddInParameter(command, "@sku", DbType.String,
        string.IsNullOrEmpty(sku) ? (object)DBNull.Value : sku);
    command.CommandTimeout = 120;
    var read = db.ExecuteReader(command);
    response.Result  = read.Reader(x => x.ToNombreReporte());
    response.Success = response.Result != null && response.Result.Any();
    return response;
}

// GET con DataTable (para exportar CSV/Excel)
public DataTable GetExportNombreReporteData(string sucursales, string sku)
{
    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
    var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_NOMBRE_REPORTE");
    db.AddInParameter(command, "@sucursales", DbType.String, sucursales);
    db.AddInParameter(command, "@sku", DbType.String,
        string.IsNullOrEmpty(sku) ? (object)DBNull.Value : sku);
    command.CommandTimeout = 120;
    var ds = db.ExecuteDataSet(command);
    return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
}
```

---

## PASO 4 — Business (`PortalGRFP.Business/`)

```csharp
public ResponseList<NombreReporteModel> GetNombreReporteBLL(string sucursales, string sku)
{
    var response = new ResponseList<NombreReporteModel>();
    try
    {
        response = _data.GetNombreReporteData(sucursales, sku);
        if (response.Success)
            response.Message = $"Reporte generado con {response.Result.Count} registros.";
    }
    catch (Exception ex)
    {
        response.Success = false;
        response.Message = "Error al generar reporte: " + ex.Message;
    }
    return response;
}

public byte[] GetExportNombreReporteBLL(string sucursales, string sku)
{
    var tabla = _data.GetExportNombreReporteData(sucursales, sku);
    var sb = new StringBuilder();
    var headers = tabla.Columns.Cast<DataColumn>()
        .Select(c => "\"" + c.ColumnName.Replace("\"", "\"\"") + "\"");
    sb.AppendLine(string.Join(",", headers));
    foreach (DataRow row in tabla.Rows)
    {
        var values = row.ItemArray
            .Select(v => "\"" + (v?.ToString() ?? "").Replace("\"", "\"\"") + "\"");
        sb.AppendLine(string.Join(",", values));
    }
    return Encoding.UTF8.GetPreamble()
        .Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
}
```

---

## PASO 5 — Controller (`PortalGRFP/Controllers/ReportesController.cs`)

```csharp
// GET — vista con combos de grupos y sucursales
public ActionResult VistaNombreReporte()
{
    var user = this.GetUsuario();
    if (user == null) return RedirectToAction("Login", "Account");

    var Grupos = new SugeridoBusiness().Filtros(3);
    Session["Grupos_Usuario"] = new GruposBLL().GetGrupos_Usuario(4).Result;
    List<Combo3> gruposUsuario = (List<Combo3>)Session["Grupos_Usuario"];
    var usuarioGrupos = (from g in gruposUsuario
                         where g.IdFiltro == user.IdUsuario && g.Valor == "True"
                         select g.Id).ToList();
    TempData["GrupoTMP"] = (from gr in Grupos.Result
                             where usuarioGrupos.Contains(gr.Id)
                             select gr).ToList();
    var Sucursales = new SugeridoBusiness().Filtros2(4);
    Session["SucursalTMP"] = Sucursales.Result;

    return View();
}

// POST — cargar sucursales por grupo (AJAX)
[HttpPost]
public ActionResult GetSucursalNombreReporte(FormCollection datos)
{
    var buscar = datos["idGrupo"].Split(',');
    List<Combo3> sucursales = (List<Combo3>)Session["SucursalTMP"];
    var result = (from N in sucursales
                  where buscar.Contains(N.IdFiltro.ToString())
                  select new { N.Id, N.Valor });
    return Json(result, JsonRequestBehavior.AllowGet);
}

// POST — buscar datos del reporte (AJAX)
[HttpPost]
public ActionResult BuscarNombreReporte(FormCollection datos)
{
    try
    {
        string sucursales = datos["Sucursal"];
        string sku        = datos["SKU"];
        var resultado = new MiBusiness().GetNombreReporteBLL(sucursales, sku);
        var json = Json(
            new { success = resultado.Success, message = resultado.Message, data = resultado.Result },
            JsonRequestBehavior.AllowGet);
        json.MaxJsonLength = 500000000;
        return json;
    }
    catch (Exception ex)
    {
        var json = Json(
            new { success = false, message = "Error: " + ex.Message, data = new List<NombreReporteModel>() },
            JsonRequestBehavior.AllowGet);
        json.MaxJsonLength = 500000000;
        return json;
    }
}

// GET — exportar CSV
public void ExportarNombreReporte(string sucursales, string sku)
{
    var csv = new MiBusiness().GetExportNombreReporteBLL(sucursales, sku);
    Response.Clear();
    Response.ContentType = "text/csv";
    Response.AddHeader("content-disposition",
        $"attachment; filename=NombreReporte_{DateTime.Now:yyyyMMdd}.csv");
    Response.BinaryWrite(csv);
    Response.End();
}
```

---

## PASO 6 — Vista (`.cshtml`)

```html
@using PortalGRFP.Entities.Common
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
@section Breadcrumbs{
    <li class="breadcrumb-item active"><a>Consulta</a></li>
    <li class="breadcrumb-item"><a>Nombre Reporte</a></li>
}

<div class="container-fluid">
    <div class="card">
        <div class="card-body">

            <!-- Filtros -->
            <div class="row" style="background:#f8f9fa; padding:15px; border-radius:5px; margin-bottom:15px;">
                <div class="col-md-3">
                    <label>Grupo <span class="text-danger">*</span></label>
                    <select id="Grupo" name="Grupo" multiple="multiple" class="form-control">
                        @foreach (var item in TempData["GrupoTMP"] as List<ComboGenerico>)
                        { <option value="@item.Id">@item.Valor</option> }
                    </select>
                </div>
                <div class="col-md-3">
                    <label>Farmacia <span class="text-danger">*</span></label>
                    <select id="Sucursal" name="Sucursal" multiple="multiple" class="form-control" disabled></select>
                </div>
                <div class="col-md-3">
                    <label>SKU (Opcional)</label>
                    <input type="text" id="SKU" class="form-control" maxlength="30"
                           placeholder="Dejar vacio para todos" />
                </div>
                <div class="col-md-3">
                    <button id="btnBuscar" class="btn btn-primary btn-block" style="margin-top:25px">
                        <i class="fa fa-search"></i> Buscar
                    </button>
                </div>
            </div>

            <!-- Mensaje -->
            <div id="mensajeContenedor"></div>

            <!-- Exportar (oculto hasta que haya datos) -->
            <div id="exportarSection" style="display:none;" class="text-right mb-2">
                <button id="btnExportar" class="btn btn-success">
                    <i class="fa fa-file-csv"></i> Exportar CSV
                </button>
            </div>

            <!-- Grid -->
            <div class="table-responsive">
                <table id="tbReporte" class="display table table-striped table-bordered" style="width:100%">
                    <thead><tr>
                        <th>ID Suc.</th>
                        <th>Sucursal</th>
                        <th>SKU</th>
                        <th>Descripcion</th>
                        <th>PVD</th>
                        <!-- agregar columnas del SP -->
                    </tr></thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>

@section scripts{
<script>
var tablaReporte = null;

$(document).ready(function () {
    // Multiselect Grupo
    $('#Grupo').multiselect({
        nonSelectedText: 'Seleccione un grupo',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '100%',
        selectAllText: 'Marcar Todos',
        allSelectedText: 'Todos Seleccionados'
    });

    // Multiselect Sucursal
    $('#Sucursal').multiselect({
        nonSelectedText: 'Seleccione una Farmacia',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '100%',
        selectAllText: 'Marcar Todas',
        allSelectedText: 'Todas Seleccionadas'
    });

    // Cargar sucursales al cambiar grupo
    $('#Grupo').on('change', function () {
        var idGrupo = $(this).val();
        if (!idGrupo || idGrupo.length === 0) {
            $('#Sucursal').empty();
            $('#Sucursal').multiselect('rebuild');
            document.getElementById('Sucursal').disabled = true;
            return;
        }
        var datos = new FormData();
        datos.append('idGrupo', idGrupo.join(','));
        showLoading();
        $.ajax({
            type: 'POST',
            url: '@Url.Action("GetSucursalNombreReporte", "ReportesController")',
            data: datos,
            processData: false,
            contentType: false,
            success: function (r) {
                hideLoading();
                $('#Sucursal').empty();
                if (r && r.length > 0) {
                    r.forEach(s => $('#Sucursal').append('<option value="' + s.Id + '">' + s.Valor + '</option>'));
                    $('#Sucursal').multiselect('rebuild');
                    document.getElementById('Sucursal').disabled = false;
                }
            },
            error: function () { hideLoading(); }
        });
    });

    // Buscar reporte
    $('#btnBuscar').on('click', function () {
        var grupo    = $('#Grupo').val();
        var sucursal = $('#Sucursal').val();
        if (!grupo || grupo.length === 0) {
            mostrarMensaje('Debe seleccionar al menos un Grupo.', 'warning');
            return;
        }
        if (!sucursal || sucursal.length === 0) {
            mostrarMensaje('Debe seleccionar al menos una Farmacia.', 'warning');
            return;
        }
        showLoading();
        $('#mensajeContenedor').empty();
        $.ajax({
            type: 'POST',
            url: '@Url.Action("BuscarNombreReporte", "ReportesController")',
            data: { Sucursal: sucursal.join(','), SKU: $('#SKU').val() },
            dataType: 'json',
            success: function (r) {
                hideLoading();
                if (r.success && r.data && r.data.length > 0) {
                    cargarTabla(r.data);
                    mostrarMensaje(r.message, 'success');
                    $('#exportarSection').show();
                } else {
                    mostrarMensaje(r.message || 'Sin resultados.', 'info');
                    $('#exportarSection').hide();
                    if (tablaReporte) tablaReporte.clear().draw();
                }
            },
            error: function () {
                hideLoading();
                mostrarMensaje('Error al realizar la busqueda.', 'danger');
            }
        });
    });

    // Exportar CSV
    $('#btnExportar').on('click', function () {
        window.location.href = '@Url.Action("ExportarNombreReporte", "ReportesController")'
            + '?sucursales=' + encodeURIComponent($('#Sucursal').val().join(','))
            + '&sku=' + encodeURIComponent($('#SKU').val());
    });
});

function cargarTabla(datos) {
    if (tablaReporte) { tablaReporte.destroy(); }
    tablaReporte = $('#tbReporte').DataTable({
        data: datos,
        columns: [
            { data: 'IdSucursal' },
            { data: 'Sucursal' },
            { data: 'SKU' },
            { data: 'Descripcion' },
            { data: 'PVD', render: d => d ? '$' + parseFloat(d).toFixed(2) : '$0.00' }
            // agregar columnas del modelo
        ],
        language: { url: '//cdn.datatables.net/plug-ins/1.11.5/i18n/es-MX.json' },
        pageLength: 25,
        scrollX: true
    });
}

function mostrarMensaje(texto, tipo) {
    $('#mensajeContenedor').html(
        '<div class="alert alert-' + tipo + ' alert-dismissible">' +
        texto +
        '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
        '</div>'
    );
}
</script>
}
```

---

## Columnas estandar del SP (nombres recomendados)

| Columna SQL    | Propiedad C#  | Tipo       |
|----------------|---------------|------------|
| `IdSucursal`   | `IdSucursal`  | `int`      |
| `Sucursal`     | `Sucursal`    | `string`   |
| `SKU`          | `SKU`         | `string`   |
| `Descripcion`  | `Descripcion` | `string`   |
| `PVD`          | `PVD`         | `decimal?` |

---

## Checklist

- [ ] `TempData["GrupoTMP"]` poblado en el action GET
- [ ] `Session["SucursalTMP"]` poblado en el action GET
- [ ] Boton Exportar oculto hasta que haya datos en la tabla
- [ ] SP acepta `@sucursales VARCHAR(MAX)` y `@sku VARCHAR(50) = NULL`
- [ ] CSV usa BOM UTF-8 (`Encoding.UTF8.GetPreamble()`)
