using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RelationService : IRelationService
    {
        private readonly IUnitWorkRelation _unitWork;
        private readonly IMapper _mapper;

        public RelationService(IMapper mapper, IUnitWorkRelation unitWork)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task<RelationRegisterDTO> Add(RelationRegisterDTO relationRegisterDTO)
        {
            int maxRegisters = 10;
            int registrosActuales = _unitWork.GetCountRegisters();
            if (maxRegisters == registrosActuales)
            {
                throw new TaskCanceledException("Error save ClassSubject, max 10 registers");
            }
            try
            {
                SubjectUser subjectUser = new SubjectUser
                {
                    UserId = relationRegisterDTO.UserId,
                    ClassSubjectId = relationRegisterDTO.ClassSubjectId
                };



                await _unitWork.SubjectUser.Add(subjectUser);
                await _unitWork.Save();
                if (subjectUser.Id == 0)
                {
                    throw new TaskCanceledException("Error save SubjectUser");
                }
                return _mapper.Map<RelationRegisterDTO>(subjectUser);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task Update(RelationRegisterDTO relationRegisterDTO)
        {
            try
            {
                var relationDb = await _unitWork.SubjectUser.GetFirst(e => e.Id == relationRegisterDTO.Id);
                if (relationDb == null)
                {
                    throw new TaskCanceledException("Relation not found");
                }
                relationDb.ClassSubjectId = relationRegisterDTO.ClassSubjectId;
                relationDb.UserId = relationRegisterDTO.UserId;
                _unitWork.SubjectUser.Update(relationDb);
                await _unitWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(int idRelation)
        {
            try
            {
                var TeacherDb = await _unitWork.SubjectUser.GetFirst(e => e.Id == idRelation);
                if (TeacherDb == null)
                {
                    throw new TaskCanceledException("Relation not found");
                }
                _unitWork.SubjectUser.Remove(TeacherDb);
                await _unitWork.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }


        /*public async Task<IEnumerable<ClassSubjectDTO>> GetClassSubjects()
        {
            try
            {
                var lista = await _unitWork.ClassSubject.GetAll(
                    orderBy: e => e.OrderBy(e => e.Title));
                return _mapper.Map<IEnumerable<ClassSubjectDTO>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }**/

        public async Task<IEnumerable<RelationDTO>> GetRelations()
        {
            try
            {
                var lista = await _unitWork.SubjectUser
                    .GetAll(orderBy: e => e.OrderBy(e => e.Id), includeProperties: "User,ClassSubject");
                return _mapper.Map<IEnumerable<RelationDTO>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}