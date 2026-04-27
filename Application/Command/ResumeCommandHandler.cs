using Application.DTO;
using Application.InterfaceRepository;
using Application.IService;
using Domain.Entity;
using MediatR;

namespace Application.Command
{
    public record ResumeCommandHandler(VacancyDTO vacansy) : IRequest<ResumeDTO?>;
    public class ResumeAppHandler : IRequestHandler<ResumeCommandHandler, ResumeDTO?>
    {
        private readonly IRepository<Resume> _Repository;
        private readonly IResumeService _Service;

        public ResumeAppHandler(IRepository<Resume> repository, IResumeService service)
        {
            _Repository = repository;
            _Service = service;
        }

        public async Task<ResumeDTO> Handle(ResumeCommandHandler request, CancellationToken cancellationToken)
        {
            var a = _Service.ResumeGeneration(request.vacansy);
            return null;
        }
    }
}
