using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController(IDataMinerConnection dataMinerConnection) : ControllerBase
    {
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<string?>> GetAllCategories()
        {
            var result = await dataMinerConnection.GetAllCategories();


            return result;
        }

        [HttpGet("GetDeviceByCategoryName")]
        public async Task<ActionResult<string?>> GetDeviceByCategoryName([FromQuery] string categoryName)
        {
            var result = await dataMinerConnection.GetDeviceByCategoryName(categoryName);

            return result;
        }

    }
}
