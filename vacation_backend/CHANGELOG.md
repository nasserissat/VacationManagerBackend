# Changelog — Vacation Manager Backend

## [v0.2.0] — 2026-03-22

### Nuevas Entidades de Dominio

Se agregaron cuatro nuevas entidades al proyecto respetando la arquitectura en capas (Domain → Infrastructure → Application → API):

#### `CompanyPolicy`
Define la política laboral de la empresa para el cálculo automático de días hábiles.
- `WorksOnSaturdays` (bool)
- `WorksOnSundays` (bool)
- `DailyWorkHours` (int)

#### `VacationBalanceLog`
Historial de auditoría de los movimientos de días de vacaciones por empleado.
- `EmployeeId` — Empleado al que pertenece el registro.
- `DaysChanged` — Cantidad de días sumados o restados (puede ser negativo).
- `Reason` — Motivo de la transacción.
- `TransactionDate` — Fecha exacta del movimiento.
- `VacationRequestId` (nullable) — Solicitud que originó el movimiento, si aplica.

#### `VacationRequestAction`
Registro del workflow de aprobaciones. Almacena cada decisión (aprobación, rechazo, comentario) realizada sobre una solicitud.
- `VacationRequestId` — Solicitud a la que pertenece la acción.
- `ActionByUserId` — Usuario que realizó la acción.
- `ActionType` — Tipo de acción (ej. `Aprobado`, `Rechazado`, `Comentario`).
- `Comments` (nullable) — Comentario adicional del aprobador.
- `CreatedAt` — Fecha y hora de la acción.

#### `VacationRequestAttachment`
Soporte para archivos adjuntos (ej. certificados médicos) asociados a una solicitud de vacaciones.
- `VacationRequestId` — Solicitud a la que pertenece el archivo.
- `FileName` — Nombre original del archivo.
- `FilePath` — Ruta de almacenamiento en el servidor.
- `UploadedAt` — Fecha y hora de subida.

---

### Actualizaciones de Entidades Existentes

#### `VacationRequest`
- Se añadió `SubstituteEmployeeId` para registrar quién cubre el cargo durante la ausencia.
- Se añadieron las relaciones de navegación: `SubstituteEmployee`, `Attachments`, `Actions`.

#### `Employee`
- Se añadieron las relaciones de navegación: `VacationBalanceLogs`, `SubstituteVacationRequests`.

---

### Infraestructura

- Se registraron los nuevos `DbSet` en `VacationDbContext`.
- Se configuraron las relaciones foráneas usando **Fluent API** (`OnModelCreating`):
  - `VacationRequest → SubstituteEmployee` (restricción ON DELETE RESTRICT).
  - `VacationRequestAction → ActionByUser` (restricción ON DELETE RESTRICT).
- Se creó y aplicó la migración EF Core: `AddVacationSystemFeatures`.

---

### Capa de Repositorios

Se añadieron interfaces e implementaciones para cada nueva entidad:

| Interfaz | Implementación |
|---|---|
| `ICompanyPolicyRepository` | `CompanyPolicyRepository` |
| `IVacationBalanceLogRepository` | `VacationBalanceLogRepository` |
| `IVacationRequestActionRepository` | `VacationRequestActionRepository` |
| `IVacationRequestAttachmentRepository` | `VacationRequestAttachmentRepository` |

---

### Capa de Servicios

- **`CompanyPolicyService`** (nuevo): Obtiene y actualiza la política de la empresa.
- **`EmployeeService`** (actualizado): Se añadió `GetEmployeeBalanceLogsAsync()` para consultar el historial de días.
- **`VacationService`** (actualizado): Se añadieron:
  - `AddVacationRequestActionAsync()` — Registra comentarios o decisiones sobre solicitudes.
  - `AddVacationRequestAttachmentAsync()` — Asocia archivos adjuntos a solicitudes.

---

### Capa de API (Controllers)

- **`CompanyPolicyController`** (nuevo):
  - `GET /api/CompanyPolicy` — Obtiene la política vigente.
  - `PUT /api/CompanyPolicy` — Actualiza la política.

---

### DTOs

Se crearon los siguientes objetos de transferencia de datos en `Application/DTOs`:

- `CompanyPolicyDto` / `CompanyPolicyUpdateDto`
- `VacationBalanceLogDto`
- `VacationRequestActionDto` / `CreateVacationRequestActionDto`
- `VacationRequestAttachmentDto`

---

### Inyección de Dependencias

Se registraron todos los nuevos repositorios y servicios en `AddScoped` dentro de:
- `Application/DependencyInjection.cs`
- `Infraestructure/DependencyInjection.cs`

---

### Autenticación — Integración de BCrypt

- Se instaló el paquete `BCrypt.Net-Next v4.1.0` en el proyecto backend y en `VacationSeeder`.
- Se actualizó `AuthService.LoginAsync` para verificar contraseñas con **BCrypt**:

  ```diff
  - if (user.PasswordHash != loginRequest.Password)
  + if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
  ```

- La base de datos fue reseteada y re-sembrada mediante el seeder, que ya generaba los hashes correctamente usando `BCrypt.Net.BCrypt.HashPassword`.
- Login verificado correctamente con `admin / admin123` vía `POST /api/Auth/login`.

---

### Datos de Prueba (VacationSeeder)

Se creó `DatabaseSeederNewEntities.cs` en el proyecto `VacationSeeder` que siembra datos iniciales para:
- 1 Política de empresa (lunes a viernes, 8 horas).
- 5 Historiales de balance (uno por empleado).
- 3 Acciones/comentarios en solicitudes existentes.
- 1 Archivo adjunto de prueba (`certificado_medico.pdf`).

---

## [v0.1.0] — Initial Setup

- Configuración inicial del proyecto ASP.NET Core 7.
- Conexión a SQL Server local (`DANIELABREUPC\SQLEXPRESS`) con Windows Authentication.
- `appsettings.json` excluido de Git mediante `.gitignore`.
- Swagger habilitado para pruebas interactivas de la API.
- Entidades base: `Employee`, `Department`, `Role`, `User`, `VacationRequest`, `Holiday`, `ExtraBenefitDay`.
- Migración inicial aplicada.
- Seeder de datos base ejecutado.
