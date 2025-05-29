using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    public class UserAppController: BaseApiController
    {
        private readonly IUserAppService _userAppService;
        private ApiResponse _response;

        public UserAppController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
            _response = new();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _userAppService.GetUsers();
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
        public async Task<IActionResult> Update(UserAppDTO userAppDTO)
        {
            try
            {
                await _userAppService.Update(userAppDTO);
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
                await _userAppService.Delete(id);
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
