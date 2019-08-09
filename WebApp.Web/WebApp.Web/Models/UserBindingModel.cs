using System.Collections.Generic;

namespace WebApp.Web.Models
{
    public class UserBindingModel
    {
        public string Name { get; set; }
        public List<UserScore> Score { get; set; }

        public UserBindingModel(string _Name, List<UserScore> _Score)
        {
            Name = _Name;
            Score = _Score;
        }
    }
}
