using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Services.PositionService
{
    public interface IPositionService
    {
        IEnumerable<Position> GetPositions();
    }
}
