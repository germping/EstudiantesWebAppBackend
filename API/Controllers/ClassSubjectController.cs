using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    public class ClassSubjectController: BaseApiController
    {
        private readonly IClassSubjectService _classSubjectService;
        private ApiResponse _response;

        public ClassSubjectController(IClassSubjectService classSubjectService)
        {
            _classSubjectService = classSubjectService;
            _response = new();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _classSubjectService.GetClassSubjects();
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
        public async Task<IActionResult> Create(ClassSubjectDTO modelDto)
        {
            try
            {
                await _classSubjectService.Add(modelDto);
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
        public async Task<IActionResult> Update(ClassSubjectDTO modelDto)
        {
            try
            {
                await _classSubjectService.Update(modelDto);
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
                await _classSubjectService.Delete(id);
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
