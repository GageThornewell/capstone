using Microsoft.EntityFrameworkCore;
using quikJobs.Data;

namespace quikJobs.Services;

public class SavedJobService
{
    private readonly KwicJobsContext _context;

    public SavedJobService(KwicJobsContext context)
    {
        _context = context;

    }


    public async Task<bool> SaveJobAsync(SavedJob savedJob)
    {
        try
        {
            _context.SavedJobs.Add(savedJob);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Add this method to your SavedJobService class
    public async Task<bool> DeleteSavedJobAsync(SavedJob s)
    {
        try
        {
            // Find the saved job based on userId and jobId
            var savedJob = await _context.SavedJobs
                .FirstOrDefaultAsync(sj => sj.UserId == s.UserId && sj.JobId == s.JobId);

            // If the saved job doesn't exist, return false
            if (savedJob == null)
                return false;

            // Remove the saved job from the context
            _context.SavedJobs.Remove(savedJob);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            // Handle any errors that might occur
            return false;
        }
    }




    // Get all jobs saved by a specific userId
    public async Task<List<Job>> GetJobsSavedByUserId(string userId)
    {
        try
        {
            // Perform a join between SavedJobs and Jobs tables based on JobId
            var jobs = await _context.SavedJobs
                .Where(sj => sj.UserId == userId) // Filter by userId
                .Join(_context.Jobs, // Join with Jobs table
                    savedJob => savedJob.JobId,  // SavedJob.JobId
                    job => job.JobId,            // Job.JobId
                    (savedJob, job) => job)       // Select the Job object
                .ToListAsync(); // Retrieve the list of jobs

            return jobs;
        }
        catch
        {
            // Handle any errors that might occur and return an empty list
            return new List<Job>();
        }
    }


}
