using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface ILaboratoryService : IGenericService<Laboratory>
    {
        IEnumerable<Laboratory> GetAllByClient(int UserId);
        Response Save(Laboratory laboratory);
        Response Exclude(int laboratoryId);
        Laboratory GetByCode(string code);
    }
}
