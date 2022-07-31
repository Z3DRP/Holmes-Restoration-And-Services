using Holmes_Services.Models.DomainModels;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Holmes_Services.Data_Access.Repos
{
    public static class RailRepo
    {
        private static string _con = DbConnector.GetConnection();
        static IEnumerable<Railing> _railing;

        public static IEnumerable<Railing> GetAllRails()
        {
            string procedure = "[sp_GetRailings]";
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? Enumerable.Empty<Railing>() : _railing;
        }

        public static IEnumerable<Railing> GetRailsByPrice(double price)
        {
            string procedure = "[sp_get_rails_by_price]";
            var parameter = new { price = price };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? Enumerable.Empty<Railing>() : _railing;
        }

        public static Railing GetRailById(int id)
        {
            string procedure = "[sp_get_rails_by_id]";
            var parameter = new { id = id };
            Railing rail = new Railing();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rail = db.QuerySingle<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }

            return rail == null ? new Railing() : rail;
        }

        public static IEnumerable<Railing> GetRailByGroup(int group)
        {
            string procedure = "[sp_get_rails_by_groupId]";
            var parameter = new { id = group };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? Enumerable.Empty<Railing>() : _railing;
        }

        public static IEnumerable<Railing> GetRailByType(string type)
        {
            string procedure = "[sp_GetRailByType]";
            var parameter = new { type = type };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? Enumerable.Empty<Railing>() : _railing;
        }

        public static bool VerifyRailing(string pCode, string pName)
        {
            string procedure = "[sp_verify_rail]";
            var parameters = new {productCode = pCode, name = pName};
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }

        public static bool VerifyRailingById(int id)
        {
            string procedure = "[sp_verify_rail_byId]";
            var parameters = new { railID = id};
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }
    }
}
