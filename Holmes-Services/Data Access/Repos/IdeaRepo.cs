using Dapper;
using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace Holmes_Services.Data_Access.Repos
{
    public static class IdeaRepo
    {
        private static string _con = DbConnector.GetConnection();
        private static IEnumerable<Idea>? _ideas;


        public static IEnumerable<Idea> GetAllIdeas()
        {
            string procedure = "[sp_get_ideas]";
            InitIdeas();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _ideas = db.Query<Idea>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _ideas == null ? Enumerable.Empty<Idea>() : _ideas;
        }

        public static Idea GetIdeaById(int id)
        {
            string procedure = "[sp_get_idea_byId]";
            var parameter = new { id = id };
            Idea idea = new Idea();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                idea = db.QuerySingle<Idea>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }

            return idea == null ? new Idea() : idea;
        }
        private static void InitIdeas() => _ideas = new List<Idea>();

    }
}
