using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Data_Access.Repos
{
    public interface IDesignRepo
    {
        IEnumerable<Design> GetAllDesigns();
        IEnumerable<Design> GetDesign(int id);
        IEnumerable<Design> GetCustomerDesigns(int customerId);
        public bool AddDesign(Design design);
        public bool UpdateDesign(Design design);
        public bool DeleteDesign(int id);
        public bool VerifyCustomerDesign(int customerID, int deckID, int railID);
    }
}
