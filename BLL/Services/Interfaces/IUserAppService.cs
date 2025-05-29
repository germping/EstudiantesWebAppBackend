using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserAppDTO>> GetUsers();
        Task<UserAppDTO> Add(UserAppDTO userAppDTO);
        Task Update(UserAppDTO userAppDTO);
        Task Delete(int idTeacher);

    }
}
