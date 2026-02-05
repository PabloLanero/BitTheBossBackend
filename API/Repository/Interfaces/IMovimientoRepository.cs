using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface IMovimientoRepository
    {
        public Task<List<Movimiento>> GetMovimientosAsync();
        public Task<Movimiento?> GetMovimientoByIdAsync(int id);
        public Task<bool> PostMovimientoAsync(Movimiento movimiento);
        public Task<bool> PutMovimientoAsync(Movimiento movimiento);
        public Task<bool> DeleteMovimientoAsync(int id);
    }
}
