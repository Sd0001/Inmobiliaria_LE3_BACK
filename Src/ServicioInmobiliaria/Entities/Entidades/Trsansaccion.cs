using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_transa")]
    public class Transaccion
    {
        [Key]
        [Column("tra_id")]
        public int Id { get; set; }

        [Column("ofe_id")]
        [ForeignKey("Oferta")]
        public int IdOferta { get; set; }

        [Column("tpt_id")]
        [ForeignKey("TipoTransaccion")]
        public int IdTipoTransaccion { get; set; }

        [Column("per_id")]
        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [Column("est_id")]
        [ForeignKey("Estado")]
        public int IdEstado { get; set; }

        [Column("tra_fecha")]
        public DateTime Fecha { get; set; }

        [Column("tra_monto")]
        public decimal Monto { get; set; }
        public Oferta? Oferta { get; set; }
        public Estado? Estado { get; set; }
        public TipoTransaccion? TipoTransaccion { get; set; }
        public Persona? Persona { get; set; }
    }
}
