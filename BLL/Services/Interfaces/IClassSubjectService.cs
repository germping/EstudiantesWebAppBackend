using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IClassSubjectService
    {
        Task<IEnumerable<ClassSubjectDTO>> GetClassSubjects();
        Task<ClassSubjectDTO> Add(ClassSubjectDTO classSubjectDTO);
        Task Update(ClassSubjectDTO classSubjectDTO);
        Task Delete(int idSubjectClass);

    }
}
