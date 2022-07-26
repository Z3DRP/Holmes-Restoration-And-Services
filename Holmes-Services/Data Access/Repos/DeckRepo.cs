using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Holmes_Services.Data_Access.Repos
{
    public class DeckRepo : IDeckRepo
    {
        private string _con = DbConnector.GetConnection();
        IEnumerable<Decking> _decking;

        public IEnumerable<Decking> GetAllDecks()
        {
            string procedure = "[sp_GetDecking]";
            _decking = new List<Decking>();
            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? new List<Decking>() : _decking;
        }
        public IEnumerable<Decking> GetDecksByPrice(double price)
        {
            string procedure = "[sp_get_decks_by_price]";
            var parameter = new { price = price };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? new List<Decking>() : _decking;
        }
        public IEnumerable<Decking> GetDeckById(int id)
        {
            string procedure = "[sp_GetDeckById]";
            var parameter = new { id = id };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }
            
            return _decking == null ? new List<Decking>() : _decking;
        }
        public IEnumerable<Decking> GetDeckByType(string type)
        {
            string procedure = "[sp_GetDeckByType]";
            var parameter = new { type = type };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? new List<Decking>() : _decking;
        }
        public IEnumerable<Decking> GetDeckByGroup(int gId)
        {
            string procedure = "[sp_get_decks_by_groupId]";
            var parameter = new { groupId = gId };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? new List<Decking>() : _decking;
        }

    }
}
