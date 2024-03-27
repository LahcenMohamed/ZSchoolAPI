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
    public class StudentServices(ZSchoolDbContext zSchoolDbContext) : IStudentServices
    {
        private readonly ZSchoolDbContext context = zSchoolDbContext;
        public async Task Add(Student model)
        {
            context.Students.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string Id)
        {
            var sub = await GetById(Id);
            context.Students.Remove(sub);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsEnumerable()
        {
            return context.Students.AsNoTracking().AsEnumerable();
        }

        public async Task<List<Student>> GetAllAsListAsync()
        {
            return await context.Students.AsNoTracking().ToListAsync();
        }

        public  async Task<IEnumerable<Student>> GetAllWithTeacherId(string Id)
        {
            var lst = context.Seances.AsNoTracking()
                                     .Where(x => x.TeacherId == Id)
                                     .Include(x => x.Classroom)
                                     .Select(x => x.Classroom.Id)
                                     .Distinct();
            List<Student> students = [];
            foreach (var item in lst) 
            {
                var stus = await context.Students.AsNoTracking().Where(x => x.ClassroomId == item).ToListAsync();
                students.AddRange(stus);
            }
            return students;
        }

        public async Task<Student> GetById(string Id)
        {
            return await context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(Student model)
        {
            context.Students.Update(model);
            await context.SaveChangesAsync();
        }
    }
}
