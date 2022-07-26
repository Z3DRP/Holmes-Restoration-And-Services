using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Data_Access.Repos
{
    public interface ICustomerRepo
    {
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Customer> GetCustomerById(int id);
        IEnumerable<Customer> GetCustomerByName(string fname, string lname);
        IEnumerable<Customer> SearchCustomers(string fname, string lname);
        public bool AddCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(int id);
        public bool VerifyCustomer(string firstname, string lastname);

    }
}
