using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
    public class ExtraBenefitDay
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;  // Ej: "Día de verano", "Cumpleaños", "Día puente"
        public int DaysGranted { get; set; } = 1;  // Cuántos días otorga
        public DateTime? ValidFrom { get; set; }   // Ej: 1 de junio
        public DateTime? ValidTo { get; set; }     // Ej: 31 de agosto
        public StatusEnum Status{ get; set; } = StatusEnum.Active;
        public ICollection<EmployeeExtraBenefitDay>? EmployeeExtraBenefitDays { get; set; }

        /***
           Ejemplo de casos de uso: 
            * Día Puente
            * Día de Verano
            * Cumpleaños
            * Graduación
        ***/
    }
}
