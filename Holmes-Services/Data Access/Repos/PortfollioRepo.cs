using Dapper;
using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace Holmes_Services.Data_Access.Repos
{
    public static class PortfollioRepo
    {
        private static string _con = DbConnector.GetConnection();
        private static IEnumerable<CompletedJob>? _portfollio;

        public static IEnumerable<CompletedJob> GetPortfollio()
        {
            string procedure = "[sp_get_portfollio]";
            InitJobs();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _portfollio = db.Query<CompletedJob>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _portfollio == null ? Enumerable.Empty<CompletedJob>() : _portfollio;
        }

        public static CompletedJob GetPortfollioJob(int id)
        {
            string procedure = "[sp_get_portfollio_byId]";
            var parameter = new { id = id };
            CompletedJob portfollioItem = new CompletedJob();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                portfollioItem = db.QuerySingle<CompletedJob>(procedure, commandType: CommandType.StoredProcedure);
            }

            return portfollioItem == null ? new CompletedJob() : portfollioItem;
        }
        private static void InitJobs() => _portfollio = new List<CompletedJob>();

    }
}
