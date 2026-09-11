---
name: sp-template
description: "Generar plantilla SQL de Stored Procedure para Portal GRFP. Usar cuando: crear SP, escribir script SQL, nuevo stored procedure para GET, INSERT, UPDATE, DELETE, carga masiva TVP, exportacion de datos."
---

# sp-template — Plantilla SQL para Stored Procedures

## Convencion de nombres

```
[mta].[SP_GRFP_{VERBO}_{ENTIDAD}]

Verbos estandar:
  GET    -> Consulta/lectura
  SAVE   -> Insert o upsert (MERGE)
  UPDATE -> Actualizacion especifica
  DELETE -> Eliminacion
  LOAD   -> Carga masiva via TVP
  EXPORT -> Retorna datos para CSV/Excel
  CONF   -> Configuracion del sistema

Ejemplos reales del proyecto:
  [mta].[SP_GRFP_GET_REPORTE_INVENADRO]
  [mta].[SP_GRFP_GET_REPORTE_INVENADRO_AUT]
  [mta].[SP_GRFP_SAVE_CONFIGURACION_CDR]
  [mta].[SP_GRFP_LOAD_EXCEPCIONES_INVENADRO]
  [mta].[SP_GET_EXPORT_BLOQUES_CDR]
```

---

## Template GET — Consulta con filtros

```sql
CREATE OR ALTER PROCEDURE [mta].[SP_GRFP_GET_ENTIDAD]
    @sucursales  VARCHAR(MAX) = NULL,   -- IDs separados por coma, opcional
    @sku         VARCHAR(50)  = NULL,   -- filtro opcional
    @usuario     INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        s.IdSucursal,
        s.NombreSucursal    AS Sucursal,
        a.SKU,
        a.Descripcion,
        pvd.PrecioVenta     AS PVD
        -- agregar columnas segun entidad
    FROM [mta].[TuTabla] t
    INNER JOIN [mta].[Sucursales]  s ON t.IdSucursal = s.IdSucursal
    INNER JOIN [mta].[Articulos]   a ON t.SKU        = a.SKU
    WHERE
        (@sucursales IS NULL OR s.IdSucursal IN (
            SELECT CAST(value AS INT)
            FROM STRING_SPLIT(@sucursales, ',')
            WHERE RTRIM(value) <> ''
        ))
        AND (@sku IS NULL OR a.SKU = @sku)
    ORDER BY s.NombreSucursal, a.SKU;
END
GO
```

---

## Template SAVE — Insert / Upsert con MERGE

```sql
CREATE OR ALTER PROCEDURE [mta].[SP_GRFP_SAVE_ENTIDAD]
    @campo1   VARCHAR(100),
    @campo2   DECIMAL(18, 2),
    @usuario  INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        MERGE [mta].[TuTabla] AS target
        USING (SELECT @campo1 AS campo1) AS source
            ON target.campo1 = source.campo1
        WHEN MATCHED THEN
            UPDATE SET
                campo2            = @campo2,
                FechaModificacion = GETDATE(),
                UsuarioModifica   = @usuario
        WHEN NOT MATCHED THEN
            INSERT (campo1, campo2, FechaCreacion, UsuarioCreacion)
            VALUES (@campo1, @campo2, GETDATE(), @usuario);

        SELECT 1 AS Success;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Success;
        -- RAISERROR(ERROR_MESSAGE(), 16, 1);
    END CATCH
END
GO
```

---

## Template UPDATE — Actualizacion de estatus/valor

```sql
CREATE OR ALTER PROCEDURE [mta].[SP_GRFP_UPDATE_ENTIDAD]
    @id       INT,
    @estatus  INT,
    @usuario  INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [mta].[TuTabla]
    SET
        Estatus           = @estatus,
        FechaModificacion = GETDATE(),
        UsuarioModifica   = @usuario
    WHERE Id = @id;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO
```

---

## Template EXPORT — Para CSV/Excel (columnas = headers del archivo)

