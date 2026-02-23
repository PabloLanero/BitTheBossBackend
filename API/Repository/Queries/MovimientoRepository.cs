using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BTB.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly BTBContext _context;

        public MovimientoRepository(BTBContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMovimientoAsync(int id)
        {
            _context.Movimientos.Remove(new Movimiento{Id = id});
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Movimiento?> GetMovimientoByIdAsync(int id)
        {
            Movimiento movimiento = await _context.Movimientos.FindAsync(new Movimiento { Id = id });
            return movimiento;
        }

        public List<Movimiento> GetMovimientosAsync()
        {
            IEnumerable<Movimiento> movimientos = _context.Movimientos.AsQueryable<Movimiento>()
            .Include(p => p.Partida).Include(p => p.NodoDestino).Include(p => p.Tropa);
            return movimientos.ToList();
        }

        public async Task<Movimiento> PostMovimientoAsync(Movimiento movimiento)
        {
            Movimiento newMovimiento = _context.Movimientos.Add(movimiento).Entity;
            await _context.SaveChangesAsync();
            return newMovimiento;
        }

        public async Task<Movimiento> PutMovimientoAsync(Movimiento movimiento)
        {
            Movimiento newMovimiento = _context.Movimientos.Update(movimiento).Entity;
            await _context.SaveChangesAsync();
            return newMovimiento;
        }
    }
}
