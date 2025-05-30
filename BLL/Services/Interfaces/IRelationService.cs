using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IRelationService
    {
        Task<IEnumerable<RelationDTO>> GetRelations();
        Task<RelationRegisterDTO> Add(RelationRegisterDTO relationRegisterDTO);
        Task Update(RelationRegisterDTO relationRegisterDTO);
        Task Delete(int idRelation);
    }
}
