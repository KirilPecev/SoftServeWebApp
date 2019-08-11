using System.Collections.Generic;

namespace WebApp.Web.Models
{
    public class UserBindingModel
    {
        public string Name { get; set; }
        public List<UserScore> Score { get; set; }

        public UserBindingModel()
        {
            Score = new List<UserScore>();
        }
    }
}
