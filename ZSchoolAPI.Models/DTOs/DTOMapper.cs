using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSchoolAPI.Models;

namespace ZSchoolAPI.Models.DTOs
{
    public sealed class DTOMapper
    {
        public static Subject SubjectDtoMapper(SubjectDTO subjectDTO)
        {
            return new Subject
            {
                Id = subjectDTO.Id == null ? Guid.NewGuid().ToString() : subjectDTO.Id,
                Name = subjectDTO.Name
            };
        }
        public static Classroom ClassroomDtoMapper(ClassroomDTO classroomDTO) 
        {
            return new Classroom
            {
                Id = classroomDTO.Id == null ? Guid.NewGuid().ToString() : classroomDTO.Id,
                ClassroomNumber = classroomDTO.ClassroomNumber,
                CountOfChairs = classroomDTO.CountOfChairs
            };
        }
    }
}
