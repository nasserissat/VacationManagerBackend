using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public VacationType VacationType { get; set; } //Indicar si tomará vacaciones normales o parte de los beneficios de días extras
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays => (EndDate - StartDate).Days + 1;
        public VacationStatusEnum Status { get; set; }

        public int EmployeeId { get; set; } // Autor de la solicitud

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Fecha en que se creó la solicitud
        public int CreatedBy { get; set; } // Usuario o empleado que creó la solicitud


        public DateTime? DecisionDate { get; set; } // Fecha en que fue aprobada o rechazada la solicitud
        public int? ApprovedById { get; set; } // Usuario (o empleado) que la aprobó o rechazó

        public DateTime? LastModifiedAt { get; set; }  // Fecha en que fue modificada por última vez la solicitud
        public int? LastModifiedById { get; set; } // Usuario (o empleado) que modificó por última vez la solicitud

        public int? ExtraBenefitDayId { get; set; } // Si en "VacationType" selecció un día extra, lo enlazo con la entidad correspondiente

        public virtual Employee Employee { get; set; }
        public virtual User? ApprovedBy { get; set; }
        public virtual User? LastModifiedBy { get; set; }

 
    }
}
