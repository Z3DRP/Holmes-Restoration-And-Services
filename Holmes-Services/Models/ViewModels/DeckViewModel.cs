using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.RouteDictionaries;

namespace Holmes_Services.Models.ViewModels
{
    public class DeckViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Deck_Type Type { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        public DeckViewModel() { }
        public DeckViewModel(Decking deck)
        {
            Id = deck.Id;
            Name = deck.Name;
            Type = deck.Type;
            Image = deck.Image;
            Price = deck.Price_Per_SqFt;
        }
    }
}
