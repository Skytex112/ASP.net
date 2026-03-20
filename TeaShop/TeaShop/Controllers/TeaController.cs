using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeaShop.CQRS.Command;
using TeaShop.CQRS.Queries;

namespace TeaShop.Controllers
{
    [ApiController]
    [Route("api/tea")]
    public class TeaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _mediator.Send(new GetTeasQuery { Page = page, PageSize = pageSize });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTeaByIdQuery { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTeaCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, UpdateTeaCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteTeaCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok(new { message = "Tea deleted successfully" });
        }
    }
}
