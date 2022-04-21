using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tracr.Server.Data;

namespace Tracr.Server.Hubs
{
    public class AlertHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public AlertHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CheckNotifications()
        {
            await CheckEmptyProperties();
            await CheckLeasesEnding();
        }

        private async Task CheckEmptyProperties()
        {
            var idUser = Context.UserIdentifier;

            var properties = await _context.Properties.Include(p => p.Renters).Where(p => p.ApplicationUserId == idUser).ToListAsync();
            foreach(var property in properties.Where(p => p.Renters == null || p.Renters.Where(r => r.EndingMonth > DateOnly.FromDateTime(DateTime.Today)).Count() == 0))
            {
                await Clients.Caller.SendAsync("Notification", property.Id, "Empty Property", $"Your property ({property.Name}) currently does not have any renters!");
            }
        }

        private async Task CheckLeasesEnding()
        {
            var idUser = Context.UserIdentifier;

            var properties = await _context.Properties.Include(p => p.Renters).Where(p => p.ApplicationUserId == idUser).ToListAsync();
            
            foreach (var property in properties.Where(p => p.Renters.Count > 0))
            {
                foreach (var renter in property.Renters.Where(r => r.EndingMonth.ToDateTime(TimeOnly.MinValue).Subtract(DateTime.Today).TotalDays < 90))
                {
                    await Clients.Caller.SendAsync("Notification", property.Id, "Lease Ending", $"{renter.FirstName} {renter.LastName}'s lease is ending on {renter.EndingMonth}!");
                }
            }
        }

    }
}
