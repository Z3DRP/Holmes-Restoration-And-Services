using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Holmes_Services.Data_Access.Repos
{
    public static class DesignRepo
    {
        private static string _con = DbConnector.GetConnection();
        private static IEnumerable<Design>? _designs;

        public static IEnumerable<Design> GetAllDesigns()
        {
            string procedure = "[sp_GetDesigns]";
            InitDesigns();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _designs = db.Query<Design>(procedure, commandType:CommandType.StoredProcedure).ToList();
            }

            return _designs == null ? Enumerable.Empty<Design>() : _designs;
        }

        public static IEnumerable<Design> GetCustomerDesigns(int customerId)
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

        public static Design GetDesignById(int id)
        {
            string procedure = "[sp_GetDesign]";
            var parameter = new { Id = id };
            Design? design = new Design();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                design = db.QuerySingle<Design>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }

            return design == null ? design : design;
        }
        public static bool AddDesign(Design design)
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

        public static bool UpdateDesign(Design design)
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

        public static bool DeleteDesign(int id)
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

        public static bool VerifyCustomerDesign(int customerID, int deckID, int railID)
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

        public static bool VerifyDesignById(int id)
        {
            string procedure = "[sp_verify_design_byId]";
            var parameters = new {designID = id};
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }
        private static void InitDesigns() => _designs = new List<Design>();
    }
}
