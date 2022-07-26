using Holmes_Services.Models.DomainModels;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Holmes_Services.Data_Access.Repos
{
    public class RailRepo : IRailRepo
    {
        private string _con = DbConnector.GetConnection();
        IEnumerable<Railing> _railing;

        public IEnumerable<Railing> GetAllRails()
        {
            string procedure = "[sp_GetRailings]";
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? new List<Railing>() : _railing;
        }

        public IEnumerable<Railing> GetRailsByPrice(double price)
        {
            string procedure = "[sp_get_rails_by_price]";
            var parameter = new { price = price };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? new List<Railing>() : _railing;
        }

        public IEnumerable<Railing> GetRailById(int id)
        {
            string procedure = "[sp_get_rails_by_id]";
            var parameter = new { id = id };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? new List<Railing>() : _railing;
        }

        public IEnumerable<Railing> GetRailByGroup(int group)
        {
            string procedure = "[sp_get_rails_by_groupId]";
            var parameter = new { id = group };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? new List<Railing>() : _railing;
        }

        public IEnumerable<Railing> GetRailByType(string type)
        {
            string procedure = "[sp_GetRailByType]";
            var parameter = new { type = type };
            _railing = new List<Railing>();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _railing = db.Query<Railing>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _railing == null ? new List<Railing>() : _railing;
        }

    }
}
