# NetSeed

---

## Descripción del Proyecto

NetSeed es una plantilla base (**Seed**) profesional para el desarrollo de APIs RESTful en **.NET 8**.

Este proyecto no es un tutorial ni una demostración de características. Es una infraestructura agnóstica de alto nivel, diseñada siguiendo estrictamente los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**. Su propósito es servir como punto de partida production-grade para equipos que requieren una arquitectura sólida sin deuda técnica inicial ni lógica de negocio preexistente que deba ser eliminada.

## Filosofía de Diseño

Este seed es opinionated en su arquitectura, pero neutral en su negocio.

- **Sin lógica "Dummy":** No encontrará controladores ni entidades de ejemplo. El dominio está intencionalmente vacío.
- **Sin Autenticación forzada:** No se incluye JWT ni integración con Identity, permitiendo que el arquitecto decida el proveedor de seguridad más adecuado.

## Arquitectura

La solución implementa una separación estricta de responsabilidades en cuatro capas concéntricas:

1. **NetSeed.Domain:** Núcleo del negocio. Contiene Entidades, Value Objects y Reglas de Negocio. No posee dependencias externas.
2. **NetSeed.Application:** Casos de Uso, interfaces de repositorios y orquestación. Depende exclusivamente de Domain.
3. **NetSeed.Infrastructure:** Implementación de detalles técnicos (Persistencia EF Core, Cliente MySQL, adaptadores externos).
4. **NetSeed.Api:** Capa de presentación y entrada. Configura el contenedor de inyección de dependencias y expone los endpoints HTTP.

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
DB_NAME=netseed_db
DB_USER=root
DB_PASSWORD=secret_password
```

## Ejecución y Despliegue

### Escenario A: Producción y Entornos Cloud (Recomendado)

En este escenario, la aplicación se ejecuta dentro de un contenedor Docker, pero se conecta a una base de datos gestionada externamente (AWS RDS, Azure SQL for MySQL, o una instancia dedicada).

**Construcción y ejecución:**
El `Dockerfile` provisto es "Production-Ready". Utiliza multi-stage builds para generar una imagen ligera y segura (non-root).

```bash
docker build -t netseed-api .
docker run -p 8080:8080 -e DB_HOST=mi-servidor-db -e DB_PASSWORD=xxx netseed-api
```

### Escenario B: Desarrollo Local (Sin Docker)

Para el desarrollo diario ("Code & Run"), se recomienda ejecutar la aplicación directamente sobre el SDK de .NET para aprovechar el Hot Reload y la depuración nativa.

1. Asegúrese de tener una instancia de MySQL accesible.
2. Configure su archivo `.env` con las credenciales correspondientes.
3. Ejecute la API:

```bash
dotnet restore
dotnet run --project src/NetSeed.Api
```

### Escenario C: Entorno Local con Docker Compose (Opcional)

El archivo `compose.yaml` se incluye como una utilidad para levantar la API rápidamente en un entorno aislado.

> **Nota:** En la versión actual (v1.0), el archivo `compose.yaml` levanta únicamente la API y espera que la base de datos se provea externamente.
