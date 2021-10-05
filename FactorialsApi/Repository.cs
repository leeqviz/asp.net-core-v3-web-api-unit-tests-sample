using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialsApi
{
    public interface IRepository
    {
        public Task Create(Factorial entity);
        public Task<IEnumerable<Factorial>> GetAll();
        public Task<Factorial> GetByValue(int? value);
        public Task<Factorial> GetByResult(long? result);
        public Task Save();
    }

    public class Repository : IRepository
    {
        private readonly FactorialsContext _context;

        public Repository(FactorialsContext context)
        {
            _context = context;
        }

        public async Task Create(Factorial entity) =>
            await _context.AddAsync(entity);

        public async Task<IEnumerable<Factorial>> GetAll() =>
            await _context.Factorials.ToListAsync();

        public async Task<Factorial> GetByValue(int? value) =>
            await _context.Factorials.FirstOrDefaultAsync(f => f.Value == value);

        public async Task<Factorial> GetByResult(long? result) =>
            await _context.Factorials.FirstOrDefaultAsync(f => f.Result == result);

        public async Task Save() => 
            await _context.SaveChangesAsync();
    }
}
