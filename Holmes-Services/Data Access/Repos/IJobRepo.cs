using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Data_Access.Repos
{
    public interface IJobRepo
    {
        IEnumerable<Job> GetAllJobs(int cId);
        IEnumerable<Job> GetJob(int id);
        IEnumerable<Job> GetCustomerJobs(int userId);
        public bool AddJob(Job job);
        public bool UpdateJob(Job job);
        public bool DeleteJob(int id);
        public bool VerifyJob(Job job);
    }
}
