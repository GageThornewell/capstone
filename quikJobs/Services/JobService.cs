using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using quikJobs.Data;

namespace quikJobs.Services;

public class JobService
{
    private readonly KwicJobsContext _context;

    public JobService(KwicJobsContext context)
    {
        _context = context;
    }

    public async Task<List<Job>> GetAllJobsAsync()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task<Job?> GetJobByIdAsync(int jobId)
    {
        return await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);
    }

    public async Task<bool> SaveJobAsync(Job newJob)
    {
        try
        {
            _context.Jobs.Add(newJob);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            // Log the exception or handle it as necessary
            return false;
        }
    }

    public async Task<List<Job>> GetRandomJobsAsync(int count = 3)
    {
        var allJobs = await _context.Jobs.ToListAsync();
        return allJobs.OrderBy(j => Guid.NewGuid()).Take(count).ToList();
    }

    public async Task<List<Job>> GetJobsByUserIdAsync(string userId)
    {
        return await _context.Jobs
                             .Where(j => j.UserId == userId)
                             .ToListAsync();
    }

    /// <summary>
    /// Deletes a job by its JobId.
    /// </summary>
    /// <param name="jobId">The JobId of the job to delete.</param>
    /// <returns>True if successful; false otherwise.</returns>
    public async Task<bool> DeleteJobByIdAsync(int jobId)
    {
        try
        {
            var job = await GetJobByIdAsync(jobId);
            if (job == null)
                return false; // Job not found

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            // Log the exception or handle it as necessary
            return false;
        }
    }

    /// <summary>
    /// Updates an existing job based on its JobId.
    /// </summary>
    /// <param name="updatedJob">The job object containing updated information.</param>
    /// <returns>True if successful; false otherwise.</returns>
    public async Task<bool> EditJobAsync(Job updatedJob)
    {
        try
        {
            var existingJob = await GetJobByIdAsync(updatedJob.JobId);
            if (existingJob == null)
                return false; // Job not found

            // Update job fields
            existingJob.Name = updatedJob.Name;
            existingJob.Description = updatedJob.Description;
            existingJob.Type = updatedJob.Type;
            existingJob.Pay = updatedJob.Pay;
            existingJob.Url = updatedJob.Url;
            existingJob.Views = updatedJob.Views;
            existingJob.CreatedOn = updatedJob.CreatedOn;

            // Save changes
            _context.Jobs.Update(existingJob);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            // Log the exception or handle it as necessary
            return false;
        }
    }
}
