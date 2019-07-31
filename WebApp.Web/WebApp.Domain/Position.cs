namespace WebApp.Domain
{
    public class Position : BaseModel<int>
    {
        public string Name { get; set; }

        public string UserId { get; set; }
        public virtual WebAppUser User { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
