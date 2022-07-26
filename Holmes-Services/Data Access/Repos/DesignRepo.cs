using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Holmes_Services.Data_Access.Repos
{
    public class DesignRepo : IDesignRepo
    {
        private string _con = DbConnector.GetConnection();
        private IEnumerable<Design> _designs;

        public IEnumerable<Design> GetAllDesigns()
        {
            string procedure = "[sp_GetDesigns]";
            InitDesigns();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _designs = db.Query<Design>(procedure, commandType:CommandType.StoredProcedure).ToList();
            }

            return _designs == null ? Enumerable.Empty<Design>() : _designs;
        }

        public IEnumerable<Design> GetCustomerDesigns(int customerId)
        {
            string procedure = "[sp_GetDesignsByCustomer]";
            var parameter = new { customerID = customerId };
            InitDesigns();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _designs = db.Query<Design>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _designs == null ? Enumerable.Empty<Design>() : _designs;
        }

        public IEnumerable<Design> GetDesign(int id)
        {
            string procedure = "[sp_GetDesign]";
            var parameter = new { Id = id };
            InitDesigns();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _designs = db.Query<Design>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _designs == null ? Enumerable.Empty<Design>() : _designs;
        }
        public bool AddDesign(Design design)
        {
            string procedure = "[sp_AddDesign]";
            int rowsAffected = 0;
            var parameter = new
            {
                customerId = design.Customer_Id,
                deckingId = design.Decking_Id,
                railingId = design.Railing_Id,
                len = design.Length,
                wid = design.Width,
                sqft = design.Square_Ft,
                estimate = design.Estimate,
                pattern = design.Pattern,
                start = design.Start_Date
            };

            using(IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameter);
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool UpdateDesign(Design design)
        {
            string procedure = "[sp_UpdateDesign]";
            int rowsAffected;
            var parameters = new
            {
                customerId = design.Customer_Id,
                deckingId = design.Decking_Id,
                railingId = design.Railing_Id,
                len = design.Length,
                wid = design.Width,
                sqft = design.Square_Ft,
                estimate = design.Estimate,
                pattern = design.Pattern,
                start = design.Start_Date
            };

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool DeleteDesign(int id)
        {
            string procedure = "[sp_DeleteDesign]";
            var parameter = new { id = id };
            int rowsAffected;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameter, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool VerifyCustomerDesign(int customerID, int deckID, int railID)
        {
            string procedure = "[sp_verify_design]";
            var parameters = new {customerId = customerID, deckId = deckID, railId = railID};
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }
        private void InitDesigns() => _designs = new List<Design>();
    }
}
