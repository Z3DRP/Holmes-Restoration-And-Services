using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Data_Access.Repos
{
    public interface IRailRepo
    {
        IEnumerable<Railing> GetAllRails();
        IEnumerable<Railing> GetRailsByPrice(double price);
        IEnumerable<Railing> GetRailById(int id);
        IEnumerable<Railing> GetRailByType(string type);
        IEnumerable<Railing> GetRailByGroup(int group);
    }
}
