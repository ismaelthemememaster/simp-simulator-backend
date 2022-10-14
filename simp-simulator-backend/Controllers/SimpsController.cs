using Microsoft.AspNetCore.Mvc;
using simp_simulator_backend.Services;
using simp_simulator_models.BsonMappers;

namespace simp_simulator_backend.Controllers;

/// <summary>
/// This controller is in charge of simps. A simp must have a job from the job controller.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SimpsController : ControllerBase
{
    private readonly SimpsService _simpsService;
    private readonly JobsService _jobsService;

    public SimpsController(SimpsService simpsService, JobsService jobsService)
    {
        _simpsService = simpsService;
        _jobsService = jobsService;
    }

    /// <summary>
    /// Get all simps.
    /// </summary>
    /// <returns>Return an array with all simp objects.</returns>
    /// <remarks>
    /// Assume that this table will have billions of entries in the near future, milking simps has become a thriving business
    /// </remarks>
    /// <response code="200">Returns an array of Simp objects</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpGet]
    public async Task<List<Simp>> Get() =>
        await _simpsService.GetAsync();

    /// <summary>
    /// Get one specific simp by id.
    /// </summary>
    /// <returns>Return a single Simp object.</returns>
    /// <remarks>
    /// Make sure to let him know he will never be stared at by a woman, we can't milk them if they don't feel the need for female attention
    /// </remarks>
    /// <response code="200">Returns a Simp object identified with the id sent</response>
    /// <response code="204">If there is no simp with the id sent</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
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

    /// <summary>
    /// Create a simp.
    /// </summary>
    /// <remarks>
    /// Maybe in the future make their real life ids into database ids, although I'm not sure mongo can or should do that,
    /// but we definitely don't want more than one simp with the same id
    /// </remarks>
    /// <response code="201">If the Job was created successfully</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpPost]
    public async Task<IActionResult> Post(Simp newSimp)
    {
        var job = await _jobsService.GetAsync(newSimp.Job);

        if (job is null)
        {
            return NotFound();  // TODO: Return an error JobNotFound or something telling the user to send a valid job
        }

        await _simpsService.CreateAsync(newSimp);

        return CreatedAtAction(nameof(Get), new { id = newSimp.Id }, newSimp);
    }

    /// <summary>
    /// Update a simp.
    /// </summary>
    /// <remarks>
    /// For now this is the only way of banning a simp, maybe in the future implement a PATCH to ban them when they send 1 dollar
    /// The e-girl only gets a fraction of that, you know?. Stop being poor.
    /// </remarks>
    /// <response code="200">If the simp sent was successfully updated</response>
    /// <response code="204">If there is no simp with the id sent</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
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

    /// <summary>
    /// Delete a simp.
    /// </summary>
    /// <response code="200">If the simp sent was successfully deleted</response>
    /// <response code="204">If there is no simp with the id sent</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
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
