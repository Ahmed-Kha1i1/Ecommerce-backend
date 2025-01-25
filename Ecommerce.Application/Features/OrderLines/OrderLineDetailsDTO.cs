using Ecommerce.Application.Features.ProductItems;
using System.Text.Json.Serialization;

namespace Ecommerce.Application.Features.OrderLines
{
    public class OrderLineDetailsDTO
    {
        public int Id { get; set; }//
        public int ProductId { get; set; }//
        public int OrderId { get; set; }//
        public string ImageName { get; set; }//
        public string Title { get; set; }//
        public int Quantity { get; set; }//
        public decimal Price { get; set; }//
        public bool IsCanceled { get; set; }//
        [JsonIgnore]
        public string? Str_Variations { get; set; }
        public ICollection<VariationDTO> Variations { get; set; }
    }
}
