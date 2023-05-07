using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_tptran")]
    public class TipoTransaccion
    {
        [Key]
        [Column("tpt_id")]
        public int Id { get; set; }
        [Column("tpt_nombre")]
        public string Nombre { get; set; }
    }
}
