using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationController : BaseApiController
    {
        private readonly ApplicationDbContext _db;

        public RelationController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectUser>>> GetRelations()
        {
            return await _db.Relations.ToListAsync();   
        }
        [HttpPost("register")]
        public async Task<ActionResult<SubjectUser>> SaveRelation(RelationRegisterDTO relationRegisterDTO)
        {
            if (await RelationExists(relationRegisterDTO.UserId, relationRegisterDTO.ClassSubjectId)) return BadRequest("Relation already exists");
            if (!await CouldRelations(relationRegisterDTO.UserId, relationRegisterDTO.ClassSubjectId)) return BadRequest("Relation bad");


            var relation = new SubjectUser
            {
                UserId = relationRegisterDTO.UserId,
                ClassSubjectId = relationRegisterDTO.ClassSubjectId
            };
          
            _db.Relations.Add(relation);
            _db.SaveChangesAsync();
            return Ok(relation);

        }

        private async Task<bool> RelationExists(int idUser, int idSubject )
        {
            return await _db.Relations.AnyAsync(x => x.UserId == idUser && x.ClassSubjectId == idSubject) ;
        }
        private async Task<bool> CouldRelations(int idUser, int idSubject)
        {
            int idProfile= _db.Users.SingleOrDefault(x => x.Id == idUser).Id;
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
                    
                    if (idTeacher!=-1)
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
