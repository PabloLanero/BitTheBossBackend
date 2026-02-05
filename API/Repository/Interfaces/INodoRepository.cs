using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface INodoRepository
    {
        public Task<List<Nodo>> GetNodosAsync();
        public Task<Nodo?> GetNodoByIdAsync(int id);
        public Task<bool> PostNodoAsync(Nodo nodo);
        public Task<bool> PutNodoAsync(Nodo nodo);
        public Task<bool> DeleteNodoAsync(int id);
    }
}
