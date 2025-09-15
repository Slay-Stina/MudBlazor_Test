using MudBlazor_Test_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MudBlazor_Test_Domain.Repositories;

public interface ILineRepository
{
    Task<IEnumerable<Line>> GetAllAsync();
    Task<Line?> GetByNameAsync(string name);
    Task AddAsync(Line line);
    Task UpdateAsync(Line line);
    Task DeleteAsync(Line line);
    Task SetDefaultAsync(Line line);
    Task SetSelectedAsync(Line line);
}
