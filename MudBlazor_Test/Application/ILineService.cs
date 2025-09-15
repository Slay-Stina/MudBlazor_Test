using MudBlazor_Test_Domain.Entities;
using MudBlazor_Test.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MudBlazor_Test.Application;

public interface ILineService
{
    Task<IEnumerable<Line>> GetAllLinesAsync();
    Task<Line?> GetLineAsync(string name);
    Task AddLineAsync(Line line);
    Task UpdateLineAsync(Line line);
    Task DeleteLineAsync(string name);
    Task SetDefaultLineAsync(string name);
    Task SetSelectedLineAsync(string name);
}