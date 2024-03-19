using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSchoolAPI.Models;
using ZScool.Services.IServices;

namespace ZScool.Services
{
    public class SeanceServices(ZSchoolDbContext zSchoolDbContext) : ISeanceServices
    {
        private readonly ZSchoolDbContext context = zSchoolDbContext;
        public async Task Add(Seance model)
        {
            context.Seances.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string Id)
        {
            var sub = await GetById(Id);
            context.Seances.Remove(sub);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Seance>> GetAllAsEnumerable()
        {
            return context.Seances.AsNoTracking().AsEnumerable();
        }

        public async Task<List<Seance>> GetAllAsListAsync()
        {
            return await context.Seances.AsNoTracking().ToListAsync();
        }

        public async Task<Seance> GetById(string Id)
        {
            return await context.Seances.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(Seance model)
        {
            context.Seances.Update(model);
            await context.SaveChangesAsync();
        }
    }
}
