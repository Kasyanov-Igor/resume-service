using System.Data;
using Dapper;
using Domain.Entity;

namespace Infrastructure.Repositories
{
    public class ResumeRepository : BaseRepository<Resume>
    {
        //
        //
        //
        public ResumeRepository(AppDbContext db, IDbConnection connection)
            : base(db, connection)
        {
        }

        //получаем все
        public override async Task<IEnumerable<Resume>> GetAllAsync(CancellationToken ct)
        {
            const string sql = "SELECT \"Id\", \"VacancyId\", \"Title\", \"Bio\", \"Description\", \"ProgressWork\" FROM \"Resumes\"";

            CommandDefinition query = new CommandDefinition(sql, cancellationToken: ct); //Token позволяет остановить выполнение запроса, если он больше не нужен

            return await _connection.QueryAsync<Resume>(query);
        }

        //получаем по id
        public override async Task<Resume?> GetByIdAsync(int id, CancellationToken ct)
        {
            const string sql = "SELECT \"Id\", \"VacancyId\", \"Bio\", \"Description\", \"ProgressWork\" FROM \"Resumes\" WHERE Id = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Resume>(new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
        }
    }
}
