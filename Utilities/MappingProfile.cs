using AutoMapper;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClassSubject, ClassSubjectDTO>();
            CreateMap<SubjectUser, RelationDTO>();
            CreateMap<SubjectUser, RelationRegisterDTO>();
            CreateMap<User, UserAppDTO>();

        }
    }
}
