using Microsoft.AspNetCore.Mvc;
using MudBlazor_Test.Application;
using MudBlazor_Test.Contracts;
using MudBlazor_Test_Domain.Entities;

namespace MudBlazor_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinesController : ControllerBase
{
    private readonly ILineService _service;
    public LinesController(ILineService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<LineResponse>> GetAll()
    {
        var lines = await _service.GetAllLinesAsync();
        return lines.Select(l => new LineResponse
        {
            Name = l.Name,
            IpAddress = l.IpAddress,
            PortNumber = l.Portnumber,
            IsDefault = l.IsDefault,
            IsSelected = l.IsSelected
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(LineRequest request)
    {
        var line = new Line(request.Name, request.IpAddress, request.PortNumber);
        if (request.IsDefault) line.SetDefault(true);
        if (request.IsSelected) line.SetSelected(true);
        await _service.AddLineAsync(line);
        return Ok();
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, LineRequest request)
    {
        var existing = await _service.GetLineAsync(name);
        if (existing == null)
            return NotFound();
        existing.Update(request.Name, request.IpAddress, request.PortNumber);
        existing.SetDefault(request.IsDefault);
        existing.SetSelected(request.IsSelected);
        await _service.UpdateLineAsync(existing);
        return Ok();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        await _service.DeleteLineAsync(name);
        return Ok();
    }

    [HttpPost("set-default/{name}")]
    public async Task<IActionResult> SetDefault(string name)
    {
        await _service.SetDefaultLineAsync(name);
        return Ok();
    }

    [HttpPost("set-selected/{name}")]
    public async Task<IActionResult> SetSelected(string name)
    {
        await _service.SetSelectedLineAsync(name);
        return Ok();
    }
}