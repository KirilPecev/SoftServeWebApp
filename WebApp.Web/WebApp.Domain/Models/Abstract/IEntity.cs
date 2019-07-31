namespace WebApp.Domain.Models.Abstract
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
