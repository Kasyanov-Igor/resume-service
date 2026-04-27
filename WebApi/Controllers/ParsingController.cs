using Application.Command;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class ParsingController : ControllerBase
    {
        private readonly IMediator _mediator;

        //
        //
        //
        public ParsingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/parsing")]
        public async Task<IActionResult> ParsePage(string url)
        {
            if (string.IsNullOrEmpty(url) && url == null)
            { return BadRequest("URL обязателен"); }

            var result = await _mediator.Send(new ParsingCommandHandler(url));
            return Ok(result);
        } 
    }
}