```sql
CREATE OR ALTER PROCEDURE [mta].[SP_GRFP_EXPORT_ENTIDAD]
    @agencia  INT,
    @usuario  INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Los nombres de columna se usan directamente como headers del CSV
    -- Usar alias descriptivos en espanol sin acentos
    SELECT
        col1   AS [ID Sucursal],
        col2   AS [Nombre Sucursal],
        col3   AS [SKU],
        col4   AS [Descripcion],
        col5   AS [Monto]
    FROM [mta].[TuTabla]
    WHERE IdAgencia = @agencia
    ORDER BY col2, col3;
END
GO
```

---

## Template LOAD — Carga masiva con TVP

```sql
-- PASO 1: Crear el tipo de tabla (ejecutar una sola vez en BD)
IF NOT EXISTS (SELECT * FROM sys.types WHERE name = 'TVP_NombreTipo' AND schema_id = SCHEMA_ID('mta'))
CREATE TYPE [mta].[TVP_NombreTipo] AS TABLE
(
    Campo1  VARCHAR(50)   NOT NULL,
    Campo2  INT           NULL,
    Campo3  DECIMAL(18,4) NULL
);
GO

-- PASO 2: SP de carga
CREATE OR ALTER PROCEDURE [mta].[SP_GRFP_LOAD_ENTIDAD]
    @datos    [mta].[TVP_NombreTipo] READONLY,
    @usuario  INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [mta].[TuTabla] (campo1, campo2, campo3, UsuarioCreacion, FechaCreacion)
    SELECT campo1, campo2, campo3, @usuario, GETDATE()
    FROM @datos;

    SELECT @@ROWCOUNT AS RegistrosCargados;
END
GO
```

---

## Template con @flag — Multiples comportamientos

```sql
CREATE OR ALTER PROCEDURE [mta].[SP_GRFP_GET_ENTIDAD_FLAGS]
    @flag       INT,            -- 1=lista, 2=por agencia, 3=con calculo
    @agencia_id INT   = 0,
    @MontoCDR   DECIMAL(18,2) = 0,
    @usuario    INT   = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @flag = 1
    BEGIN
        -- Retorna lista completa
        SELECT * FROM [mta].[TuTabla];
    END

    IF @flag = 2
    BEGIN
        -- Retorna por agencia especifica
        SELECT * FROM [mta].[TuTabla] WHERE IdAgencia = @agencia_id;
    END

    IF @flag = 3
    BEGIN
        -- Retorna con calculo de balanceo
        SELECT *, @MontoCDR * 0.125 AS MontoCalculado
        FROM [mta].[TuTabla]
        WHERE IdAgencia = @agencia_id;
    END
END
GO
```

---

## Parametros estandar del proyecto

| Parametro             | Tipo           | Cuando usarlo                          |
|-----------------------|----------------|----------------------------------------|
| `@usuario INT`        | Auditoria      | En todo SP de escritura (obligatorio)  |
| `@flag INT`           | Modo operacion | Cuando el SP tiene multiples modos     |
| `@sucursales VARCHAR(MAX)` | Multi-filtro | Reportes/consultas por sucursal       |
| `@agencia INT`        | Filtro CDR     | Modulo Balanceo/Invenadro              |
| `@sku VARCHAR(50) = NULL` | Filtro opt.| Reportes de articulos (NULL = todos)   |

---

## Scripts utiles del proyecto

```sql
-- Ver SPs existentes en schema mta
SELECT name, create_date, modify_date
FROM sys.objects
WHERE schema_id = SCHEMA_ID('mta') AND type = 'P'
ORDER BY name;

-- Ver parametros de un SP especifico
EXEC sp_help '[mta].[SP_GRFP_GET_ALGO]';

-- Ver TVPs existentes
SELECT name FROM sys.types WHERE schema_id = SCHEMA_ID('mta') AND is_table_type = 1;
```
