using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_tpinmu")]
    public class TipoInmueble
    {
        [Key]
        [Column("tpi_id")]
        public int Id { get; set; }
        [Column("tpi_nombre")]
        public string Nombre { get; set; }
    }
}
