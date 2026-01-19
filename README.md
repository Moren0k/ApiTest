# ApiTest

---

## Descripción del Proyecto

lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.

## Arquitectura

La solución implementa una separación estricta de responsabilidades en cuatro capas concéntricas:

1. **ApiTest.Domain:** Núcleo del negocio. Contiene Entidades, Value Objects y Reglas de Negocio. No posee dependencias externas.
2. **ApiTest.Application:** Casos de Uso, interfaces de repositorios y orquestación. Depende exclusivamente de Domain.
3. **ApiTest.Infrastructure:** Implementación de detalles técnicos (Persistencia EF Core, Cliente MySQL, adaptadores externos).
4. **ApiTest.Api:** Capa de presentación y entrada. Configura el contenedor de inyección de dependencias y expone los endpoints HTTP.

## Requisitos Previos

- .NET SDK 8.0 o superior.
- Motor de Base de Datos MySQL (local o remoto).
- Docker (Opcional, para contenedorización).

## Configuración

El proyecto utiliza un enfoque **Fail-Fast** para la configuración. La aplicación no iniciará si faltan variables de entorno críticas.

En el entorno de desarrollo, se utiliza un archivo `.env` en la raíz de la solución.

**Ejemplo de archivo .env:**

```env
DB_HOST=localhost
DB_PORT=3306
DB_NAME=ApiTest_db
DB_USER=root
DB_PASSWORD=secret_password
```

## Ejecución y Despliegue

### Escenario A: Producción y Entornos Cloud (Recomendado)

En este escenario, la aplicación se ejecuta dentro de un contenedor Docker, pero se conecta a una base de datos gestionada externamente (AWS RDS, Azure SQL for MySQL, o una instancia dedicada).

**Construcción y ejecución:**
El `Dockerfile` provisto es "Production-Ready". Utiliza multi-stage builds para generar una imagen ligera y segura (non-root).

```bash
docker build -t apitest-api .
docker run -p 8080:8080 -e DB_HOST=mi-servidor-db -e DB_PASSWORD=xxx ApiTest-api
```

### Escenario B: Desarrollo Local (Sin Docker)

Para el desarrollo diario ("Code & Run"), se recomienda ejecutar la aplicación directamente sobre el SDK de .NET para aprovechar el Hot Reload y la depuración nativa.

1. Asegúrese de tener una instancia de MySQL accesible.
2. Configure su archivo `.env` con las credenciales correspondientes.
3. Ejecute la API:

```bash
dotnet restore
dotnet run --project src/ApiTest.Api
```

### Escenario C: Entorno Local con Docker Compose (Opcional)

El archivo `compose.yaml` se incluye como una utilidad para levantar la API rápidamente en un entorno aislado.

> **Nota:** En la versión actual (v1.0), el archivo `compose.yaml` levanta únicamente la API y espera que la base de datos se provea externamente.

---

### Generar la Migración

Desde la **raíz de la solución**, ejecutar:

```bash
dotnet ef migrations add NombreDescriptivoDeLaMigracion \
  --project src/ApiTest.Infrastructure \
  --startup-project src/ApiTest.Api
```

### Aplicar Migraciones a la Base de Datos

Para aplicar las migraciones pendientes:

```bash
dotnet ef database update \
  --project src/ApiTest.Infrastructure \
  --startup-project src/ApiTest.Api
```

### Comando de Conexión

Utilizando las variables de entorno proporcionadas, el comando de conexión es el siguiente:

```bash
mysql \
  -h netseed-morenok.i.aivencloud.com \
  -P 14463 \
  -u avnadmin \
  -p defaultdb
```

Al ejecutar el comando, el cliente solicitará la contraseña:

```bash
Enter password:
```

Ingrese el valor correspondiente a DB_PASSWORD.
