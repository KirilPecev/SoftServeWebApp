namespace WebApp.Data.Repo.PositionRepo
{
    using Domain;
    using System.Collections.Generic;

    public interface IPositionRepository
    {
        IEnumerable<Position> GetPositions();
    }
}
