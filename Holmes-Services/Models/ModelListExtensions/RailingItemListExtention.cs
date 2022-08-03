using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;

namespace Holmes_Services.Models.ModelListExtensions
{
    public static class RailingItemListExtention
    {
        public static List<RailItemDTO> ToDTO(this List<RailItem> list) =>
            list.Select(r => new RailItemDTO
            {
                RailId = r.Rail.RailId,
            }).ToList();
    }
}
