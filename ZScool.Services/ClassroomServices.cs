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
    public class ClassroomServices(ZSchoolDbContext zSchoolDbContext) : IClassroomServices
    {
        private readonly ZSchoolDbContext context = zSchoolDbContext;
        public async Task Add(Classroom model)
        {
            context.Classrooms.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string Id)
        {
            var sub = await GetById(Id);
            context.Classrooms.Remove(sub);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Classroom>> GetAllAsEnumerable()
        {
            return context.Classrooms.AsNoTracking().AsEnumerable();
        }

        public async Task<List<Classroom>> GetAllAsListAsync()
        {
            return await context.Classrooms.AsNoTracking().ToListAsync();
        }

        public async Task<Classroom> GetById(string Id)
        {
            return await context.Classrooms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(Classroom model)
        {
            context.Classrooms.Update(model);
            await context.SaveChangesAsync();
        }
    }
}
