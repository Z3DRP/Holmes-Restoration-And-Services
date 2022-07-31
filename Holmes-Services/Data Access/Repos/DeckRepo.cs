using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Holmes_Services.Data_Access.Repos
{
    public static class DeckRepo
    {
        private static string _con = DbConnector.GetConnection();
        private static IEnumerable<Decking>? _decking;

        public static IEnumerable<Decking> GetAllDecks()
        {
            string procedure = "[sp_GetDecking]";
            _decking = new List<Decking>();
            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? Enumerable.Empty<Decking>() : _decking;
        }
        public static IEnumerable<Decking> GetDecksByPrice(double price)
        {
            string procedure = "[sp_get_decks_by_price]";
            var parameter = new { price = price };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? Enumerable.Empty<Decking>() : _decking;
        }
        public static Decking GetDeckById(int id)
        {
            string procedure = "[sp_GetDeckById]";
            var parameter = new { id = id };
            Decking deck = new Decking();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                deck = db.QuerySingle<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }
            
            return deck == null ? new Decking() : deck;
        }
        public static IEnumerable<Decking> GetDeckByType(string type)
        {
            string procedure = "[sp_GetDeckByType]";
            var parameter = new { type = type };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? Enumerable.Empty<Decking>() : _decking;
        }
        public static IEnumerable<Decking> GetDeckByGroup(int gId)
        {
            string procedure = "[sp_get_decks_by_groupId]";
            var parameter = new { groupId = gId };
            _decking = new List<Decking>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _decking = db.Query<Decking>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _decking == null ? Enumerable.Empty<Decking>() : _decking;
        }

        public static bool VerifyDecking(string pCode, string productName)
        {
            string procedure = "[sp_verify_deck]";
            var parameters = new { productCode = pCode, productName };
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }

        public static bool VerifyDeckingById(int id)
        {
            string procedure = "[sp_verify_deck_byId]";
            var parameters = new { deckID = id};
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }
    }
}
