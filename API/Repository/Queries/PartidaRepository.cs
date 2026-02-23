using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BTB.Repository
{
    public class PartidaRepository : IPartidaRepository
    {
        private readonly BTBContext _context;

        public PartidaRepository(BTBContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePartidaAsync(string id)
        {
            _context.Partidas.Remove(new Partida{IdPartida=id});
            await _context.SaveChangesAsync();
            // Si todo va bien
            return true;
        }

        public async Task<Partida?> GetPartidaByIdAsync(string id)
        {
            Partida partida = await _context.Partidas.FindAsync(new Partida{IdPartida= id});
            return partida;
        }

        public List<Partida> GetPartidasAsync()
        {
            IEnumerable<Partida> partidas =  _context.Partidas.AsQueryable<Partida>()
            .Include(p => p.ArrUsuario).Include(p => p.LstNodos).Include(p => p.movimientos);
            return partidas.ToList();
        }

        public async Task<Partida> PostPartidaAsync(Partida partida)
        {
            Partida newPartida =  _context.Partidas.Add(partida).Entity;
            await _context.SaveChangesAsync();
            return newPartida;
        }

        public async Task<Partida> PutPartidaAsync(Partida partida)
        {
            Partida newPartida = _context.Partidas.Update(partida).Entity;
            await _context.SaveChangesAsync();
            
            return newPartida;
        }
    }
}