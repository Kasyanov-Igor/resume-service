using Application.DTO;
using Application.InterfaceRepository;
using Application.IService;
using Application.Query;
using Domain.Entity;
using MediatR;

namespace Application
{
    internal static class VacancyMappingExtensions
    {
        internal static Vacancy MapToVacancy(this VacancyDTO vacancy)
        {
            return new Vacancy
            {
                Title = vacancy.Title,
                Description = vacancy.Description
            };
        }
    }
}

namespace Application.Command
{
    public record ParsingCommandHandler(string url) : IRequest<VacancyDTO?>;

    public class ParsingAppHandler : IRequestHandler<ParsingCommandHandler, VacancyDTO?>
    {
        private readonly IRepository<Vacancy> _Repository;
        private readonly IParsingService _Service;
        private readonly IMediator _mediator;

        public ParsingAppHandler(IRepository<Vacancy> repository, IParsingService service, IMediator mediator)
        {
            _Repository = repository;
            _Service = service;
            _mediator = mediator;
        }

        public async Task<VacancyDTO?> Handle(ParsingCommandHandler request, CancellationToken cancellationToken)
        {
            VacancyDTO? vacancy = await _Service.ParsingUrl(request.url);

            if (vacancy != null)
            {
                await _Repository.AddAsync(vacancy.MapToVacancy(), cancellationToken);

                ResumeDTO? result = await _mediator.Send(new ResumeCommandHandler(vacancy));
            }

            return vacancy;
        }
    }
}
