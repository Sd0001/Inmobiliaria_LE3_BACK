using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_estado")]
    public class Estado
    {
        [Key]
        [Column("est_id")]
        public int Id { get; set; }
        [Column("est_nombre")]
        public string Nombre { get; set; }
    }
}
