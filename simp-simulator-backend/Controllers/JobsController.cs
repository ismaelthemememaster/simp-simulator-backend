using Microsoft.AspNetCore.Mvc;
using simp_simulator_backend.Services;
using simp_simulator_models.BsonMappers;

namespace simp_simulator_backend.Controllers;

/// <summary>
/// This is a simple controller for jobs, which a simp must have.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly JobsService _jobsService;

    public JobsController(JobsService jobsService) =>
        _jobsService = jobsService;

    /// <summary>
    /// Get all jobs.
    /// </summary>
    /// <returns>Return an array with all job objects.</returns>
    /// <remarks>
    /// If a simp has no job just assign him into "NEET" or "Selling dignity"
    /// </remarks>
    /// <response code="200">Returns an array of Job objects</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpGet]
    public async Task<List<Job>> Get() =>
        await _jobsService.GetAsync();

    /// <summary>
    /// Get one specific job by id.
    /// </summary>
    /// <returns>Return a single Job object.</returns>
    /// <remarks>
    /// The "pay" attribute is an average, we don't really care how much a simp earns, if he's a true simp he'll go into debt
    /// </remarks>
    /// <response code="200">Returns a Job object identified with the id sent</response>
    /// <response code="204">If there is no job with the id sent</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Job>> Get(string id)
    {
        var job = await _jobsService.GetAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        return job;
    }

    /// <summary>
    /// Create a job.
    /// </summary>
    /// <remarks>
    /// Maybe in the future implement a way of impeding job duplication, we don't want 26 variations of Neet, NEET, NeeT, neet...
    /// </remarks>
    /// <response code="201">If the Job was created successfully</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpPost]
    public async Task<IActionResult> Post(Job newJob)
    {
        await _jobsService.CreateAsync(newJob);

        return CreatedAtAction(nameof(Get), new { id = newJob.Id }, newJob);
    }

    /// <summary>
    /// Update a job.
    /// </summary>
    /// <remarks>
    /// If this endpoint asks you two times for the id, call whoever programmed this trash and tell him "Railgun S?, more like Railgun Sucks!".
    /// Call an ambulance while he tracks your ip, maybe get life insurance
    /// </remarks>
    /// <response code="200">If the job sent was successfully updated</response>
    /// <response code="204">If there is no job with the id sent</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Job updatedJob)
    {
        var job = await _jobsService.GetAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        updatedJob.Id = job.Id;

        await _jobsService.UpdateAsync(id, updatedJob);

        return NoContent();
    }

    /// <summary>
    /// Delete a job.
    /// </summary>
    /// <response code="200">If the job sent was successfully deleted</response>
    /// <response code="204">If there is no job with the id sent</response>
    /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var job = await _jobsService.GetAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        await _jobsService.RemoveAsync(id);

        return NoContent();
    }
}
