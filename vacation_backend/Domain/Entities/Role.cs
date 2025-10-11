namespace vacation_backend.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        /***
         * Director
         * Gerente
         * Subgerente
         * Auditor
         ***/

    }
}
