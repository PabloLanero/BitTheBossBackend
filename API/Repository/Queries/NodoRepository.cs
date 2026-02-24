using BTB.Data;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BTB.Repository
{
    public class NodoRepository : INodoRepository
    {
        private readonly BTBContext _context;
        public NodoRepository(BTBContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteNodoAsync(int id)
        {
            _context.Nodos.Remove(new Nodo{IdNodo=(byte)id});
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Nodo?> GetNodoByIdAsync(int id)
        {
            Nodo nodo = await _context.Nodos.FindAsync(new Nodo{IdNodo = (byte)id});
            return nodo;
        }

        public List<Nodo> GetNodosAsync()
        {
            IEnumerable<Nodo> nodos = _context.Nodos.AsQueryable()
            .Include(p => p.DuenoNodo).Include(p => p.ArrTropas);

            return nodos.ToList();
        }

        public async Task<Nodo> PostNodoAsync(NodoDTOIn dto)
        {
            Nodo newNodo = _context.Nodos.Add(new Nodo{ArrTropas = [
                new Tropa {Id= dto.ArrTropas[0]},
                new Tropa {Id= dto.ArrTropas[1]}
            ],
            DuenoNodo = new Usuario{UsuarioId = dto.idUsuario}
            }).Entity;
            await _context.SaveChangesAsync();
            return newNodo;
        }

        public async Task<Nodo> PutNodoAsync(NodoDTOIn dto)
        {
            Nodo newNodo = _context.Nodos.Update(new Nodo{ArrTropas = [
                new Tropa {Id= dto.ArrTropas[0]},
                new Tropa {Id= dto.ArrTropas[1]}
            ],
            DuenoNodo = new Usuario{UsuarioId = dto.idUsuario}
            }).Entity;
            await _context.SaveChangesAsync();
            return newNodo;
        }
    }
}
