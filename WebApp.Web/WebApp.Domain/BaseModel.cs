namespace WebApp.Domain
{
    public class BaseModel<T> : IEntity
    {
        public T Id { get; set; }
    }
}
