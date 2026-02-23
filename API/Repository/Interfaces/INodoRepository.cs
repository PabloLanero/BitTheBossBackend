using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface INodoRepository
    {
        public List<Nodo> GetNodosAsync();
        public Task<Nodo?> GetNodoByIdAsync(int id);
        public Task<Nodo> PostNodoAsync(Nodo nodo);
        public Task<Nodo> PutNodoAsync(Nodo nodo);
        public Task<bool> DeleteNodoAsync(int id);
    }
}
