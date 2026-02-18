using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service.Common;

namespace BTB.Service
{
    public class TierService : ITierService
    {
        private readonly ITierRepository _repo;

        public TierService(ITierRepository repo)
        {
            _repo = repo;
        }

        public async Task<TierDTOOut> AddTierAsync(TierDTOIn dto)
        {
            if (dto == null) throw new ValidationException("TierDTOIn no puede ser null");
            var model = new Tier { Titulo = dto.Titulo ?? string.Empty, Visible = dto.Visible, FechaCreacion = System.DateTime.UtcNow };
            Tier newTier = await _repo.PostTierAsync(model);
            return new TierDTOOut { Id = newTier.Id, Titulo = newTier.Titulo, Visible = newTier.Visible, FechaCreacion = newTier.FechaCreacion };
        }

        public Task<bool> DeleteTierAsync(int id)
        {
            if (id <= 0) throw new ValidationException("Id inválido");
            return _repo.DeleteTierAsync(id);
        }

        public async Task<TierDTOOut?> GetTierByIdAsync(int id)
        {
            var t = await _repo.GetTierByIdAsync(id);
            if (t == null) return null;
            return new TierDTOOut { Id = t.Id, Titulo = t.Titulo, Visible = t.Visible, FechaCreacion = t.FechaCreacion };
        }

        public async Task<List<TierDTOOut>> GetTiersAsync()
        {
            var lst = await _repo.GetTiersAsync();
            return lst.Select(t => new TierDTOOut { Id = t.Id, Titulo = t.Titulo, Visible = t.Visible, FechaCreacion = t.FechaCreacion }).ToList();
        }

        public async Task<Tier?> UpdateTierAsync(int id, TierDTOIn dto)
        {
            if (id <= 0) throw new ValidationException("Id inválido");
            
            Tier model = new Tier { Id = id, Titulo = dto.Titulo ?? string.Empty, Visible = dto.Visible };
            return await _repo.PutTierAsync(model);
        }
    }
}
