using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service.Common;

namespace BTB.Service
{
    public class NodoService : INodoService
    {
        private readonly INodoRepository _repo;

        public NodoService(INodoRepository repo)
        {
            _repo = repo;
        }

        public async Task<NodoDTOOut> AddNodoAsync(NodoDTOIn dto)
        {
            if (dto == null) throw new ValidationException("NodoDTOIn no puede ser null");
            var model = new Nodo { IdNodo = (byte)dto.IdNodo, ArrTropas = (dto.ArrTropas ?? new List<TropaDTOIn>()).Select(t => new Tropa { Nombre = t.Nombre ?? string.Empty, Vida = t.Vida, Damage = t.Damage }).ToArray(), DuenoNodo = dto.DuenoNodo == null ? null : new Usuario { UsuarioId = dto.DuenoNodo.Id ?? 0, Nombre = dto.DuenoNodo.Nombre ?? string.Empty, Correo = dto.DuenoNodo.Correo ?? string.Empty } };
            await _repo.PostNodoAsync(model);
            return new NodoDTOOut { IdNodo = model.IdNodo, ArrTropas = model.ArrTropas?.Select(t => new TropaDTOOut { Id = t.Id, Nombre = t.Nombre, Vida = t.Vida, Damage = t.Damage }).ToList() ?? new List<TropaDTOOut>(), DuenoNodo = model.DuenoNodo == null ? null : new UsuarioRefDTOOut { Id = model.DuenoNodo.UsuarioId, Nombre = model.DuenoNodo.Nombre, Correo = model.DuenoNodo.Correo } };
        }

        public Task<bool> DeleteNodoAsync(int id)
        {
            if (id <= 0) throw new ValidationException("Id inválido para eliminar nodo");
            return _repo.DeleteNodoAsync(id);
        }

        public async Task<NodoDTOOut?> GetNodoByIdAsync(int id)
        {
            var n = await _repo.GetNodoByIdAsync(id);
            if (n == null) return null;
            return new NodoDTOOut { IdNodo = n.IdNodo, ArrTropas = n.ArrTropas?.Select(t => new TropaDTOOut { Id = t.Id, Nombre = t.Nombre, Vida = t.Vida, Damage = t.Damage }).ToList() ?? new List<TropaDTOOut>(), DuenoNodo = n.DuenoNodo == null ? null : new UsuarioRefDTOOut { Id = n.DuenoNodo.UsuarioId, Nombre = n.DuenoNodo.Nombre, Correo = n.DuenoNodo.Correo } };
        }

        public async Task<List<NodoDTOOut>> GetNodosAsync()
        {
            var lst =  _repo.GetNodosAsync();
            return lst.Select(n => new NodoDTOOut { IdNodo = n.IdNodo, ArrTropas = n.ArrTropas?.Select(t => new TropaDTOOut { Id = t.Id, Nombre = t.Nombre, Vida = t.Vida, Damage = t.Damage }).ToList() ?? new List<TropaDTOOut>(), DuenoNodo = n.DuenoNodo == null ? null : new UsuarioRefDTOOut { Id = n.DuenoNodo.UsuarioId, Nombre = n.DuenoNodo.Nombre, Correo = n.DuenoNodo.Correo } }).ToList();
        }

        public async Task<NodoDTOOut> UpdateNodoAsync(int id, NodoDTOIn dto)
        {
            if (id <= 0) throw new ValidationException("Id inválido para actualizar nodo");
            var model = new Nodo { IdNodo = (byte)id, ArrTropas = (dto.ArrTropas ?? new List<TropaDTOIn>()).Select(t => new Tropa { Nombre = t.Nombre ?? string.Empty, Vida = t.Vida, Damage = t.Damage }).ToArray(), DuenoNodo = dto.DuenoNodo == null ? null : new Usuario { UsuarioId = dto.DuenoNodo.Id ?? 0, Nombre = dto.DuenoNodo.Nombre ?? string.Empty, Correo = dto.DuenoNodo.Correo ?? string.Empty } };
            Nodo nodo = await _repo.PutNodoAsync(model);
            NodoDTOOut nodoDTOOut = new NodoDTOOut
            {
                IdNodo= nodo.IdNodo,
                ArrTropas = nodo.ArrTropas?.Select(t => new TropaDTOOut{Id = t.Id, Nombre=t.Nombre, Damage=t.Damage, Vida=t.Vida}).ToList(),
                DuenoNodo = new UsuarioRefDTOOut {Id=nodo.DuenoNodo?.UsuarioId, Nombre= nodo.DuenoNodo?.Nombre, Correo= nodo.DuenoNodo?.Correo}
            };
            return nodoDTOOut;
        }
    }
}
