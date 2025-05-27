using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitWorkTeacher _unitWork;
        private readonly IMapper _mapper;

        public TeacherService(IUnitWorkTeacher unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task<TeacherDTO> Add(TeacherDTO TeacherDTO)
        {
            int maxRegisters = 5;
            int registrosActuales = _unitWork.GetCountRegisters();
            if (maxRegisters >= registrosActuales)
            {
                throw new TaskCanceledException("Error save Teacher, max "+maxRegisters+" egisters");
            }
            try
            {
                User teacher = new User
                {
                    Username = TeacherDTO.Username,
                    IdProfile = 1
                };
                await _unitWork.User.Add(teacher);

                await _unitWork.Save();
                if (teacher.Id == 0)
                {
                    throw new TaskCanceledException("Error save Teacher");
                }
                return _mapper.Map<TeacherDTO>(teacher);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task Update(TeacherDTO TeacherDTO)
        {
            try
            {
                var TeacherDb = await _unitWork.User.GetFirst(e => e.Id == TeacherDTO.Id);
                if (TeacherDb == null)
                {
                    throw new TaskCanceledException("Teacher not found");
                }
                TeacherDb.Username = TeacherDTO.Username;
                TeacherDb.Email = TeacherDTO.email;
                TeacherDb.IdProfile = TeacherDTO.idProfile;
                _unitWork.User.Update(TeacherDb);
                await _unitWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(int idTeacher)
        {
            try
            {
                var TeacherDb = await _unitWork.User.GetFirst(e => e.Id == idTeacher);
                if (TeacherDb == null)
                {
                    throw new TaskCanceledException("Teacher not found");
                }
                _unitWork.User.Remove(TeacherDb);
                await _unitWork.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TeacherDTO>> GetTeachers()
        {
            try
            {
                var lista = await _unitWork.User.GetAll(
                    orderBy: e => e.OrderBy(e => e.Username));
                return _mapper.Map<IEnumerable<TeacherDTO>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
