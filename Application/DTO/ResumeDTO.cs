using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTO
{
    public class ResumeDTO
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public required string Title { get; set; }
        public required string Bio { get; set; }
        public required string Description { get; set; }
        public required string ProgressWork { get; set; }
    }
}
