using Microsoft.AspNetCore.Mvc;
using server.Services.InfoService;
namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IInfoService _infoService;
        private readonly Authorization _validateService;
        private string GetUserJwt()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt is null) return " ";
            return jwt;
        }
        public InfoController(IInfoService infoService, Authorization validateService)
        {
            _infoService = infoService;
            _validateService = validateService;
        }

        [HttpPost("post")]
        public async Task<ActionResult<Info>> Post(InfoDto dto)
        {
            int userId = _validateService.ValidateUser(GetUserJwt());
            if (userId > 0)
            {
                await _infoService.CreateInfo(userId, dto);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<Info>>> GetAll()
        {
            return await _infoService.GetAllInfos();
        }

        [HttpPut("updateInfo/{id}")]
        public async Task<ActionResult<Info>> UpdateInfo(int id, InfoDto dto)
        {
            if (_validateService.ValidateUser(GetUserJwt()) > 0)
            {
                var result = await _infoService.UpdateInfo(id, dto);
                if (result is null)
                    return NotFound("Info not found! :(");
                return Ok(result);
            }
            return Unauthorized();
        }



        [HttpPut("updateInfo/role/{infoId}")]
        public async Task<ActionResult<int>> SendLaugh(int infoId)
        {
            var roleId = _validateService.ValidateUser(GetUserJwt());
            if (roleId > 0)
            {
                var result = await _infoService.Role(infoId, roleId);
                if (result == -1) return NotFound("Info not found :(");
                return Ok(result);
            }
            return Unauthorized();
        }





        [HttpGet("has-user-laughed/{infoId}")]





        public async Task<ActionResult<bool>> HasUserLaughed(int infoId)
        {
            var userId = _validateService.ValidateUser(GetUserJwt());
            var response = await _infoService.HasLaughed(infoId, userId);
            return response;
        }

        [HttpDelete("deleteInfo/{id}")]
        public async Task<ActionResult<Info>> DeleteInfo(int id)
        {
            if (_validateService.ValidateUser(GetUserJwt()) > 0)
            {
                var result = await _infoService.DeleteInfo(id);
                if (result is null)
                    return NotFound("Info not found! :(");
                return Ok();
            }
            return Unauthorized();
        }
    }
}