using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tracr.Server.Data;

namespace Tracr.Server.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public NotificationHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CheckNotifications()
        {
            await CheckEmptyProperties();
        }

        private async Task CheckEmptyProperties()
        {
            var idUser = Context.UserIdentifier;

            var properties = await _context.Properties.Include(p => p.Renters).Where(p => p.ApplicationUserId == idUser).ToListAsync();
            foreach(var property in properties.Where(p => p.Renters == null || p.Renters.Count == 0))
            {
                await Clients.Caller.SendAsync("Notification", "Empty Property", $"Your property ({property.Name}) currently does not have any renters!");
            }
        }
    }
}
