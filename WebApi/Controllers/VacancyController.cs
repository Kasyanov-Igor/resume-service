using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IMediator _mediator;

        //
        //
        public VacancyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/get-vacancy")]
        public async Task<IActionResult> GetVacancies()
        {
            var result = await _mediator.Send(new GetVacanciesQuery());
            return Ok(result);
        }
    }
}
