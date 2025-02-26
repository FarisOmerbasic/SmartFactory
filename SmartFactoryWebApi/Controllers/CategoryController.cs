using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Models;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IDataMinerConnection dataMinerConnection) : ControllerBase
    {
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<List<Category>>?> GetAllCategories(CancellationToken cancellationToken)
        {
            var result = await dataMinerConnection.GetAllCategories(cancellationToken);

            if(result == null) return BadRequest();
            if(result.Count <= 0) return BadRequest();

            return result;
        }
    }
}
