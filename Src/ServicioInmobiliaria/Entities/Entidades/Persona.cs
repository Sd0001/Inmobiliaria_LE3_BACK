using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_person")]
    public class Persona
    {
        [Key]
        [Column("per_id")]
        public int Id { get; set; }
        [Column("per_identi")]
        public string Identificacion { get; set; }
        [Column("per_nombre")]
        public string Nombre { get; set; }
        [Column("per_apelli")]
        public string Apellido { get; set; }
        [Column("per_telefo")]
        public string Telefono { get; set; }
        [Column("per_direcc")]
        public string Direccion { get; set; }
        [Column("per_email")]
        public string Email { get; set; }
        [Column("per_paswor")]
        public string Password { get; set; }
    }
}
