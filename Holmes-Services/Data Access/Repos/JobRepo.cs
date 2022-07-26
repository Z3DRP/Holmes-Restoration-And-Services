using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Holmes_Services.Data_Access.Repos
{
    public class JobRepo : IJobRepo
    {
        private string _con = DbConnector.GetConnection();
        private IEnumerable<Job> _jobs;

        public IEnumerable<Job> GetAllJobs(int cId)
        {
            string procedure = "[sp_GetJobs]";
            InitJobs();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _jobs = db.Query<Job>(procedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return _jobs == null ? Enumerable.Empty<Job>() : _jobs;
        }

        public IEnumerable<Job> GetJob(int jobId)
        {
            string procedure = "[sp_GetJob]";
            var parameter = new { id = jobId };
            InitJobs();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _jobs = db.Query<Job>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _jobs == null ? Enumerable.Empty<Job>() : _jobs;
        }

        public IEnumerable<Job> GetCustomerJobs(int id)
        {
            string procedure = "[sp_GetCustomerJobs]";
            var parameter = new { customerId = id };
            InitJobs();

            using (IDbConnection db = new MySqlConnection(_con))
            {
                _jobs = db.Query<Job>(procedure, parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return _jobs == null ? Enumerable.Empty<Job>() : _jobs;
        }

        public bool AddJob(Job job)
        {
            string procedure = "[sp_AddJob]";
            int rowsAffected = 0;
            var parameters = new
            {
                customerId = job.Customer_Id,
                DesignId = job.Design_Id
            };

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameters, commandType:CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool UpdateJob(Job job)
        {
            string procedure = "[sp_UpdateJob]";
            int rowsAffected = 0;
            var parameters = new
            {
                jobId = job.Id,
                customerId = job.Customer_Id,
                designId = job.Design_Id
            };

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool DeleteJob(int id)
        {
            string procedure = "[sp_DeleteJob]";
            var parameter = new { id = id };
            int rowsAffected = 0;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameter, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool VerifyJob(int customerID, int designID)
        {
            string procedure = "[sp_verify_job]";
            var parameters = new {customerId = customerID, designId = designID };
            bool doesExist;

            using (IDbConnection db = new MySqlConnection(_con))
            {
                doesExist = db.ExecuteScalar<bool>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return doesExist;
        }
        public void InitJobs() => _jobs = new List<Job>();
    }
}
