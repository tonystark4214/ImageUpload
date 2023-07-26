using Microsoft.AspNetCore.Mvc;
using UserDataApplication.Models;
using UserDataApplication.Repository;

namespace UserDataApplication.Controllers
{
    public class UserDataController : Controller
    {
        public InterfaceUserData IuserData;
        public UserDataController(InterfaceUserData IuserData)
        {
            this.IuserData = IuserData;
        }

        [HttpGet]
        [Route("GetUserData")]
        public IActionResult GetUserData()
        {
            return Ok(IuserData.GetUserData());
        }
        [HttpPost]
        [Route("PostUserData")]
        public IActionResult PostUserData([FromBody] PostUserDataModel postUserDataModel)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel response = new ResponseModel();
                var message = string.Join(" | ", ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage));
                response.Message = message;

                return Ok(response);

            }
            return Ok(IuserData.PostUserData(postUserDataModel));
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            return Ok(IuserData.Delete(id));
        }

        [HttpGet]
        [Route("GetUserDataById")]
        public IActionResult GetUserDataById(int id)
        {
            return Ok(IuserData.GetUserDataById(id));
        }

        [HttpGet]
        [Route("GetCountryData")]
        public IActionResult GetCountryData()
        {
            return Ok(IuserData.GetCountryData());
        }

        [HttpGet]
        [Route("GetStateData")]
        public IActionResult GetStateData(int id)
        {
            return Ok(IuserData.GetStateData(id));
        }

        [HttpGet]
        [Route("GetCityData")]
        public IActionResult GetCityData(int id)
        {
            return Ok(IuserData.GetCityData(id));
        }
    }
}
