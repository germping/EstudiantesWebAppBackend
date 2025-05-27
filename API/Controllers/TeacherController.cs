using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    public class TeacherController: BaseApiController
    {
        private readonly ITeacherService _teacherService;
        private ApiResponse _response;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
            _response = new();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _teacherService.GetTeachers();
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
        [HttpPost]
        public async Task<IActionResult> Create(TeacherDTO TeacherDTO)
        {
            try
            {
                await _teacherService.Add(TeacherDTO);
                _response.IsSuccesful = true;
                _response.StatusCode=System.Net.HttpStatusCode.OK;
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
        public async Task<IActionResult> Update(TeacherDTO TeacherDTO)
        {
            try
            {
                await _teacherService.Update(TeacherDTO);
                _response.IsSuccesful=true;
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
                await _teacherService.Delete(id);
                _response.IsSuccesful=true;
                _response.StatusCode= System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccesful = false;
                _response.Message = ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

    }
}
