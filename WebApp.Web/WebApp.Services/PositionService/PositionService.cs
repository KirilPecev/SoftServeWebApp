using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.PositionService
{
    public class PositionService : IPositionService
    {
        private IPositionRepo positionRepo;
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
