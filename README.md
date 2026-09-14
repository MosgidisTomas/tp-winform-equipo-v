# TP WinForm - Catálogo de Artículos

Aplicación de escritorio en C# (WinForms, .NET Framework 4.8) para gestionar un catálogo de artículos, con marcas, categorías e imágenes. El proyecto está organizado en 3 capas:

- **`TPWinForm`** — la interfaz gráfica (formularios de Windows)
- **`negocio`** — lógica de negocio y acceso a datos (SQL Server)
- **`dominio`** — clases del modelo (`Articulo`, `Marca`, `Categoria`, `Imagen`)

## Requisitos

- Visual Studio 2022 (o superior) con la carga de trabajo **".NET desktop development"**
- SQL Server corriendo localmente (por ejemplo, en un contenedor Docker) o SQL Server Express instalado
- El script `CATALOGO_DB_v3.sql` ejecutado sobre tu instancia, para crear la base `CATALOGO_P3_DB`

## Cómo configurar tu conexión a la base de datos

La cadena de conexión (usuario y contraseña) **no está en el código ni en git** — cada integrante del equipo usa su propia instancia local, así que cada uno tiene su propio archivo de configuración que nunca se sube al repositorio.

### Paso a paso

1. Andá a la carpeta `TPWinForm/` del proyecto.
2. Vas a encontrar un archivo llamado **`conexion.local.txt.example`**. Es solo un ejemplo, así que:
   - Hacé una copia de ese archivo en la misma carpeta.
   - Renombrá la copia a **`conexion.local.txt`** (sin `.example`).

   ```
   TPWinForm/
     ├── conexion.local.txt.example   ← este SÍ está en git (es solo un ejemplo)
     └── conexion.local.txt           ← este archivo lo creás vos, NO se sube a git
   ```

3. Abrí `conexion.local.txt` con el Bloc de notas (o cualquier editor de texto) y vas a ver algo así:

   ```
   server=localhost; database=CATALOGO_P3_DB; user id=sa; password=TU_CLAVE_ACA;
   ```

4. Reemplazá `TU_CLAVE_ACA` por la contraseña real de **tu** SQL Server local (la que usás para conectarte a tu contenedor Docker, por ejemplo). Si tu servidor no se llama `localhost`, o el usuario no es `sa`, también podés cambiar esos valores.

5. Guardá el archivo. Listo — no hace falta tocar ni recompilar nada más.

### ¿Qué pasa si no creo el archivo?

Nada grave: la aplicación va a intentar conectarse automáticamente a una instancia local de **SQL Server Express** (`.\SQLEXPRESS`) usando autenticación de Windows, como respaldo. Si no tenés SQL Server Express instalado así, vas a necesitar crear tu `conexion.local.txt` como se explica arriba.

### ¿Por qué se hizo así?

Antes, la contraseña estaba escrita directamente en el código (`AccesoDatos.cs`). Eso generaba un problema: cada vez que alguien del equipo hacía un commit, **pisaba la contraseña de otro compañero** con la suya propia, y encima quedaba expuesta en el historial de git. Ahora cada uno tiene su archivo local (ignorado por git vía `.gitignore`), y el código simplemente lo lee si existe.

**Importante:** nunca subas tu `conexion.local.txt` a git ni lo compartas en el repositorio — solo `conexion.local.txt.example` debe estar versionado.

## Cómo correr el proyecto

1. Abrí `TPWinForm_equipo-v.slnx` con Visual Studio.
2. Verificá que el proyecto de inicio sea **TPWinForm** (aparece en negrita en el Explorador de soluciones; si no, clic derecho sobre `TPWinForm` → "Establecer como proyecto de inicio").
3. Configurá tu `conexion.local.txt` como se explicó arriba.
4. Presioná **F5** (o el botón ▶ Iniciar) para compilar y ejecutar.

## Estructura del proyecto

```
tp-winform-equipo-v/
├── dominio/          # Modelo de datos (POCOs): Articulo, Marca, Categoria, Imagen
├── negocio/          # Lógica de negocio y acceso a datos (AccesoDatos, *Negocio)
└── TPWinForm/         # Interfaz gráfica (formularios WinForms)
    ├── frmArticulos.cs        # Listado de artículos
    ├── frmAltaArticulo.cs     # Alta de un nuevo artículo
    └── conexion.local.txt.example  # Plantilla de configuración de conexión
```
