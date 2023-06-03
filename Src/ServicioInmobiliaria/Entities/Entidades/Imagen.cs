using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inmobiliaria.Entities
{
    [Table("tbl_imagen")]
    public class Imagen
    {
        [Key]
        [Column("img_id")]
        public int Id { get; set; }

        [Column("ofe_id")]
        [ForeignKey("Oferta")]
        public int IdOferta { get; set; }

        [Column("img_ruta")]
        public string Ruta { get; set; }
        [JsonIgnore]
        public Oferta Oferta { get; set; }
    }
}
