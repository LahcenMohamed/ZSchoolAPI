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
    public class TeacherServices(ZSchoolDbContext zSchoolDbContext) : ITeacherServices
    {
        private readonly ZSchoolDbContext context = zSchoolDbContext;
        public async Task Add(Teacher model)
        {
            context.Teachers.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string Id)
        {
            var sub = await GetById(Id);
            context.Teachers.Remove(sub);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllAsEnumerable()
        {
            return context.Teachers.AsNoTracking().AsEnumerable();
        }

        public async Task<List<Teacher>> GetAllAsListAsync()
        {
            return await context.Teachers.AsNoTracking().ToListAsync();
        }

        public async Task<Teacher> GetById(string Id)
        {
            return await context.Teachers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(Teacher model)
        {
            context.Teachers.Update(model);
            await context.SaveChangesAsync();
        }
    }
}
