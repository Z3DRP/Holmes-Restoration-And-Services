using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.ViewModels
{
    public class DeckingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Deck_Type Type { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        public DeckingViewModel(Decking deck)
        {
            Id = deck.Id;
            Name = deck.Name;
            Type = deck.Type;
            Image = deck.Image;
            Price = deck.Price_Per_SqFt;
        }
    }
}
