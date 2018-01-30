using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface ILotService : IGenericService<Lot>
    {
        IEnumerable<Lot> GetAllByClient(int UserId);
        Response Save(Lot lot);
        Lot GetByCode(string LotCode);
        IEnumerable<Lot> GetAllForAnalysis();
        Response Exclude(int LotId);
    }
}
