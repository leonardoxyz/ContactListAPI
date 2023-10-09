using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using volxyseat.Domain.Core.Data;
using volxyseat.Domain.Models;
using volxyseat.Infrastructure.Data;

namespace Volxyseat.Infrastructure.Repository
{
    public class ContactRepository : IRepository<Contact, Guid>
    {
        private readonly DataContext _context;

        public ContactRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact entity)
        {
            await _context.Contacts.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Contacts.FindAsync(id);
            if (client != null)
            {
                _context.Contacts.Remove(client);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task UpdateAsync(Contact entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
