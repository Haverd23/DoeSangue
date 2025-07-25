﻿using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DOS.Auth.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task AdicionarUser(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task<User> ObterPorId(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> ObterPorEmail(Email email)
        {
           return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

      
    }
}
