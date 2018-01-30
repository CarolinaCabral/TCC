using System;
using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using System.Linq;

namespace CoffeeExperience.Data.Repositories
{
    public class LotRepository : GenericRepository<Lot> , ILotRepository
    {
        public LotRepository(IGetContext getContext) : base(getContext)
        {

        }

        public Lot GetByCode(string LotCode)
        {
            return this.Get(p => p.Code == LotCode);
            //vai no banco de dados e retorna o primeiro objeto Lot pelo código
        }

        public Lot GetByCodeAsNoTracking(string LotCode)
        {
            return this.GetAsNoTracking(p => p.Code == LotCode);
        }
    }
}
