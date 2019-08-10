using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public interface IPositionRepo
    {
        IEnumerable<Position> GetPositions();
    }
}
