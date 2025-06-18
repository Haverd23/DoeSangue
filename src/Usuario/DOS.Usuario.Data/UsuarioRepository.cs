using DOS.Core.Data;
using DOS.Usuario.Domain;
using DOS.Usuario.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
namespace DOS.Usuario.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioContext _context;
        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task Adcionar(User usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }
        public async Task<User> GetByCPF(CPF cpf)
        {
           return await _context.Usuarios.FirstOrDefaultAsync(u => u.CPF.Numero == cpf.Numero);
        }

        public Task<User> GetById(Guid id)
        {
            return _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
