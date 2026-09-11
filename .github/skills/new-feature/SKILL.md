---
name: new-feature
description: "Crear un feature completo en Portal GRFP siguiendo arquitectura en 4 capas. Usar cuando: agregar endpoint, nuevo campo que pase por capas, nuevo módulo, integrar nuevo SP. Genera: Entidad, Mapper, Data, Business, Controller y Vista."
---

# new-feature — Feature Completo en 4 Capas

Sigue este orden estricto para cada nuevo feature del proyecto.

---

## PASO 1 — Entidad (`PortalGRFP.Entities/Common/`)

```csharp
// NombreModelo.cs
namespace PortalGRFP.Entities.Common
{
    public class NombreModelo
    {
        public int      Id     { get; set; }
        public string   Campo1 { get; set; }
        public decimal? Monto  { get; set; }
        // usar nullable (?) para campos opcionales del SP
    }
}
```

---

## PASO 2 — Mapper (`PortalGRFP.Data/Extensions/MapExtension.cs`)

Agregar **al final** de la clase `MapExtension`, antes del cierre `}`:

```csharp
public static NombreModelo ToNombreModelo(this IDataReader reader)
{
    return new NombreModelo
    {
        Id     = reader.Get<int>("NombreColumnaEnSP"),
        Campo1 = reader.Get<string>("OtraColumna"),
        Monto  = reader.Get<decimal?>("MontoColumna"),
    };
}
```

---

## PASO 3 — Data (`PortalGRFP.Data/`)

```csharp
// GET lista
public ResponseList<NombreModelo> GetNombreData(int usuario)
{
    var response = new ResponseList<NombreModelo>();
    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
    var command = db.GetStoredProcCommand("mta.SP_GRFP_GET_NOMBRE");
    db.AddInParameter(command, "@usuario", DbType.Int32, usuario);
    command.CommandTimeout = 120;
    var read = db.ExecuteReader(command);
    response.Result  = read.Reader(x => x.ToNombreModelo());
    response.Success = response.Result.Any();
    return response;
}

// ESCRITURA
public Response SaveNombreData(NombreModelo modelo, int usuario)
{
    var response = new Response();
    var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
    var command = db.GetStoredProcCommand("mta.SP_GRFP_SAVE_NOMBRE");
    db.AddInParameter(command, "@campo1",  DbType.String,  modelo.Campo1);
    db.AddInParameter(command, "@monto",   DbType.Decimal, modelo.Monto);
    db.AddInParameter(command, "@usuario", DbType.Int32,   usuario);
    command.CommandTimeout = 120;
    var exito = db.ExecuteNonQuery(command);
    response.Success = exito != default;
    return response;
}
```

---

## PASO 4 — Business (`PortalGRFP.Business/`)

```csharp
public ResponseList<NombreModelo> GetNombreBLL(int usuario)
{
    var response = new ResponseList<NombreModelo>();
    try
    {
        response = _data.GetNombreData(usuario);
    }
    catch (Exception ex)
    {
        response.Success = false;
        response.Message = "Error al consultar datos: " + ex.Message;
    }
    return response;
}

public Response SaveNombreBLL(NombreModelo modelo, int usuario)
{
    var response = new Response();
    try
    {
        response = _data.SaveNombreData(modelo, usuario);
        response.Message = response.Success
            ? "Operacion realizada con exito."
            : "No se pudo completar la operacion.";
    }
    catch (Exception ex)
    {
        response.Success = false;
        response.Message = "Error al guardar: " + ex.Message;
    }
    return response;
}
```

---

## PASO 5 — Controller (`PortalGRFP/Controllers/`)

```csharp
// GET — vista
public ActionResult VistaFuncionalidad()
{
    var user = this.GetUsuario();
    if (user == null) return RedirectToAction("Login", "Account");
    // poblar TempData si la vista necesita combos
    return View();
}

// POST — datos JSON
[HttpPost]
public ActionResult GetNombre(FormCollection datos)
{
    var user = this.GetUsuario();
    var resultado = new MiBusiness().GetNombreBLL(user.IdUsuario);
    var json = Json(resultado, JsonRequestBehavior.AllowGet);
    json.MaxJsonLength = 500000000;
    return json;
}

// POST — guardar
[HttpPost]
public ActionResult SaveNombre(FormCollection datos)
{
    var user = this.GetUsuario();
    var modelo = new NombreModelo
    {
        Campo1 = datos["campo1"],
        Monto  = decimal.TryParse(datos["monto"], out var m) ? m : (decimal?)null
    };
    var resultado = new MiBusiness().SaveNombreBLL(modelo, user.IdUsuario);
    var json = Json(resultado, JsonRequestBehavior.AllowGet);
    json.MaxJsonLength = 500000000;
    return json;
}
```

---

## PASO 6 — Vista (`.cshtml`)

```html
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
@section Breadcrumbs{
    <li class="breadcrumb-item active"><a>Modulo</a></li>
    <li class="breadcrumb-item"><a>Funcionalidad</a></li>
}

<div class="container-fluid">
    <div class="card">
        <div class="card-body">
            <!-- contenido -->
        </div>
    </div>
</div>

@section scripts{
<script>
$(document).ready(function () {
    cargarDatos();
});

function cargarDatos() {
    showLoading();
    $.ajax({
        url: '@Url.Action("GetNombre", "MiController")',
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            hideLoading();
            if (!response.Success) {
                swal(response.Message || "Sin datos", "", "warning");
                return;
            }
            // procesar response.Result
        },
        error: function () {
            hideLoading();
            swal("Error al cargar datos", "", "error");
        }
    });
}
</script>
}
```

---

## Checklist antes de terminar

- [ ] El SP recibe `@usuario`
- [ ] `CommandTimeout` configurado (120 para reportes, 0 para operaciones largas)
- [ ] `GetUsuario()` validado en cada action que retorna vista
- [ ] `MaxJsonLength = 500000000` en todos los Json()
- [ ] Mapper agregado en `MapExtension.cs`
- [ ] Entidad en `PortalGRFP.Entities.Common`
- [ ] Connection string: `"dev_GRFP_RCB_DB"`
- [ ] SP en schema `mta.`
