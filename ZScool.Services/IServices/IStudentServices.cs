﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSchoolAPI.Models;

namespace ZScool.Services.IServices
{
    public interface IStudentServices : IBaseServices<Student>
    {
        public Task<IEnumerable<Student>> GetAllWithTeacherId(string Id);
    }
}
