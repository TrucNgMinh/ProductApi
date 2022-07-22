using ProductApi.Entities.Common;
using ProductApi.Entities.EntityInterface;

namespace ProductApi.Entities.Products
{
    public class Product : BaseEntity, INameEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
