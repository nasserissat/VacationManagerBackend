namespace vacation_backend.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Key { get; set; } // Ej: ApproveVacation
        public string? Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
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
