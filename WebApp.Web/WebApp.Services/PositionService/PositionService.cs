namespace WebApp.Services.PositionService
{
    using Data.Repo.PositionRepo;
    using Domain;
    using System.Collections.Generic;

    public class PositionService : IPositionService
    {
        private readonly IPositionRepo positionRepo;

        public PositionService(IPositionRepo positionRepo)
        {
            this.positionRepo = positionRepo;
        }

        public IEnumerable<Position> GetPositions()
        {
            return positionRepo.GetPositions();
        }
    }
}
