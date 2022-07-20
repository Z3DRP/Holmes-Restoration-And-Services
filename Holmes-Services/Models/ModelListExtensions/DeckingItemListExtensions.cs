using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using System.Collections.Generic;

namespace Holmes_Services.Models.ModelListExtensions
{
    public static class DeckingItemListExtensions
    {
        public static List<DeckItemDTO> ToDTO(this List<DeckItem> list) =>
            list.Select(d => new DeckItemDTO
            {
                DeckId = d.Deck.DeckId,
                Price = d.Price
            }).ToList();
    }
}
