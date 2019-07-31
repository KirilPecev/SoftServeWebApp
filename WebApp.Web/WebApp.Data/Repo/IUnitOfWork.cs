using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Data.Repo
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();

    }
}
