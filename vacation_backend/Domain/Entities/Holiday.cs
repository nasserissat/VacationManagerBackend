using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
    // Esta entidad sirve para poder registrar los días feriados del año para que cuando un empleado seleccione una fecha donde se encuentre un día feriado no se cuente ese día de sus vacaciones. 
    public class Holiday
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; } // ej dia de las mercedes, dia de independencia, semana santa, dia de duarte, reyes, navidad etc...
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // en caso de semana santa son dos dias seguidos normalmente de jueves a viernes laboral por eso puse EndDate como opcional porque la mayoria dura solo 1 día
        public StatusEnum Status { get; set; }
    }
}
