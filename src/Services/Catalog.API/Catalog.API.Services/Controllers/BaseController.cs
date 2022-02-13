using Catalog.API.Helpers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Services.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ValidateModelFilter))]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {

    }
}
