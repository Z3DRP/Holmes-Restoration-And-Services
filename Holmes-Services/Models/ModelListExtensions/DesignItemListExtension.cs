using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;

namespace Holmes_Services.Models.ModelListExtensions
{
    public static class DesignItemListExtension
    {
        public static List<DesignItemDTO> ToDTO(this List<DesignItem> list) =>
            list.Select(d => new DesignItemDTO
            {
                DesignID = d.DesignId,
                DeckId = d.Deck.Id,
                RailId = d.Rail.Id,
                Estimate = d.Price
            }).ToList();
    }
}
