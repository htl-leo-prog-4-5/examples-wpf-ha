using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnterpriseSimpleV2.Repository.Abstraction.Entities;
using EnterpriseSimpleV2.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseSimpleV2.Logic.Abstraction
{
    public class MyTableRepository : IMyTableRepository
    {
        private MyContext _context;

        public MyTableRepository(MyContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<IList<MyTable>> GetAll()
        {
            return await _context.Set<MyTable>().ToListAsync();
        }

        public async Task<MyTable> Get(int id)
        {
            return await _context.Set<MyTable>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> Add(MyTable value)
        {
            _context.Entry(value).State = EntityState.Added;
            _context.SaveChanges();
            return await Task.FromResult(value.Id);
        }
    }
}