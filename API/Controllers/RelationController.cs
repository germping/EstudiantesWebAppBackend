using BLL.Services;
using BLL.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace API.Controllers
{
    public class RelationController : BaseApiController
    {
        private readonly IRelationService _relationStudentService;
        private ApiResponse _response;

        private readonly ApplicationDbContext _db;


        public RelationController(IRelationService relationStudentService, ApplicationDbContext db)
        {
            _db = db;
            _relationStudentService = relationStudentService;
            _response = new();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _relationStudentService.GetRelations();
                _response.IsSuccesful = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {

                _response.IsSuccesful = false;
                _response.Message = ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(RelationRegisterDTO modelDto)
        {
            try
            {
                await _relationStudentService.Update(modelDto);
                _response.IsSuccesful = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccesful = false;
                _response.Message = ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _relationStudentService.Delete(id);
                _response.IsSuccesful = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccesful = false;
                _response.Message = ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RelationRegisterDTO modelDto)
        {
            _response.IsSuccesful = false;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            

            if (await RelationExists(modelDto.UserId, modelDto.ClassSubjectId))
            {
                _response.Message = "Relación ya existe";
                return Ok(_response);
            }
            if (!await CouldRelations(modelDto.UserId, modelDto.ClassSubjectId))
            {
                _response.Message = "El usuario no puede tener más registros";
                return Ok(_response);
            }
            try
            {
                await _relationStudentService.Add(modelDto);
                _response.IsSuccesful = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsSuccesful = false;
                _response.Message = ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        private async Task<bool> RelationExists(int idUser, int idSubject)
        {
            return await _db.Relations.AnyAsync(x => x.UserId == idUser && x.ClassSubjectId == idSubject);
        }
        private async Task<bool> CouldRelations(int idUser, int idSubject)
        {
            int idProfile = _db.Users.SingleOrDefault(x => x.Id == idUser).IdProfile;
            //Teacher
            if (idProfile == 1)
            {
                // Más de 2 materias
                int countRelations = _db.Relations.Where(x => x.UserId == idUser).Count();
                if (countRelations < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //Student 
            if (idProfile == 2)
            {
                //Más de 3 materias
                int countRelations = _db.Relations.Where(x => x.UserId == idUser).Count();
                if (countRelations < 3)
                {
                    return true;
                }
                //Mismo profesor
                else
                {
                    // Obtener el id del profesor de la materia
                    int idTeacher = -1;
                    try
                    {
                        idTeacher = _db.Relations
                        .Where(x => x.ClassSubjectId == idSubject)
                        .Select(x => x.UserId)
                        .FirstOrDefault();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    if (idTeacher != -1)
                    {
                        //Validar si existe un registro con el profesor
                        return await _db.Relations.AnyAsync(x => x.UserId == idTeacher && x.ClassSubjectId == idSubject);
                    }
                }
            }
            return false;

        }
    }
}
