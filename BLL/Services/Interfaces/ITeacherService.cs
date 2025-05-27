using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDTO>> GetTeachers();
        Task<TeacherDTO> Add(TeacherDTO teacherDTO);
        Task Update(TeacherDTO teacherDTO);
        Task Delete(int idTeacher);

    }
}
