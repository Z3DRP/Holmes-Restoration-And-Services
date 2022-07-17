using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set;  }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public void Load(Customer customer)
        {
            Id = customer.Id;
            Firstname = customer.First_Name;
            Lastname = customer.Last_Name;
            Email = customer.Email;
            Phone = customer.Phone_Number;
            City = customer.City;
            State = customer.State;
            Zipcode = customer.Zipcode;
        }

    }
}
