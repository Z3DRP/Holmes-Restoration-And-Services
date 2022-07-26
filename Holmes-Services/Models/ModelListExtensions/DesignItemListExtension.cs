using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;

namespace Holmes_Services.Models.ModelListExtensions
{
    public static class DesignItemListExtension
    {
        public static List<DesignItemDTO> ToDTO(this List<DesignItem> list) =>
            list.Select(d => new DesignItemDTO
            {
                Id = d.Design.DesignId,
                Price = d.Design.Estimate
            }).ToList();
    }
}
