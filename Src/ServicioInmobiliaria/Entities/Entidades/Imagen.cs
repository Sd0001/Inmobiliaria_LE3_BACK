using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_imagen")]
    public class Imagen
    {
        [Key]
        [Column("img_id")]
        public int Id { get; set; }

        [Column("ofe_id")]
        public int IdOferta { get; set; }

        [Column("img_ruta")]
        public string Ruta { get; set; }      
    }
}
