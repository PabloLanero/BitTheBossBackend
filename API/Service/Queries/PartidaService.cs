using System.Threading.Tasks;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service.Common;

namespace BTB.Service
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repository;
        private readonly INodoService _nodoService;
        private readonly IUsuarioService _usuarioService;

        public PartidaService(IPartidaRepository repository, INodoService nodoService, IUsuarioService usuarioService)
        {
            _repository = repository;
            _nodoService = nodoService;
            _usuarioService = usuarioService;
        }

        public async Task<PartidaDTOOut> AddPartidaAsync(PartidaDTOIn dto)
        {
            if (dto == null) throw new ValidationException("PartidaDTOIn no puede ser null");
            if (dto.ArrUsuario == null || !dto.ArrUsuario.Any()) throw new ValidationException("La partida debe contener al menos un usuario.");
            // if (dto.LstNodos != null)
            // {
            //     var duplicate = dto.LstNodos.GroupBy(n => n.IdNodo).Any(g => g.Count() > 1);
            //     if (duplicate) throw new ValidationException("Hay nodos duplicados (IdNodo debe ser único).");
            // }

            var model = await MapDtoToModel(dto);
            await _repository.PostPartidaAsync(model);
            return MapModelToDto(model);
        }

        public Task<bool> DeletePartidaAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Task.FromResult(false);
            return _repository.DeletePartidaAsync(id);
        }

        public async Task<PartidaDTOOut?> GetPartidaByIdAsync(string id)
        {
            var p = await _repository.GetPartidaByIdAsync(id);
            if (p == null) return null;
            return MapModelToDto(p);
        }

        public async Task<List<PartidaDTOOut>> GetPartidasAsync()
        {
            var lst =  _repository.GetPartidasAsync();
            return lst.Select(MapModelToDto).ToList();
        }

        public async Task<PartidaDTOOut> UpdatePartidaAsync(string id, PartidaDTOIn dto)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ValidationException("Id inválido para actualizar partida.");
            var existing = _repository.GetPartidaByIdAsync(id).GetAwaiter().GetResult();
            if (existing == null) throw new NotFoundException($"Partida con id {id} no encontrada.");

            var model = await MapDtoToModel(dto);
            
            Partida partida = await _repository.PutPartidaAsync(model);
            PartidaDTOOut partidaDTOOut = MapModelToDto(partida);
            return partidaDTOOut;
        }

        private async Task<Partida> MapDtoToModel(PartidaDTOIn dto)
        {
            var partida = new Partida
            {
                IdPartida = dto.IdPartida ?? Guid.NewGuid().ToString(),
                ArrUsuario = [await _usuarioService.GetUsuarioById(dto.ArrUsuario[0]),await _usuarioService.GetUsuarioById(dto.ArrUsuario[1])],
            };
            dto.LstNodos.ForEach(async nodo =>
            {
                NodoDTOOut nodoDtoOut = await _nodoService.GetNodoByIdAsync(nodo);
                
                partida.LstNodos.Add(new Nodo{ IdNodo= (byte) nodoDtoOut.IdNodo });  
            });

            return partida;
        }

        private PartidaDTOOut MapModelToDto(Partida partida)
        {
            if (partida == null) return new PartidaDTOOut();

            return new PartidaDTOOut
            {
                IdPartida = partida.IdPartida,
                ArrUsuario = partida.ArrUsuario?.Select(u => new UsuarioRefDTOOut { Id = u.UsuarioId, Nombre = u.Nombre, Correo = u.Correo }).ToList() ?? new List<UsuarioRefDTOOut>(),
                LstNodos = partida.LstNodos?.Select(n => new NodoDTOOut {
                    IdNodo = n.IdNodo,
                    ArrTropas = n.ArrTropas?.Select(t => new TropaDTOOut { Id = t.Id, Nombre = t.Nombre, Vida = t.Vida, Damage = t.Damage }).ToList() ?? new List<TropaDTOOut>(),
                    DuenoNodo = n.DuenoNodo == null ? null : new UsuarioRefDTOOut { Id = n.DuenoNodo.UsuarioId, Nombre = n.DuenoNodo.Nombre, Correo = n.DuenoNodo.Correo }
                }).ToList() ?? new List<NodoDTOOut>()
            };
        }
    }
}
