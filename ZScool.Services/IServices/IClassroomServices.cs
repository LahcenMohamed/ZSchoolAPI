using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSchoolAPI.Models;

namespace ZScool.Services.IServices
{
    public interface IClassroomServices : IBaseServices<Classroom>
    {
        public Task<IEnumerable<Classroom>> GetClassroomsWithTeacherId(string TeacherId);
    }
}
