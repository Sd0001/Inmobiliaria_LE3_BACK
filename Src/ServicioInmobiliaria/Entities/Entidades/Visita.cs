using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_visita")]
    public class Visita
    {
        [Key]
        [Column("vis_id")]
        public int Id { get; set; }

        [Column("ofe_id")]
        [ForeignKey("Oferta")]
        public int IdOferta { get; set; }

        [Column("per_id")]
        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [Column("vis_fecha")]
        public DateTime Fecha { get; set; }

        [Column("vis_realiz")]
        public bool Realizada { get; set; }

        [Column("vis_coment")]
        public string? Comentario { get; set; }
        
        public Persona? Persona { get; set; }
        public Oferta? Oferta { get; set; }
    }
}
