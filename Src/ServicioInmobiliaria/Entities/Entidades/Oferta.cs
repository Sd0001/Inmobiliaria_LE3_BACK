using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inmobiliaria.Entities
{
    [Table("tbl_oferta")]
    public class Oferta
    {
        [Key]
        [Column("ofe_id")]
        public int Id { get; set; }

        [Column("inm_id")]
        [ForeignKey("Inmueble")]
        public int IdInmueblle { get; set; }

        [Column("est_id")]
        [ForeignKey("Estado")]
        public int IdEstado { get; set; }

        [Column("ofe_fecini")]
        public DateTime FechaInicio { get; set; }

        [Column("ofe_fecfin")]
        public DateTime? Fechafin { get; set; }

        [Column("ofe_movent")]
        public decimal? MontoVenta { get; set; }

        [Column("ofe_moalqu")]
        public decimal? Montoalquiler { get; set; }

        [Column("ofe_esvent")]
        public bool EsAlquiler { get; set; }

        [Column("ofe_esalqu")]
        public bool EsVenta { get; set; }
        public Estado? Estado { get; set; }
        public Inmueble? Inmueble { get; set; }

        [JsonIgnore]
        [InverseProperty("Oferta")]
        public ICollection<Transaccion>? Transacciones { get; set; }

        [InverseProperty("Oferta")]
        public ICollection<Imagen>? Imagenes { get; set; }

        [NotMapped]
        public bool EstaVendida { get {
                if(Transacciones!= null)
                return Transacciones.Any(); 
                return false;
            } }
    }
}
