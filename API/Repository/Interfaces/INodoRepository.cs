using BTB.Entities.DTO;
using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface INodoRepository
    {
        public List<Nodo> GetNodosAsync();
        public Task<Nodo?> GetNodoByIdAsync(int id);
        public Task<Nodo> PostNodoAsync(NodoDTOIn nodo);
        public Task<Nodo> PutNodoAsync(NodoDTOIn nodo);
        public Task<bool> DeleteNodoAsync(int id);
    }
}
