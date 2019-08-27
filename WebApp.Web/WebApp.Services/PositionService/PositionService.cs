namespace WebApp.Services.PositionService
{
    using Data.Repo.PositionRepo;
    using Domain;
    using System.Collections.Generic;

    public class PositionService : IPositionService
    {
        private readonly IPositionRepository position;

        public PositionService(IPositionRepository position)
        {
            this.position = position;
        }

        public IEnumerable<Position> GetPositions()
        {
            return position.GetPositions();
        }
    }
}
