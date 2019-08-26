namespace WebApp.Web.Models
{
    using System.Collections.Generic;

    public class UserBindingModel
    {
        public UserBindingModel()
        {
            Score = new List<UserScore>();
        }

        public string Name { get; set; }

        public IEnumerable<UserScore> Score { get; set; }
    }
}
