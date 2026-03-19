using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductCQRS.CQRS.Command;
using ProductCQRS.CQRS.Handler;
using ProductCQRS.Profiles;
using ProductCQRS.Services;

namespace ProductCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly AppSettingProfile _appSettings;
        private readonly PaginationProfile _pagination;
        private readonly AdminProfile _adminProfile;
        private readonly ILogger _logger;

        public ProductController(IMediator mediator,IOptions<AppSettingProfile> options,IOptions<AdminProfile> roleOptions, IOptions<PaginationProfile> paginationOptions, ILogger logger)
        {
            _mediator = mediator;
            _appSettings = options.Value;
            _pagination = paginationOptions.Value;
            _adminProfile = roleOptions.Value;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult GetConfig()
        {
            return Ok(new
            {
                AppName = _appSettings.ApplicationName,
                MaxProducts = _appSettings.MaxProductsPerPage,
                PageNumber = _pagination.PageNumber,
                PageSize = _pagination.PageSize
            });
        }
        [HttpPost("check-admin")]
        public IActionResult CheckAdmin([FromBody] AdminProfile request)
        {
            bool hasAccess =
                _adminProfile.Username == request.Username &&
                _adminProfile.Password == request.Password &&
                _adminProfile.Role == request.Role;

            if (hasAccess)
            {
                return Ok(new { IsAdmin = true, Role = request.Role, Message = "Access granted" });
            }
            return Ok(new { IsAdmin = false, Message = "Access denied" });
        }

        //Зробити конфігурацію для адмін прав 
        //1. username
        //2. password
        //3. role
        //зробити ендпоінт для перевірки прав
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("get all products...");
            var result = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(result);
        }
        [HttpPost("create-product")]
        public async Task<IActionResult> Create([FromBody] GetAllProductsQueryRequest request)
        {
            var result = await _mediator.Send(request);
            _logger.LogInformation("Creating product...");
            if (!result.IsSuccess)
            {
                _logger.LogError($"Product {request.Name} already exist");
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
