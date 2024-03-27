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
        public static Teacher TeacherDtoMapper(TeacherDTO teacherDTO) 
        {
            return new Teacher
            {
                Id = teacherDTO.Id == null ? Guid.NewGuid().ToString() : teacherDTO.Id,
                FirstName = teacherDTO.FirstName,
                LastName = teacherDTO.LastName,
                Email = teacherDTO.Email,
                PhoneNumber = teacherDTO.PhoneNumber,
                Address = teacherDTO.Address,
                Salary = teacherDTO.Salary,
                ImageUrl = teacherDTO.ImageUrl,
                SubjectId = teacherDTO.SubjectId
            };
        }
        public static Student StudentDtoMapper(StudentDTO studentDTO) 
        {
            return new Student
            {
                Id = studentDTO.Id == null ? Guid.NewGuid().ToString() : studentDTO.Id,
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Email = studentDTO.Email,
                PhoneNumber = studentDTO.PhoneNumber,
                Address = studentDTO.Address,
                ImageUrl = studentDTO.ImageUrl,
                ClassroomId = studentDTO.ClassroomId
            };
        }
        public static Seance SeanceDtoMapper(SeanceDTO seanceDTO) 
        {
            return new Seance
            {
                Id = seanceDTO.Id == null ? Guid.NewGuid().ToString() : seanceDTO.Id,
                StartTime = seanceDTO.StartTime,
                EndTime = seanceDTO.EndTime,
                SeanceDate = seanceDTO.SeanceDate,
                TeacherId = seanceDTO.TeacherId,
                ClassroomId = seanceDTO.ClassroomId
            };
        }
    }
}
