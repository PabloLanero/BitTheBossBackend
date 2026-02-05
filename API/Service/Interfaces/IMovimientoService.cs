using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface IMovimientoService
    {
        public Task<List<MovimientoDTOOut>> GetMovimientosAsync();
        public Task<MovimientoDTOOut?> GetMovimientoByIdAsync(int id);
        public Task<MovimientoDTOOut> AddMovimientoAsync(MovimientoDTOIn dto);
        public Task<bool> DeleteMovimientoAsync(int id);
    }
}
