using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class RailDTO
    {
        public int RailId { get; set; }
        public string Product_Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public void Load(Railing rail)
        {
            RailId = rail.Id;
            Product_Code = rail.Product_Code;
            Name = rail.Name;
            Price = rail.Price_Per_SqFt;
        }
    }
}
