using Holmes_Services.Models.DomainModels;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Holmes_Services.Data_Access.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private string _con = DbConnector.GetConnection();
        private IEnumerable<Customer> _customers;

        public IEnumerable<Customer> GetAllCustomers()
        {
            string procedure = "[sp_GetCustomers]";
            InitCustomers();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _customers = db.Query<Customer>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _customers == null ? new List<Customer>() : _customers;
        }

        public IEnumerable<Customer> GetCustomerById(int id)
        {
            string procedure = "[sp_GetCustomerById]";
            var parameter = new { id = id };
            InitCustomers();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _customers = db.Query<Customer>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _customers == null ? new List<Customer>() : _customers;
        }

        public IEnumerable<Customer> GetCustomerByName(string firstname, string lastname)
        {
            string procedure = "[sp_GetCustomerByName]";
            var parameters = new {firstname = firstname, lastname = lastname };
            InitCustomers();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _customers = db.Query<Customer>(procedure, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return _customers == null ? new List<Customer>() : _customers;

        }

        public IEnumerable<Customer> SearchCustomers(string firstname, string lastname)
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

        public bool AddCustomer(Customer customer)
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

        public bool UpdateCustomer(Customer customer)
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

        public bool DeleteCustomer(int id)
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

        public bool VerifyCustomer(string firstname, string lastname)
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
        public void InitCustomers() => _customers = new List<Customer>();
        
    }
}
