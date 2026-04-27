using Application.DTO;

namespace Application.IService
{
    public interface IResumeService
    {
        Task<ResumeDTO> ResumeGeneration(VacancyDTO vacancy);
    }
}
