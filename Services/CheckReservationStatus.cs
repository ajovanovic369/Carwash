using Microsoft.EntityFrameworkCore;

namespace CarWash.Services
{
    /*
     * IHostedService for checking and editing appointment status, after 15 minutes reservation is automatically rejected
     */
    public class CheckReservationStatus : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public CheckReservationStatus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var cwopen = await context.Schedulings.Where(x => x.Status == "Pending").ToListAsync();
                if (cwopen.Any())
                {
                    foreach (var cwo in cwopen)
                    {
                        var input = cwo.CurrentDate - DateTime.Now;
                        var inputMinutes = input.Minutes;
                        if (inputMinutes <= -15)
                        {
                            cwo.Status = "Rejected";
                        }
                    }
                    await context.SaveChangesAsync();
                }

            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

    }
}

