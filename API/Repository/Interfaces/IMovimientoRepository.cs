using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface IMovimientoRepository
    {
        public List<Movimiento> GetMovimientosAsync();
        public Task<Movimiento?> GetMovimientoByIdAsync(int id);
        public Task<Movimiento> PostMovimientoAsync(Movimiento movimiento);
        public Task<Movimiento> PutMovimientoAsync(Movimiento movimiento);
        public Task<bool> DeleteMovimientoAsync(int id);
    }
}
