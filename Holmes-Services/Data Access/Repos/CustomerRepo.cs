using Holmes_Services.Models.DomainModels;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Holmes_Services.Data_Access.Repos
{
    public static class CustomerRepo
    {
        private static string _con = DbConnector.GetConnection();
        private static IEnumerable<Customer>? _customers;

        public static IEnumerable<Customer> GetAllCustomers()
        {
            string procedure = "[sp_GetCustomers]";
            InitCustomers();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _customers = db.Query<Customer>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _customers == null ? Enumerable.Empty<Customer>() : _customers;
        }

        public static Customer GetCustomerById(int id)
        {
            string procedure = "[sp_GetCustomerById]";
            var parameter = new { id = id };
            Customer customer = new Customer();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _customers = db.Query<Customer>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return customer == null ? new Customer() : customer;
        }

        public static Customer GetCustomerByName(string firstname, string lastname)
        {
            string procedure = "[sp_GetCustomerByName]";
            var parameters = new {firstname = firstname, lastname = lastname };
            Customer customer = new Customer();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                customer = db.QuerySingle<Customer>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return customer == null ? new Customer() : customer;
        }

        public static IEnumerable<Customer> SearchCustomers(string firstname, string lastname)
        {
            string procedure = "[sp_search_customers]";
            var parameters = new { firstname = firstname, lastname = lastname };
            InitCustomers();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _customers = db.Query<Customer>(procedure, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return _customers == null ? Enumerable.Empty<Customer>() : _customers;
        }

        public static bool AddCustomer(Customer customer)
        {
            string procedure = "[sp_AddCustomer]";
            int rowsAffected = 0;
            var parameters = new
            {
                fname = customer.First_Name,
                lname = customer.Last_Name,
                email = customer.Email,
                phone = customer.Phone_Number,
                street = customer.Street_Address,
                city = customer.City,
                state = customer.State,
                zip = customer.Zipcode
            };

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            // check to see if we get rows affected back

            return rowsAffected > 0 ? true : false;
        }

        public static bool UpdateCustomer(Customer customer)
        {
            string procedure = "[sp_UpdateCustomer]";
            int rowsAffected = 0;
            var parameters = new
            {
                fname = customer.First_Name,
                lname = customer.Last_Name,
                email = customer.Email,
                phone = customer.Phone_Number,
                street = customer.Street_Address,
                city = customer.City,
                state = customer.State,
                zip = customer.Zipcode
            };

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }

        public static bool DeleteCustomer(int id)
        {
            string procedure = "[sp_DeleteCustomer]";
            var parameter = new { id = id };
            int rowsAffect = 0;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffect = db.Execute(procedure, parameter, commandType: CommandType.StoredProcedure);
            }

            return rowsAffect > 0 ? true : false;
        }

        public static bool VerifyCustomer(string firstname, string lastname)
        {
            string procedure = "[sp_verify_custmomer]";
            var parameters = new {firstname = firstname, lastname = lastname };
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType:CommandType.StoredProcedure);
            }

            return doesExist;
        }

        public static bool VerifyCustomerById(int id)
        {
            string procedure = "[sp_verify_custmomer_byId]";
            var parameters = new { customerID = id };
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }
        public static void InitCustomers() => _customers = new List<Customer>();
        
    }
}
