namespace Domain.Entities
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        protected BaseAuditableEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
