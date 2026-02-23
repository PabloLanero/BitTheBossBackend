using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service.Common;

namespace BTB.Service
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _repo;

        public MovimientoService(IMovimientoRepository repo)
        {
            _repo = repo;
        }

        public async Task<MovimientoDTOOut> AddMovimientoAsync(MovimientoDTOIn dto)
        {
            if (dto == null) throw new ValidationException("MovimientoDTOIn no puede ser null");
            var model = new Movimiento { Tropa = new Tropa { Nombre = dto.Tropa?.Nombre ?? string.Empty, Vida = dto.Tropa?.Vida ?? 0, Damage = dto.Tropa?.Damage ?? 0 }, NodoDestino = new Nodo { IdNodo = (byte)dto.NodoDestinoId } };
            await _repo.PostMovimientoAsync(model);
            return new MovimientoDTOOut { Id = model.Id, Tropa = new TropaDTOOut { Id = model.Tropa.Id, Nombre = model.Tropa.Nombre, Vida = model.Tropa.Vida, Damage = model.Tropa.Damage }, NodoDestinoId = model.NodoDestino.IdNodo };
        }

        public Task<bool> DeleteMovimientoAsync(int id)
        {
            if (id <= 0) throw new ValidationException("Id inválido para eliminar movimiento");
            return _repo.DeleteMovimientoAsync(id);
        }

        public async Task<MovimientoDTOOut?> GetMovimientoByIdAsync(int id)
        {
            var m = await _repo.GetMovimientoByIdAsync(id);
            if (m == null) return null;
            return new MovimientoDTOOut { Id = m.Id, Tropa = new TropaDTOOut { Id = m.Tropa.Id, Nombre = m.Tropa.Nombre, Vida = m.Tropa.Vida, Damage = m.Tropa.Damage }, NodoDestinoId = m.NodoDestino.IdNodo };
        }

        public async Task<List<MovimientoDTOOut>> GetMovimientosAsync()
        {
            var lst =  _repo.GetMovimientosAsync();
            return lst.Select(m => new MovimientoDTOOut { Id = m.Id, Tropa = new TropaDTOOut { Id = m.Tropa.Id, Nombre = m.Tropa.Nombre, Vida = m.Tropa.Vida, Damage = m.Tropa.Damage }, NodoDestinoId = m.NodoDestino.IdNodo }).ToList();
        }
    }
}
