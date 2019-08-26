namespace WebApp.Services.PositionService
{
    using Domain;
    using System.Collections.Generic;

    public interface IPositionService
    {
        IEnumerable<Position> GetPositions();
    }
}
