using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IUnitWorkSubjectClass _unitWork;
        private readonly IMapper _mapper;

        public ClassSubjectService(IUnitWorkSubjectClass unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task<ClassSubjectDTO> Add(ClassSubjectDTO classSubjectDTO)
        {
            int maxRegisters = 10;
            int registrosActuales = _unitWork.GetCountRegisters();
            if (maxRegisters>= registrosActuales)
            {
                throw new TaskCanceledException("Error save ClassSubject, max 10 egisters");
            }
            try
            {
                ClassSubject classSubject = new ClassSubject
                {
                    Title = classSubjectDTO.Title,
                    Credits = classSubjectDTO.Credits
                };
                await _unitWork.ClassSubject.Add(classSubject);

                await _unitWork.Save();
                if (classSubject.Id == 0)
                {
                    throw new TaskCanceledException("Error save ClassSubject");
                }
                return _mapper.Map<ClassSubjectDTO>(classSubject);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task Update(ClassSubjectDTO classSubjectDTO)
        {
            try
            {
                var classSubjectDb= await _unitWork.ClassSubject.GetFirst(e=> e.Id== classSubjectDTO.Id);
                if (classSubjectDb == null) 
                {
                    throw new TaskCanceledException("Classsubject not found");
                }
                classSubjectDb.Title= classSubjectDTO.Title;
                classSubjectDb.Credits= classSubjectDTO.Credits;
                _unitWork.ClassSubject.Update(classSubjectDb);
                await _unitWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(int idSubjectClass)
        {
            try
            {
                var classSubjectDb = await _unitWork.ClassSubject.GetFirst(e => e.Id == idSubjectClass);
                if (classSubjectDb == null)
                {
                    throw new TaskCanceledException("Classsubject not found");
                }
                _unitWork.ClassSubject.Remove(classSubjectDb);
                await _unitWork.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ClassSubjectDTO>> GetClassSubjects()
        {
            try
            {
                var lista=await _unitWork.ClassSubject.GetAll(
                    orderBy: e=>e.OrderBy(e=> e.Title));
                return _mapper.Map<IEnumerable<ClassSubjectDTO>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
