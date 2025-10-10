using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
    public class ExtraBenefitDay
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;  // Ej: "Día de verano", "Cumpleaños", "Día puente"
        public int DaysGranted { get; set; } = 1;  // Cuántos días otorga
        public bool HasAvailableDays => DaysGranted > 0; // Para saber si le quedan días extras disponibles
        public DateTime? ValidFrom { get; set; }   // Ej: 1 de junio
        public DateTime? ValidTo { get; set; }     // Ej: 31 de agosto
        public VacationStatusEnum Status{ get; set; }

        public virtual Employee Employee { get; set; } = null!;
     /***
        Ejemplo de casos de uso: 
         * Día Puente
         * Día de Verano
         * Cumpleaños
         * Graduación
     ***/
    }

}
