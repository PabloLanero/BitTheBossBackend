using BTB.Entities.Models;
using BTB.Repository.Interfaces;

namespace BTB.Repository
{
    public class NodoRepository : INodoRepository
    {
        private readonly List<Nodo> _lst = new List<Nodo>();
        private int _id = 1;

        public NodoRepository() {}

        public Task<bool> DeleteNodoAsync(int id)
        {
            var existing = _lst.FirstOrDefault(n => n.IdNodo == id);
            if (existing == null) return Task.FromResult(false);
            _lst.Remove(existing);
            return Task.FromResult(true);
        }

        public Task<Nodo?> GetNodoByIdAsync(int id)
        {
            var n = _lst.FirstOrDefault(x => x.IdNodo == id);
            return Task.FromResult(n);
        }

        public Task<List<Nodo>> GetNodosAsync()
        {
            return Task.FromResult(_lst.ToList());
        }

        public Task<bool> PostNodoAsync(Nodo nodo)
        {
            nodo.IdNodo = (byte)_id++;
            _lst.Add(nodo);
            return Task.FromResult(true);
        }

        public Task<bool> PutNodoAsync(Nodo nodo)
        {
            var existing = _lst.FirstOrDefault(n => n.IdNodo == nodo.IdNodo);
            if (existing == null) return Task.FromResult(false);
            existing.ArrTropas = nodo.ArrTropas;
            existing.DuenoNodo = nodo.DuenoNodo;
            return Task.FromResult(true);
        }
    }
}
