CREATE PROCEDURE [dbo].[SP_CONSULTA_FALTAS_PERMISOS_AGRUPADOS]
(
    @FECHA_INICIO DATE = NULL,
    @FECHA_FIN DATE = NULL,
    @CENTRO_COSTO_ID DECIMAL(12,0) = NULL,
    @COLABORADOR_ID DECIMAL(12,0) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- CTE para obtener datos base con numeración secuencial
    WITH DatosBase AS (
        SELECT 
            a.RHCOL_ID,
            a.RHASICON_ID,
            a.RHSIS_FECHA,
            a.RHSIS_OBSERVACION,
            -- Aquí deberías agregar los campos del colaborador y centro de costo según tus tablas
            -- c.COLABORADOR_NOMBRE,
            -- cc.CENTRO_COSTO_NOMBRE,
            ROW_NUMBER() OVER (
                PARTITION BY a.RHCOL_ID, a.RHASICON_ID 
                ORDER BY a.RHSIS_FECHA
            ) as NumSecuencial
        FROM RH_ASISTENCIA a
        -- INNER JOIN [TABLA_COLABORADORES] c ON a.RHCOL_ID = c.RHCOL_ID
        -- INNER JOIN [TABLA_CENTRO_COSTO] cc ON c.CENTRO_COSTO_ID = cc.CENTRO_COSTO_ID
        WHERE 1=1
            AND (@FECHA_INICIO IS NULL OR a.RHSIS_FECHA >= @FECHA_INICIO)
            AND (@FECHA_FIN IS NULL OR a.RHSIS_FECHA <= @FECHA_FIN)
            AND (@CENTRO_COSTO_ID IS NULL OR EXISTS (
                SELECT 1 FROM [TABLA_COLABORADORES] tc 
                WHERE tc.RHCOL_ID = a.RHCOL_ID 
                AND tc.CENTRO_COSTO_ID = @CENTRO_COSTO_ID
            ))
            AND (@COLABORADOR_ID IS NULL OR a.RHCOL_ID = @COLABORADOR_ID)
            -- Filtrar solo faltas y permisos según tu catálogo de conceptos
            AND a.RHASICON_ID IN (
                -- Aquí debes poner los IDs que corresponden a faltas y permisos
                -- Por ejemplo: 1, 2, 3, 4 (ajustar según tu catálogo)
                SELECT RHASICON_ID FROM [TABLA_CONCEPTOS_ASISTENCIA] 
                WHERE CONCEPTO_TIPO IN ('FALTA', 'PERMISO')
            )
    ),
    
    -- CTE para crear grupos de fechas contiguas
    GruposContiguos AS (
        SELECT 
            RHCOL_ID,
            RHASICON_ID,
            RHSIS_FECHA,
            RHSIS_OBSERVACION,
            -- Crear grupo: restar el número secuencial a la fecha para agrupar fechas contiguas
            DATEADD(DAY, -NumSecuencial, RHSIS_FECHA) as GrupoContiguo
        FROM DatosBase
    ),
    
    -- CTE para agrupar períodos contiguos
    PeriodosAgrupados AS (
        SELECT 
            RHCOL_ID,
            RHASICON_ID,
            GrupoContiguo,
            MIN(RHSIS_FECHA) as FechaInicio,
            MAX(RHSIS_FECHA) as FechaFin,
            COUNT(*) as DiasTotal,
            -- Concatenar observaciones si hay múltiples
            STRING_AGG(
                CASE 
                    WHEN RHSIS_OBSERVACION IS NOT NULL AND RTRIM(RHSIS_OBSERVACION) != ''
                    THEN RHSIS_OBSERVACION 
                    ELSE NULL 
                END, 
                ' | '
            ) WITHIN GROUP (ORDER BY RHSIS_FECHA) as ObservacionesConcatenadas
        FROM GruposContiguos
        GROUP BY RHCOL_ID, RHASICON_ID, GrupoContiguo
    )
    
    -- Consulta final con información completa
    SELECT 
        p.RHCOL_ID,
        -- c.COLABORADOR_NOMBRE,
        -- c.COLABORADOR_RUT,
        -- cc.CENTRO_COSTO_ID,
        -- cc.CENTRO_COSTO_NOMBRE,
        p.RHASICON_ID,
        -- ca.CONCEPTO_NOMBRE,
        -- ca.CONCEPTO_TIPO,
        p.FechaInicio,
        p.FechaFin,
        p.DiasTotal,
        CASE 
            WHEN p.FechaInicio = p.FechaFin 
            THEN CONVERT(VARCHAR(10), p.FechaInicio, 103)
            ELSE CONVERT(VARCHAR(10), p.FechaInicio, 103) + ' al ' + CONVERT(VARCHAR(10), p.FechaFin, 103)
        END as RangoFechas,
        p.ObservacionesConcatenadas,
        -- Campos adicionales útiles
        CASE 
            WHEN p.DiasTotal = 1 THEN 'Día único'
            WHEN p.DiasTotal <= 3 THEN 'Período corto'
            WHEN p.DiasTotal <= 7 THEN 'Período medio'
            ELSE 'Período largo'
        END as TipoPeriodo
    FROM PeriodosAgrupados p
    -- INNER JOIN [TABLA_COLABORADORES] c ON p.RHCOL_ID = c.RHCOL_ID
    -- INNER JOIN [TABLA_CENTRO_COSTO] cc ON c.CENTRO_COSTO_ID = cc.CENTRO_COSTO_ID
    -- INNER JOIN [TABLA_CONCEPTOS_ASISTENCIA] ca ON p.RHASICON_ID = ca.RHASICON_ID
    ORDER BY 
        p.RHCOL_ID,
        -- cc.CENTRO_COSTO_NOMBRE,
        p.RHASICON_ID,
        p.FechaInicio;

END
GO

-- Ejemplo de uso:
-- EXEC SP_CONSULTA_FALTAS_PERMISOS_AGRUPADOS 
--     @FECHA_INICIO = '2024-01-01',
--     @FECHA_FIN = '2024-12-31',
--     @CENTRO_COSTO_ID = 100,
--     @COLABORADOR_ID = NULL

/*
EXPLICACIÓN DEL SP:

1. **DatosBase CTE**: 
   - Obtiene registros de asistencia filtrados
   - Asigna número secuencial por colaborador y concepto

2. **GruposContiguos CTE**:
   - Crea grupos restando el número secuencial a la fecha
   - Las fechas contiguas tendrán el mismo valor de grupo

3. **PeriodosAgrupados CTE**:
   - Agrupa por colaborador, concepto y grupo contiguo
   - Calcula fecha inicio, fin, días totales
   - Concatena observaciones del período

4. **Consulta Final**:
   - Une con tablas maestras (colaboradores, centros de costo, conceptos)
   - Formatea las fechas y agrega campos calculados

CAMPOS DE SALIDA:
- RHCOL_ID: ID del colaborador
- COLABORADOR_NOMBRE: Nombre del colaborador
- CENTRO_COSTO_ID/NOMBRE: Centro de costo
- RHASICON_ID: ID del concepto (falta/permiso)
- CONCEPTO_NOMBRE: Nombre del concepto
- FechaInicio/FechaFin: Rango del período
- DiasTotal: Cantidad de días del período
- RangoFechas: Formato legible del rango
- ObservacionesConcatenadas: Observaciones del período
- TipoPeriodo: Clasificación por duración

CASOS DE USO:
- Si colaborador tiene permisos: 01/01, 02/01, 03/01 → 1 registro (3 días)
- Si colaborador tiene permisos: 01/01, 02/01, 05/01 → 2 registros (2 días + 1 día)
*/