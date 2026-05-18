using cw7.DTOs;
using cw7.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly IPCService _service;

    public PCsController(IPCService service)
    {
        _service = service;
    }
    

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetPcs();

        return Ok(result);
    }
    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetComponents(int id)
    {
        var result = await _service.GetComponents(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreatePcDto dto)
    {
        var id = await _service.Create(dto);

        return Created($"/api/pcs/{id}", null);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePcDto dto)
    {
        var ok = await _service.Update(id, dto);

        if (!ok)
            return NotFound();

        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.Delete(id);

        if (!ok)
            return NotFound();

        return NoContent();
    }
}