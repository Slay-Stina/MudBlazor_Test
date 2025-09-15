using MudBlazor_Test_Domain.Entities;
using MudBlazor_Test_Domain.Repositories;

namespace MudBlazor_Test.Application;

public class LineService : ILineService
{
    private readonly ILineRepository _repository;
    public LineService(ILineRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Line>> GetAllLinesAsync() => _repository.GetAllAsync();
    public Task<Line?> GetLineAsync(string name) => _repository.GetByNameAsync(name);
    public Task AddLineAsync(Line line) => _repository.AddAsync(line);
    public Task UpdateLineAsync(Line line) => _repository.UpdateAsync(line);
    public Task DeleteLineAsync(string name)
        => _repository.GetByNameAsync(name).ContinueWith(t => t.Result != null ? _repository.DeleteAsync(t.Result) : Task.CompletedTask).Unwrap();
    public Task SetDefaultLineAsync(string name)
        => _repository.GetByNameAsync(name).ContinueWith(t => t.Result != null ? _repository.SetDefaultAsync(t.Result) : Task.CompletedTask).Unwrap();
    public Task SetSelectedLineAsync(string name)
        => _repository.GetByNameAsync(name).ContinueWith(t => t.Result != null ? _repository.SetSelectedAsync(t.Result) : Task.CompletedTask).Unwrap();
}