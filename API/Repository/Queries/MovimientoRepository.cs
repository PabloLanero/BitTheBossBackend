using BTB.Entities.Models;
using BTB.Repository.Interfaces;

namespace BTB.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly List<Movimiento> _lst = new List<Movimiento>();
        private int _id = 1;

        public MovimientoRepository() {}

        public Task<bool> DeleteMovimientoAsync(int id)
        {
            var existing = _lst.FirstOrDefault(m => m.Id == id);
            if (existing == null) return Task.FromResult(false);
            _lst.Remove(existing);
            return Task.FromResult(true);
        }

        public Task<Movimiento?> GetMovimientoByIdAsync(int id)
        {
            var m = _lst.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(m);
        }

        public Task<List<Movimiento>> GetMovimientosAsync()
        {
            return Task.FromResult(_lst.ToList());
        }

        public Task<bool> PostMovimientoAsync(Movimiento movimiento)
        {
            movimiento.Id = _id++;
            _lst.Add(movimiento);
            return Task.FromResult(true);
        }

        public Task<bool> PutMovimientoAsync(Movimiento movimiento)
        {
            var existing = _lst.FirstOrDefault(m => m.Id == movimiento.Id);
            if (existing == null) return Task.FromResult(false);
            existing.Tropa = movimiento.Tropa;
            existing.NodoDestino = movimiento.NodoDestino;
            return Task.FromResult(true);
        }
    }
}
