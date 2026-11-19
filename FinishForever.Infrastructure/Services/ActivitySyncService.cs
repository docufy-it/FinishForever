using FinishForever.Domain.Entities;
using FinishForever.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinishForever.Infrastructure.Services;

public class ActivitySyncService
{
    private readonly ApplicationDbContext _db;

    public ActivitySyncService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task SyncAsync(
    Guid athleteId,
    IEnumerable<StravaActivity> activities)
    {
        foreach(var activity in activities)
        {
            var existing =
                await _db.Activities
                    .FirstOrDefaultAsync(
                        x => x.Id == activity.Id);

            if(existing == null)
            {
                
                _db.Activities.Add(activity);
            }

            activity.AthleteId = athleteId;
        }

        await _db.SaveChangesAsync();
    }
}