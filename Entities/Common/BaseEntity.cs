namespace ProductApi.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsDeactivated { get; set; } = false;
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
