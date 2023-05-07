using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Entities
{
    [Table("tbl_inmueb")]
    public class Inmueble
    {
        [Key]
        [Column("inm_id")]
        public int Id { get; set; }

        [Column("suc_id")]
        [ForeignKey("Sucursal")]
        public int IdSucursal { get; set; }

        [Column("tpi_id")]
        [ForeignKey("TipoInmueble")]
        public int IdTipoInmueble { get; set; }

        [Column("per_id")]
        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [Column("est_id")]
        [ForeignKey("Estado")]
        public int IdEstado { get; set; }

        [Column("imn_refere")]
        public string Referencia { get; set; }
        [Column("imn_direcc")]
        public string Direccion { get; set; }
        [Column("imn_superf")]
        public int Superficie { get; set; }
        [Column("imn_nhabit")]
        public byte NroHabitaciones { get; set; }
        [Column("imn_nbanio")]
        public byte NroBanios { get; set; }
        [Column("imn_ncocin")]
        public byte NroCocinas { get; set; }
        [Column("imn_tngas")]
        public bool TieneGas { get; set; }
        [Column("imn_tnparq")]
        public bool TieneParqueadero { get; set; }

        public Sucursal Sucursal { get; set; }
        public TipoInmueble TipoInmueble { get; set; }
        public Persona Persona { get; set; }
        public Estado Estado { get; set; }
    }
}
