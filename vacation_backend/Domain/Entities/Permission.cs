namespace vacation_backend.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Key { get; set; } // Ej: ApproveVacation
        public string? Description { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        /***
         Ejemplo de casos de uso: 
            * CrearSolicitudVacaciones
            * AprobarSolicitudVacaciones
            * RechazarSolicitudVacaciones
            * CancelarSolictudVacaciones
            * AsignarRoles
            * CrearReporte
            * CrearEmpleado
            * EliminarEmpleado
            * EditarEmpelado
        ***/
    }
}
