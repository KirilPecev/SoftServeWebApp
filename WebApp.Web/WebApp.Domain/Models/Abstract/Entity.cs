namespace WebApp.Domain.Models.Abstract
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
