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
    public class SubjectServices(ZSchoolDbContext zSchoolDbContext) : ISubjectServices
    {
        private readonly ZSchoolDbContext context = zSchoolDbContext;
        public async Task Add(Subject model)
        {
            context.Subjects.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string Id)
        {
            var sub = await GetById(Id);
            context.Subjects.Remove(sub);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllAsEnumerable()
        {
            return context.Subjects.AsNoTracking().AsEnumerable();
        }

        public async Task<List<Subject>> GetAllAsListAsync()
        {
            return await context.Subjects.AsNoTracking().ToListAsync();
        }

        public async Task<Subject> GetById(string Id)
        {
            return await context.Subjects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(Subject model)
        {
            context.Subjects.Update(model);
            await context.SaveChangesAsync();
        }
    }
}
