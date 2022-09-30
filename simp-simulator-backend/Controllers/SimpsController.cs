using simp_simulator_models;
using Microsoft.AspNetCore.Mvc;
using simp_simulator_backend.Services;
using simp_simulator_models.BsonMappers;

namespace simp_simulator_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimpsController : ControllerBase
{
    private readonly SimpsService _simpsService;

    public SimpsController(SimpsService simpsService) =>
        _simpsService = simpsService;

    [HttpGet]
    public async Task<List<Simp>> Get() =>
        await _simpsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Simp>> Get(string id)
    {
        var simp = await _simpsService.GetAsync(id);

        if (simp is null)
        {
            return NotFound();
        }

        return simp;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Simp newSimp)
    {
        await _simpsService.CreateAsync(newSimp);

        return CreatedAtAction(nameof(Get), new { id = newSimp.Id }, newSimp);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Simp updatedSimp)
    {
        var simp = await _simpsService.GetAsync(id);

        if (simp is null)
        {
            return NotFound();
        }

        updatedSimp.Id = simp.Id;

        await _simpsService.UpdateAsync(id, updatedSimp);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var simp = await _simpsService.GetAsync(id);

        if (simp is null)
        {
            return NotFound();
        }

        await _simpsService.RemoveAsync(id);

        return NoContent();
    }
}
