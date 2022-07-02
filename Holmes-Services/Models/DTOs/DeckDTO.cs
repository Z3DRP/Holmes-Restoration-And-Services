using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.DTOs
{
    public class DeckDTO
    {
        public int DeckId { get; set; }
        public string Product_Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public void Load(Decking deck)
        {
            DeckId = deck.Id;
            Product_Code = deck.Product_Code;
            Name = deck.Name;
            Price = deck.Price_Per_SqFt;
        }

    }
}
