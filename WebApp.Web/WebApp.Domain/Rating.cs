namespace WebApp.Domain
{
    public class Rating : BaseModel<int>
    {
        public string UserId { get; set; }
        public virtual WebAppUser User { get; set; }

        public int Scores { get; set; }
    }
}
