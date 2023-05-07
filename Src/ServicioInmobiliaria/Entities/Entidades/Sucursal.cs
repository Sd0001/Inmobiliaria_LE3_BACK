using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_sucurs")]
    public class Sucursal
    {
        [Key]
        [Column("suc_id")]
        public int Id { get; set; }

        [Column("est_id")]
        [ForeignKey("Estado")]
        public int IdEstado { get; set; }

        [Column("suc_nombre")]
        public string Nombre { get; set; }

        [Column("suc_direcc")]
        public string Direccion { get; set; }

        [Column("suc_telefo")]
        public string Telefono { get; set; }
        public Estado Estado { get; set; }
    }
}
