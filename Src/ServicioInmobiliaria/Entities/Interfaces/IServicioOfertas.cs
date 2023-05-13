namespace Inmobiliaria.Entities.Interfaces
{
    public interface IServicioOfertas : IServicio<Oferta>
    {
        Respuesta<IEnumerable<Oferta>> ListarActivas();
    }
    
}
