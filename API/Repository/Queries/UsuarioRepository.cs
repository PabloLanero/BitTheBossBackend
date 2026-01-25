namespace BTB.Repository
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(IConfiguration p_configuration)
        {
            _connectionString = p_configuration.GetConnectionString("MiSuperConectionString");
        }
    }
}