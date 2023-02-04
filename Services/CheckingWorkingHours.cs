using Microsoft.EntityFrameworkCore;

namespace CarWash.Services
{
    /*
     * IHostedService for checking and editing CarWashOpen
     */
    public class CheckingWorkingHours : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public CheckingWorkingHours(IServiceProvider serviceProvider)
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
                var today = DateTime.Now.Hour;
                var cwopen = await context.CarWashes.Where(x => x.OpeningHours < today + 1).ToListAsync();
                if (cwopen.Any())
                {
                    foreach (var cwo in cwopen)
                    {
                        cwo.CarWashOpen = true;
                    }
                    await context.SaveChangesAsync();
                }

                var cwclosed = await context.CarWashes.Where(x => x.ClosingHours < today + 1).ToListAsync();
                if (cwclosed.Any())
                {
                    foreach (var cwc in cwclosed)
                    {
                        cwc.CarWashOpen = false;
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
